using CQRS_Project.Entities;
using CQRS_Project.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace CQRS_Project.Services
{
	public class CarRecommendationService : ICarRecommendationService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		private readonly ILogger<CarRecommendationService> _logger;

		public CarRecommendationService(
			HttpClient httpClient,
			IConfiguration configuration,
			ILogger<CarRecommendationService> logger)
		{
			_httpClient = httpClient;
			_httpClient.Timeout = TimeSpan.FromSeconds(30);
			_configuration = configuration;
			_logger = logger;
		}

		public async Task<string> GetCarRecommendationAsync(string userQuery, List<Car> availableCars)
		{
			try
			{
				_logger.LogInformation($"AI Öneri talebi: {userQuery}");

				var geminiResponse = await TryGeminiAPI(userQuery, availableCars);
				if (!string.IsNullOrEmpty(geminiResponse))
				{
					_logger.LogInformation("Gemini API'den başarılı yanıt alındı");
					return geminiResponse;
				}

				// Gemini başarısız olursa fallback
				_logger.LogWarning("Gemini API başarısız, fallback kullanılıyor");
				return GenerateFallbackRecommendation(userQuery, availableCars);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "AI recommendation error");
				return GenerateFallbackRecommendation(userQuery, availableCars);
			}
		}

		private async Task<string> TryGeminiAPI(string userQuery, List<Car> availableCars)
		{
			try
			{
				var apiKey = _configuration["Gemini:ApiKey"];
				if (string.IsNullOrEmpty(apiKey))
				{
					_logger.LogWarning("Gemini API key bulunamadı");
					return null;
				}

				// Araç bilgilerini hazırla
				var carsInfo = string.Join("\n", availableCars.Take(8)
					.Select(c => $"• {c.Brand?.BrandName} {c.Model} ({c.Category?.CategoryName}) - {c.PricePerDay}₺/gün"));

				// Detaylı prompt oluştur
				var prompt = $@"Sen bir araç kiralama uzmanısın. Müşterinin isteğine göre en uygun araçları öner.

Müşteri İsteği: ""{userQuery}""

Mevcut Araçlar:
{carsInfo}

Lütfen:
1. Müşterinin ihtiyacına en uygun 2-3 araç seç
2. Her araç için neden uygun olduğunu kısaca açıkla
3. Profesyonel ve yardımsever bir tonda yanıtla
4. Türkçe yanıtla

Format:
[Araç Adı] - [Fiyat] - [Neden uygun]";

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
						topP = 0.8,
						topK = 40,
						maxOutputTokens = 500
					}
				};

				var json = JsonSerializer.Serialize(requestBody);
				var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

				_logger.LogInformation("Gemini API'ye istek gönderiliyor...");

				var response = await _httpClient.PostAsync(
					$"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
					content);

				var responseContent = await response.Content.ReadAsStringAsync();
				_logger.LogInformation($"Gemini API yanıtı: Status={response.StatusCode}, Content Length={responseContent.Length}");

				if (response.IsSuccessStatusCode)
				{
					var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

					if (result.TryGetProperty("candidates", out var candidates) &&
						candidates.GetArrayLength() > 0)
					{
						var candidate = candidates[0];
						if (candidate.TryGetProperty("content", out var contentObj) &&
							contentObj.TryGetProperty("parts", out var parts) &&
							parts.GetArrayLength() > 0)
						{
							var text = parts[0].GetProperty("text").GetString();

							if (!string.IsNullOrWhiteSpace(text))
							{
								return FormatGeminiResponse(text);
							}
						}
					}

					// Hata durumunu kontrol et
					if (result.TryGetProperty("error", out var error))
					{
						_logger.LogWarning($"Gemini API hatası: {error}");
					}
				}
				else
				{
					_logger.LogWarning($"Gemini API HTTP hatası: {response.StatusCode} - {responseContent}");
				}

				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Gemini API çağrısında hata");
				return null;
			}
		}

		private string FormatGeminiResponse(string geminiText)
		{
			// Gemini'den gelen yanıtı HTML formatına çevir
			var formatted = geminiText
				.Replace("\n", "<br>")
				.Replace("✅", "<span class='text-success'>✅</span>")
				.Replace("🚗", "<span class='text-primary'>🚗</span>")
				.Replace("💰", "<span class='text-warning'>💰</span>");

			return $@"
				<div class='ai-response-plain'>
					<div class='d-flex align-items-center mb-3'>
						<i class='fas fa-robot text-primary me-2'></i>
						<h6 class='mb-0 text-primary'>AI Uzman Önerisi</h6>
					</div>
					{formatted}
					<div class='mt-3'>
						<small class='text-muted'>
							<i class='fas fa-sparkles me-1'></i>
							Bu öneriler Google Gemini AI tarafından özelleştirilmiştir
						</small>
					</div>
				</div>";
		}

		private string GenerateFallbackRecommendation(string userQuery, List<Car> availableCars)
		{
			if (!availableCars.Any())
			{
				return @"<div class='alert alert-warning'>
                            <i class='fas fa-exclamation-triangle me-2'></i>
                            Şu anda müsait araç bulunmamaktadır.
                        </div>";
			}

			// Akıllı fallback: soruda geçen anahtar kelimeler
			var queryLower = userQuery.ToLower();
			var recommendedCars = availableCars.AsQueryable();

			
			var selectedCars = recommendedCars.Take(5).ToList();

			var html = $@"
				<div class='fallback-recommendation'>
					<div class='d-flex align-items-center mb-3'>
						<i class='fas fa-list text-secondary me-2'></i>
						<h6 class='mb-0 text-secondary'>Araç Önerileri</h6>
					</div>
					<p class='text-muted mb-3'>AI servisi geçici olarak kullanılamıyor. İşte müsait araçlarımız:</p>
					<div class='row'>";

			foreach (var car in selectedCars)
			{
				html += $@"
					<div class='col-md-6 mb-2'>
						<div class='recommendation-item p-3'>
							<h6 class='mb-1'>{car.Brand?.BrandName} {car.Model}</h6>
							<small class='text-muted'>{car.Category?.CategoryName}</small>
							<div class='d-flex justify-content-between align-items-center mt-2'>
								<span class='badge bg-primary'>{car.PricePerDay:N0}₺/gün</span>
								<small class='text-success'>Müsait</small>
							</div>
						</div>
					</div>";
			}

			html += "</div></div>";
			return html;
		}
	}
}