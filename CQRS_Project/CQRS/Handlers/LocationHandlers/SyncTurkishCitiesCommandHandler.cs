using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.LocationCommands;
using CQRS_Project.Entities;
using CQRS_Project.Services;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class SyncTurkishCitiesCommandHandler
	{
		private readonly CqrsContext _context;
		private readonly LocationService _locationService;

		public SyncTurkishCitiesCommandHandler(CqrsContext context, LocationService locationService)
		{
			_context = context;
			_locationService = locationService;
		}

		public async Task Handle(SyncTurkishCitiesCommand command, CancellationToken cancellationToken)
		{
			var turkishCities = new List<string>
			{
				"İstanbul, Türkiye", "Ankara, Türkiye", "İzmir, Türkiye", "Bursa, Türkiye",
				"Antalya, Türkiye", "Adana, Türkiye", "Konya, Türkiye", "Şanlıurfa, Türkiye",
				"Gaziantep, Türkiye", "Kocaeli, Türkiye", "Mersin, Türkiye", "Diyarbakır, Türkiye",
				"Hatay, Türkiye", "Manisa, Türkiye", "Kayseri, Türkiye", "Samsun, Türkiye",
				"Balıkesir, Türkiye", "Kahramanmaraş, Türkiye", "Van, Türkiye", "Aydın, Türkiye",
				"Denizli, Türkiye", "Sakarya, Türkiye", "Muğla, Türkiye", "Eskişehir, Türkiye",
				"Tekirdağ, Türkiye", "Ordu, Türkiye", "Edirne, Türkiye", "Elazığ, Türkiye"
			};

			var addedCount = 0;

			foreach (var city in turkishCities)
			{
				try
				{
					var cityName = city.Split(',')[0].Trim();
					var existingLocation = await _context.Locations
						.FirstOrDefaultAsync(x => x.City.Contains(cityName), cancellationToken);

					if (existingLocation == null)
					{
						var (lat, lng, address) = await _locationService.FindPlaceFromTextAsync(city);

						var location = new Location
						{
							City = cityName,
							District = "",
							Address = address,
							Latitude = lat,
							Longitude = lng,
							IsActive = true
						};

						_context.Locations.Add(location);
						addedCount++;
					}

					// API rate limit için bekleme
					await Task.Delay(100, cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Şehir eklenirken hata: {city} - {ex.Message}");
				}
			}

			await _context.SaveChangesAsync(cancellationToken);
			Console.WriteLine($"{addedCount} şehir başarıyla eklendi.");
		}
	}
}
