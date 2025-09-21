using CQRS_Project.Context;
using Newtonsoft.Json;

namespace CQRS_Project.Services
{
	public class LocationService
	{
		private readonly HttpClient _httpClient;

		public LocationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "");
			_httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "google-map-places.p.rapidapi.com");
		}

		public async Task<(double lat, double lng, string address)> FindPlaceFromTextAsync(string input)
		{
			var url = $"https://google-map-places.p.rapidapi.com/maps/api/place/findplacefromtext/json?input={Uri.EscapeDataString(input)}&inputtype=textquery&fields=formatted_address,name,geometry";

			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			dynamic obj = JsonConvert.DeserializeObject(json);

			var result = obj.candidates[0];

			double lat = result.geometry.location.lat;
			double lng = result.geometry.location.lng;
			string address = result.formatted_address;

			return (lat, lng, address);
		}
	}
}
