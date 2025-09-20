using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.CarQueries;
using CQRS_Project.CQRS.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class GetAvailableCarsQueryHandler
	{
		private readonly CqrsContext _context;

		public GetAvailableCarsQueryHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task<List<GetAvailableCarsQueryResult>> Handle(GetAvailableCarsQuery query)
		{
			try
			{
				var reservedCarIds = await _context.Reservations
					.Where(r =>
						(query.StartDate < r.EndDate && query.EndDate > r.StartDate) &&
						r.Status != "Cancelled" && r.Status != "Completed")
					.Select(r => r.CarId)
					.ToListAsync();

				var availableCarsQuery = _context.Cars
					.Include(c => c.Brand)
					.Include(c => c.Category)
					.Where(c => c.IsAvailable && !reservedCarIds.Contains(c.CarId));

				if (query.CategoryId.HasValue && query.CategoryId > 0)
				{
					availableCarsQuery = availableCarsQuery.Where(c => c.CategoryId == query.CategoryId.Value);
				}

				var availableCars = await availableCarsQuery
					.OrderBy(c => c.PricePerDay)
					.Select(c => new GetAvailableCarsQueryResult
					{
						CarId = c.CarId,
						Brand = c.Brand.BrandName,
						Model = c.Model,
						ModelYear = c.ModelYear,
						PricePerDay = c.PricePerDay,
						FuelType = c.FuelType,
						Transmission = c.Transmission,
						SeatCount = c.SeatCount,
						Category = c.Category.CategoryName,
						ImageUrl = c.ImageUrl ?? "/images/default-car.jpg",
						Stars = c.Stars,
						IsAvailable = c.IsAvailable
					})
					.ToListAsync();

				return availableCars;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetAvailableCarsQueryHandler: {ex.Message}");
				throw;
			}
		}
	}
}
