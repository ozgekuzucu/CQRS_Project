using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.ReservationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class GetTotalReservationsQueryHandler
	{
		private readonly CqrsContext _context;

		public GetTotalReservationsQueryHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task<List<GetReservationQueryResult>> Handle()
		{
			var reservations = await _context.Reservations
				.Include(r => r.Customer)
				.Include(r => r.Car)
				.ThenInclude(c => c.Brand)
				.Include(r => r.PickUpLocation)
				.Include(r => r.DropOffLocation)
				.Select(r => new GetReservationQueryResult
				{
					ReservationId = r.ReservationId,
					CustomerId = r.CustomerId,
					CustomerName = r.Customer.FullName,
					CarId = r.CarId,
					CarBrand = r.Car.Brand.BrandName,
					CarModel = r.Car.Model,
					StartDate = r.StartDate,
					EndDate = r.EndDate,
					PickUpLocationId = r.PickUpLocationId,
					PickUpLocationName = r.PickUpLocation.City + " - " + r.PickUpLocation.District,
					DropOffLocationId = r.DropOffLocationId,
					DropOffLocationName = r.DropOffLocation.City + " - " + r.DropOffLocation.District,
					Status = r.Status,
					TotalPrice = r.Car.PricePerDay * (decimal)(r.EndDate - r.StartDate).TotalDays,
					TotalDays = (int)(r.EndDate - r.StartDate).TotalDays
				})
				.OrderByDescending(r => r.ReservationId)
				.ToListAsync();

			return reservations;
		}
	}
}
