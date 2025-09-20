namespace CQRS_Project.CQRS.Queries.ReviewQueries
{
	public class GetReviewByIdQuery
	{
		public int ReviewId { get; set; }

		public GetReviewByIdQuery(int reviewId)
		{
			ReviewId = reviewId;
		}
	}
}
