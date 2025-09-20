namespace CQRS_Project.Entities
{
	public class Reservation
	{
		public int ReservationId { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int CarId { get; set; }
		public Car Car { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		// Lokasyon ilişkileri
		public int PickUpLocationId { get; set; }   
		public Location PickUpLocation { get; set; }

		public int DropOffLocationId { get; set; }   
		public Location DropOffLocation { get; set; }

		public string Status { get; set; }       
	}

}
