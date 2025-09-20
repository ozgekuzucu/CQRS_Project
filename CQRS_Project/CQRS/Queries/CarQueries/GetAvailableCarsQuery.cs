namespace CQRS_Project.CQRS.Queries.CarQueries
{
	public class GetAvailableCarsQuery
	{
		public int PickUpLocationId { get; set; }
		public int DropOffLocationId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? CategoryId { get; set; } 
	}
}
