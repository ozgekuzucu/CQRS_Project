using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class RemoveReservationCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveReservationCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveReservationCommand command)
		{
			var valeus = await _context.Reservations.FindAsync(command.ReservationId);
			_context.Remove(valeus);
			await _context.SaveChangesAsync();
		}
	}
}
