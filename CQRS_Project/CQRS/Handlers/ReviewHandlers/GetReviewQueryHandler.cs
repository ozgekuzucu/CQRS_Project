using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.ReviewResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ReviewHandlers
{
	public class GetReviewQueryHandler
	{
		private readonly CqrsContext _context;

		public GetReviewQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetReviewQueryResult>> Handle()
		{
			var values = await _context.Reviews
				.Include(r => r.Car)       
				.Include(r => r.Customer) 
				.ToListAsync();

			return values.Select(x => new GetReviewQueryResult
			{
				ReviewId = x.ReviewId,
				Comment = x.Comment ?? "",
				Rating = x.Rating,
				CarId = x.CarId,
				CustomerId = x.CustomerId,
				CarName = x.Car?.Model ?? "",         
				CustomerName = x.Customer?.FullName ?? "" ,
				ImageUrl = x.Customer.DrivingLicenseNo,
				
			}).ToList();
		}
	}
}
