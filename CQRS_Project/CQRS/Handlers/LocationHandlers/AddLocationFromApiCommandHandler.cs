using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.LocationCommands;
using CQRS_Project.Entities;
using CQRS_Project.Services;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class AddLocationFromApiCommandHandler
	{
		private readonly CqrsContext _context;
		private readonly LocationService _locationService;

		public AddLocationFromApiCommandHandler(CqrsContext context, LocationService locationService)
		{
			_context = context;
			_locationService = locationService;
		}

		public async Task<int> Handle(AddLocationFromApiCommand command, CancellationToken cancellationToken)
		{
			try
			{
				// API'den konum bilgisini al
				var (lat, lng, address) = await _locationService.FindPlaceFromTextAsync(command.SearchInput);

				// Aynı lokasyon var mı kontrol et
				var existingLocation = await _context.Locations
					.FirstOrDefaultAsync(x => x.Latitude == lat && x.Longitude == lng, cancellationToken);

				if (existingLocation != null)
				{
					return existingLocation.LocationId;
				}

				// Adres parçalarını ayır
				var addressParts = address.Split(',');
				var city = addressParts.Length > 1 ? addressParts[addressParts.Length - 2].Trim() : command.SearchInput;
				var district = addressParts.Length > 2 ? addressParts[0].Trim() : "";

				// Yeni lokasyon oluştur
				var location = new Location
				{
					City = city,
					District = district,
					Address = address,
					Latitude = lat,
					Longitude = lng,
					IsActive = true
				};

				_context.Locations.Add(location);
				await _context.SaveChangesAsync(cancellationToken);

				return location.LocationId;
			}
			catch (Exception ex)
			{
				throw new Exception($"Lokasyon eklenirken hata oluştu: {ex.Message}");
			}
		}
	}
}
