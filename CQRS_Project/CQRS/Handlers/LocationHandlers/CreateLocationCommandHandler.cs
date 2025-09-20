using Azure.Core;
using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.LocationCommands;
using CQRS_Project.Entities;
using CQRS_Project.Services;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class CreateLocationCommandHandler
	{
		private readonly CqrsContext _contect;
		private readonly LocationService _locationService;

		public CreateLocationCommandHandler(CqrsContext contect, LocationService locationService)
		{
			_contect = contect;
			_locationService = locationService;
		}
		public async Task Handle(CreateLocationCommand command,CancellationToken cancellationToken)
		{
			var (lat, lng, address) = await _locationService.FindPlaceFromTextAsync(command.City);

			_contect.Locations.Add(new Location
			{
				City = command.City,
				District = command.District,
				Address = address,
				IsActive = true,
				Latitude = lat,
				Longitude = lng,
			});
			await _contect.SaveChangesAsync(cancellationToken);
		}

		internal async Task Handle(CreateLocationCommand command)
		{
			throw new NotImplementedException();
		}
	}
}
