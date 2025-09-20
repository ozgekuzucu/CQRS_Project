using CQRS_Project.Models;
using CQRS_Project.Services.Abstract;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Globalization;
using CQRS_Project.Models;
namespace CQRS_Project.Services
{
	public class FuelPriceService : IFuelPriceService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		private readonly IMemoryCache _cache;

		public FuelPriceService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
		{
			_httpClient = httpClient;
			_configuration = configuration;
			_cache = cache;
		}

		public async Task<FuelPriceResponse> GetTurkeyFuelPricesAsync()
		{
			if (_cache.TryGetValue("TurkeyFuelPrices", out FuelPriceResponse cached))
			{
				Console.WriteLine("FuelPrice cache'den alındı ");
				return cached;
			}

			try
			{
				var apiKey = _configuration["RapidAPI:Key"];
				if (string.IsNullOrEmpty(apiKey))
					throw new Exception("RapidAPI key bulunamadı");

				// Tek seferde fuel fiyatları
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri("https://gas-price.p.rapidapi.com/europeanCountries"),
					Headers =
					{
						{ "x-rapidapi-key", apiKey },
						{ "x-rapidapi-host", "gas-price.p.rapidapi.com" }
					}
				};

				using var response = await _httpClient.SendAsync(request);
				response.EnsureSuccessStatusCode();

				var body = await response.Content.ReadAsStringAsync();
				var jsonData = JObject.Parse(body);
				var resultArray = jsonData["result"]?.ToArray();

				var turkey = resultArray?.FirstOrDefault(x => x["country"]?.ToString() == "Turkey");
				if (turkey == null)
					throw new Exception("Turkey verisi bulunamadı");

				decimal euroToTL = await GetEuroToTLRate();

				decimal ParseFuel(string? value)
				{
					if (string.IsNullOrEmpty(value) || value == "-" || value == "0,000")
						return 0;
					value = value.Replace(",", ".");
					decimal eur = decimal.Parse(value, CultureInfo.InvariantCulture);
					return Math.Round(eur * euroToTL, 2);
				}

				var responseObj = new FuelPriceResponse
				{
					Benzin = ParseFuel(turkey["gasoline"]?.ToString()),
					Motorin = ParseFuel(turkey["diesel"]?.ToString()),
					Lpg = ParseFuel(turkey["lpg"]?.ToString()),
					LastUpdate = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
				};

				Console.WriteLine($"Yakıt fiyatları alındı: B={responseObj.Benzin}, M={responseObj.Motorin}, L={responseObj.Lpg}");

				_cache.Set("TurkeyFuelPrices", responseObj, TimeSpan.FromMinutes(60));

				return responseObj;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"GetTurkeyFuelPricesAsync hatası: {ex.Message}");
				throw;
			}
		}

		private async Task<decimal> GetEuroToTLRate()
		{
			if (_cache.TryGetValue("EuroToTRY", out decimal cachedRate))
				return cachedRate;

			try
			{
				var apiKey = _configuration["RapidAPI:Key"];

				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri("https://exchange-rates7.p.rapidapi.com/convert?base=EUR&target=TRY"),
					Headers =
					{
						{ "x-rapidapi-key", apiKey },
						{ "x-rapidapi-host", "exchange-rates7.p.rapidapi.com" }
					}
				};

				using var response = await _httpClient.SendAsync(request);
				response.EnsureSuccessStatusCode();

				var body = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(body);

				decimal rate = json["result"]?.Value<decimal>() ??
							   json["conversion_result"]?.Value<decimal>() ??
							   json["rate"]?.Value<decimal>() ?? 0;

				if (rate <= 0)
					throw new Exception("Kur alınamadı");

				_cache.Set("EuroToTRY", rate, TimeSpan.FromMinutes(60));

				Console.WriteLine($"EUR/TRY kuru: {rate}");
				return rate;
			}
			catch
			{
				return 48.5m; 
			}
		}

		public async Task<FuelPriceResponse> GetCurrentFuelPriceAsync()
		{
			return await GetTurkeyFuelPricesAsync();
		}

		public Task<decimal> GetFuelPriceAsync(string fuelType)
		{
			throw new NotImplementedException();
		}
	}
}
