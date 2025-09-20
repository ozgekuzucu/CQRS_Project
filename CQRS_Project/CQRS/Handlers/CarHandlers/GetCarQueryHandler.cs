using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class GetCarQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCarQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetCarQueryResult>> Handle()
		{
			var values = await _context.Cars.Include(c => c.Brand)
											.Include(c => c.Category)
											.ToListAsync();
			return values.Select(x => new GetCarQueryResult
			{
				CarId = x.CarId,
				Model = x.Model,
				ImageUrl = x.ImageUrl,
				PricePerDay = x.PricePerDay,
				IsAvailable = x.IsAvailable,
				BrandName = x.Brand.BrandName,
				CategoryName = x.Category.CategoryName,
				Transmission = x.Transmission,
				FuelType = x.FuelType,
				Stars = x.Stars,
				SeatCount = x.SeatCount,
				ModelYear = x.ModelYear,
			}).ToList();
		}
	}
}
