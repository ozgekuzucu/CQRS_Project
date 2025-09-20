using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;
using CQRS_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class CreateReservationCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateReservationCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<int> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
		{
			var isCarAvailable = !await _context.Reservations
				.AnyAsync(r => r.CarId == command.CarId &&
							  r.Status != "Cancelled" &&
							  ((r.StartDate <= command.EndDate && r.EndDate >= command.StartDate)),
						 cancellationToken);

			if (!isCarAvailable)
			{
				throw new Exception("Seçilen araç bu tarih aralığında müsait değil.");
			}

			var reservation = new Reservation
			{
				CustomerId = command.CustomerId,
				CarId = command.CarId,
				StartDate = command.StartDate,
				EndDate = command.EndDate,
				PickUpLocationId = command.PickUpLocationId,
				DropOffLocationId = command.DropOffLocationId,
				Status = command.Status
			};

			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync(cancellationToken);

			return reservation.ReservationId;
		}
	}
}
