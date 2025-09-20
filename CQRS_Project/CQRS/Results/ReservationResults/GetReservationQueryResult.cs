namespace CQRS_Project.CQRS.Results.ReservationResults
{
	public class GetReservationQueryResult
	{
		public int ReservationId { get; set; }
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
		public int CarId { get; set; }
		public string CarBrand { get; set; }
		public string CarModel { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int PickUpLocationId { get; set; }
		public string PickUpLocationName { get; set; }
		public int DropOffLocationId { get; set; }
		public string DropOffLocationName { get; set; }
		public string Status { get; set; }
		public decimal TotalPrice { get; set; }
		public int TotalDays { get; set; }
	}
}
