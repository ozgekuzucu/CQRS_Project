using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Results.ReviewResults
{
	public class GetReviewQueryResult
	{
		public int ReviewId { get; set; }
		public string Comment { get; set; }
		public int Rating { get; set; }
		public int CarId { get; set; }
		public int CustomerId { get; set; }
		public string CarName { get; set; }
		public string CustomerName { get; set; }
		public string ImageUrl { get; set; }
	}
}
