namespace CQRS_Project.CQRS.Results.ReservationResults
{
	public class ReservationSummaryResult
	{
		public decimal TotalPrice { get; set; }
		public decimal FuelCost { get; set; }
		public double EstimatedDistance { get; set; }
		public int TotalDays { get; set; }
		public string PickUpLocationName { get; set; }
		public string DropOffLocationName { get; set; }
	}
}
