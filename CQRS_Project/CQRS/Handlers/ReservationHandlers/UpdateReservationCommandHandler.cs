using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class UpdateReservationCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateReservationCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateReservationCommand command)
		{
			var values = await _context.Reservations.FindAsync(command.ReservationId);
			values.CustomerId = command.CustomerId;
			values.CarId = command.CarId;
			values.StartDate = command.StartDate;
			values.EndDate = command.EndDate;
			values.PickUpLocationId = command.PickUpLocationId;
			values.DropOffLocationId = command.DropOffLocationId;
			values.Status = command.Status;
			await _context.SaveChangesAsync();
		}
	}
}
