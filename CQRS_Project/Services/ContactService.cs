using CQRS_Project.Services.Abstract;
using System.Net.Http.Json;
using System.Text.Json;

namespace CQRS_Project.Services
{
	public class ContactService : IContactService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public ContactService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			// Gemini API key'i configuration'dan al
			_apiKey = configuration["Gemini:ApiKey"] ?? "";
		}

		public async Task<string> GenerateAutoReplyAsync(string message, string subject)
		{
			if (string.IsNullOrEmpty(_apiKey))
			{
				return "API anahtarı bulunamadı.";
			}

			int retryCount = 0;
			int maxRetries = 3;

			while (retryCount < maxRetries)
			{
				try
				{
					var prompt = $@"Sen profesyonel bir Türk müşteri hizmetleri temsilcisisin. 

Müşteri bilgileri:
Konu: {subject}
Mesaj: {message}

Görevlerin:
1. Müşterinin hangi dilde yazdığını tespit et (Türkçe/İngilizce/diğer)
2. Aynı dilde yanıt ver
3. Konusuna uygun profesyonel ve yardımsever bir cevap yaz
4. Kısa ve öz ol (max 2-3 cümle)
5. Müşteri hizmetleri tonu kullan

Örnek:
- Fiyat sorusu → fiyat bilgilerini ileteceğimizi söyle
- Hizmet sorusu → hizmetlerimiz hakkında bilgi vereceğimizi belirt  
- Genel soru → size yardımcı olmaktan memnuniyet duyarız de

Sadece yanıtı yaz, açıklama yapma:";

					var requestBody = new
					{
						contents = new[]
						{
							new
							{
								parts = new[]
								{
									new { text = prompt }
								}
							}
						},
						generationConfig = new
						{
							temperature = 0.7,
							topK = 40,
							topP = 0.95,
							maxOutputTokens = 200,
							stopSequences = new string[] { }
						},
						safetySettings = new[]
						{
							new
							{
								category = "HARM_CATEGORY_HARASSMENT",
								threshold = "BLOCK_MEDIUM_AND_ABOVE"
							},
							new
							{
								category = "HARM_CATEGORY_HATE_SPEECH",
								threshold = "BLOCK_MEDIUM_AND_ABOVE"
							}
						}
					};


					var response = await _httpClient.PostAsJsonAsync(
						$"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}",
						requestBody
					);

					// Rate limit kontrolü
					if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
					{
						retryCount++;
						if (retryCount >= maxRetries)
						{
							Console.WriteLine("Rate limit aşıldı");
							return "Sistem yoğunluğu nedeniyle şu anda yanıt veremiyoruz. Lütfen birkaç dakika sonra tekrar deneyin.";
						}

						var delayMs = (int)Math.Pow(2, retryCount) * 1000;
						Console.WriteLine($"Rate limit - {retryCount}. deneme, {delayMs / 1000}s bekleniyor...");
						await Task.Delay(delayMs);
						continue;
					}

					// Diğer HTTP hatalar
					if (!response.IsSuccessStatusCode)
					{
						var errorContent = await response.Content.ReadAsStringAsync();
						Console.WriteLine($"HTTP Error: {response.StatusCode}");
						Console.WriteLine($"Response: {errorContent}");

						return response.StatusCode switch
						{
							System.Net.HttpStatusCode.Unauthorized => "API anahtarı geçersiz. Lütfen Gemini API key'ini kontrol edin.",
							System.Net.HttpStatusCode.BadRequest => $"Geçersiz istek: {errorContent}",
							System.Net.HttpStatusCode.Forbidden => "API erişimi engellendi. API key'i kontrol edin.",
							_ => $"API hatası: {response.StatusCode}"
						};
					}

					var resultContent = await response.Content.ReadAsStringAsync();
					try
					{
						var result = JsonSerializer.Deserialize<JsonElement>(resultContent);

						if (result.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
						{
							var firstCandidate = candidates[0];
							if (firstCandidate.TryGetProperty("content", out var content))
							{
								if (content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
								{
									var firstPart = parts[0];
									if (firstPart.TryGetProperty("text", out var textElement))
									{
										var generatedText = textElement.GetString();
										if (!string.IsNullOrEmpty(generatedText))
										{
											var cleanResponse = CleanGeminiResponse(generatedText);
											Console.WriteLine($"Gemini AI Yanıtı: {cleanResponse}");
											return cleanResponse;
										}
									}
								}
							}
						}
					}
					catch (JsonException jsonEx)
					{
						Console.WriteLine($"JSON parse hatası: {jsonEx.Message}");
						Console.WriteLine($"Raw content: {resultContent}");
					}

					return "Mesajınız için teşekkür ederiz. En kısa sürede size geri dönüş yapacağız.";
				}
				catch (HttpRequestException ex)
				{
					retryCount++;
					Console.WriteLine($"HTTP hatası ({retryCount}/{maxRetries}): {ex.Message}");

					if (retryCount >= maxRetries)
					{
						return "Bağlantı hatası oluştu. Lütfen internet bağlantınızı kontrol edip tekrar deneyin.";
					}
					await Task.Delay(2000 * retryCount);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Genel hata: {ex.Message}");
					return "Mesajınız için teşekkür ederiz. Teknik bir sorun nedeniyle daha sonra size geri dönüş yapacağız.";
				}
			}

			Console.WriteLine("Maksimum deneme sayısı aşıldı");
			return "Mesajınız için teşekkür ederiz. Sistem yoğunluğu nedeniyle daha sonra size geri dönüş yapacağız.";
		}

		private string CleanGeminiResponse(string response)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(response))
					return "Mesajınız için teşekkür ederiz. En kısa sürede size geri dönüş yapacağız.";

				// Yanıtı temizle
				var cleaned = response.Trim();

				// Gereksiz açıklamaları kaldır
				var removePatterns = new[]
				{
					"Sen profesyonel", "Müşteri bilgileri:", "Görevlerin:", "Örnek:",
					"Sadece yanıtı yaz", "açıklama yapma", "Yanıt:", "Cevap:"
				};

				foreach (var pattern in removePatterns)
				{
					var index = cleaned.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
					if (index >= 0)
					{
						cleaned = cleaned.Substring(0, index).Trim();
					}
				}

				// Çift satır sonlarını temizle
				cleaned = cleaned.Replace("\n\n", "\n").Replace("\n", " ").Trim();

				// Çok kısa ise
				if (cleaned.Length < 15)
				{
					return "Mesajınız için teşekkür ederiz. Konunuzla ilgili detaylı bilgi için size geri dönüş yapacağız.";
				}

				// Çok uzun ise kısalt (max 250 karakter)
				if (cleaned.Length > 250)
				{
					var sentences = cleaned.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
					if (sentences.Length > 0)
					{
						var result = "";
						foreach (var sentence in sentences.Take(2))
						{
							if (result.Length + sentence.Length > 200) break;
							result += sentence.Trim() + ". ";
						}
						return result.Trim();
					}

					cleaned = cleaned.Substring(0, 200).Trim() + "...";
				}

				// İlk harfi büyük yap
				if (!string.IsNullOrEmpty(cleaned))
				{
					cleaned = char.ToUpper(cleaned[0]) + cleaned.Substring(1);
				}

				return cleaned;
			}
			catch (Exception ex)
			{
				return "Mesajınız için teşekkür ederiz. En kısa sürede size geri dönüş yapacağız.";
			}
		}
	}
}