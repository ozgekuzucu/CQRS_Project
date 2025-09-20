using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.ReservationCommands
{
	public class RemoveReservationCommand
	{
		public int ReservationId { get; set; }

		public RemoveReservationCommand(int reservationId)
		{
			ReservationId = reservationId;
		}
	}
}
