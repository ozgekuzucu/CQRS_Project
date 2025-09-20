using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.ReservationCommands
{
	public class UpdateReservationCommand
	{
		public int ReservationId { get; set; }
		public int CustomerId { get; set; }
		public int CarId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int PickUpLocationId { get; set; }
		public int DropOffLocationId { get; set; }
		public string Status { get; set; }
	}
}
