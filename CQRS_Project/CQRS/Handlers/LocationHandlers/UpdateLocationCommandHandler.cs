using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.LocationCommands;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class UpdateLocationCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateLocationCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateLocationCommand command)
		{
			var values = await _context.Locations.FindAsync(command.LocationId);
			values.City = command.City;
			values.District = command.District;
			values.Address = command.Address;
			values.IsActive = command.IsActive;
			values.Latitude = command.Latitude;
			values.Longitude = command.Longitude;
			await _context.SaveChangesAsync();
		}
	}
}
