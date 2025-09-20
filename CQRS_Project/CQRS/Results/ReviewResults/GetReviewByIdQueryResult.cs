namespace CQRS_Project.CQRS.Results.ReviewResults
{
	public class GetReviewByIdQueryResult
	{
		public int ReviewId { get; set; }
		public string Comment { get; set; }
		public int Rating { get; set; }
		public int CarId { get; set; }
		public int CustomerId { get; set; }
	}
}
