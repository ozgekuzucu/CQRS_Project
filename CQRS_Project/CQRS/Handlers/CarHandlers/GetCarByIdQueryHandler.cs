using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.CarQueries;
using CQRS_Project.CQRS.Results.CarResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class GetCarByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCarByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
		{
			var values = await _context.Cars.Include(c => c.Brand).Include(c => c.Category).FirstOrDefaultAsync(c => c.CarId == query.CarId);

			return new GetCarByIdQueryResult
			{
				CarId = values.CarId,
				Brand = values.Brand.BrandName,
				Model = values.Model,
				Category = values.Category.CategoryName,
				PricePerDay = values.PricePerDay,
				ImageUrl = values.ImageUrl,
				IsAvailable = true,
				SeatCount = values.SeatCount,
				FuelType = values.FuelType,
				ModelYear = values.ModelYear,
				Transmission = values.Transmission,
				Stars = values.Stars,
			};
		}
	}
}
