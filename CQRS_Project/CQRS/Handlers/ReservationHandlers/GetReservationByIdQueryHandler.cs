using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.ReservationQueries;
using CQRS_Project.CQRS.Results.ReservationResults;
using CQRS_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class GetReservationByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetReservationByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetReservationByIdQueryResult> Handle(GetReservationByIdQuery query)
		{
			var reservation = await _context.Reservations
				.Include(r => r.Customer)
				.Include(r => r.Car)
				.ThenInclude(c => c.Brand)
				.Include(r => r.PickUpLocation)
				.Include(r => r.DropOffLocation)
				.Where(r => r.ReservationId == query.ReservationId)
				.Select(r => new GetReservationByIdQueryResult
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
				.FirstOrDefaultAsync();

			return reservation;
		}
	}
}