using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CarCommands;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class RemoveCarCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveCarCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveCarCommand command)
		{
			var values = await _context.Cars.FindAsync(command.CarId);
			_context.Remove(values);
			await _context.SaveChangesAsync();	
		}
	}
}
