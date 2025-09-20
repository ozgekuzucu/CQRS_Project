using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.LocationCommands;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class RemoveLocationCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveLocationCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveLocationCommand command)
		{
			var values = await _context.Locations.FindAsync(command.LocationId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
