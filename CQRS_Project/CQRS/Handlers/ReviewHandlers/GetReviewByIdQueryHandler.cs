using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.ReviewQueries;
using CQRS_Project.CQRS.Results.ReviewResults;

namespace CQRS_Project.CQRS.Handlers.ReviewHandlers
{
	public class GetReviewByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetReviewByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetReviewByIdQueryResult> Handle(GetReviewByIdQuery query)
		{
			var values = await _context.Reviews.FindAsync(query.ReviewId);
			return new GetReviewByIdQueryResult
			{
				ReviewId = values.ReviewId,
				Comment = values.Comment,
				Rating = values.Rating,
				CarId = values.CarId,
				CustomerId = values.CustomerId,
			};
		}
	}
}
