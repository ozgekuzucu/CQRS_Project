using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.ReservationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ReservationHandlers
{
	public class GetReservationQueryHandler
	{
		private readonly CqrsContext _context;

		public GetReservationQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetReservationQueryResult>> Handle()
		{
			var values = await _context.Reservations.Include(x => x.Customer)
													.Include(x => x.Car)
													.Include(x => x.PickUpLocation)
													.Include(x => x.DropOffLocation)
													.ToListAsync();
			return values.Select(x => new GetReservationQueryResult
			{
				ReservationId = x.ReservationId,
				CustomerId = x.CustomerId,
				CarId = x.CarId,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
				PickUpLocationId = x.PickUpLocationId,
				DropOffLocationId = x.DropOffLocationId,
				Status = x.Status ?? "Beklemede",
				CustomerName = x.Customer != null ? x.Customer.FullName : "Bilinmiyor",
				CarModel = x.Car != null ? x.Car.Model : "Bilinmiyor",
				PickUpLocationName = x.PickUpLocation != null ? $"{x.PickUpLocation.City} / {x.PickUpLocation.District}" : "Bilinmiyor",
				DropOffLocationName = x.DropOffLocation != null ? $"{x.DropOffLocation.City} / {x.DropOffLocation.District}" : "Bilinmiyor"

			}).ToList();

		}
	}
}
