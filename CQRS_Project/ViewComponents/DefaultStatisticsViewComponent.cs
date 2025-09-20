using CQRS_Project.CQRS.Handlers.CarHandlers;
using CQRS_Project.CQRS.Handlers.CustomerHandlers;
using CQRS_Project.CQRS.Handlers.LocationHandlers;
using CQRS_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.ViewComponents
{
	public class DefaultStatisticsViewComponent : ViewComponent
	{
		private readonly GetTotalCustomersCountQueryHandler _customers;
		private readonly GetTotalCarsQueryHandler _cars;
		private readonly GetActiveLocationsCountQueryHandler _activeLocations;
		private readonly GetTotalReservationQueryHandler _reservations;

		public DefaultStatisticsViewComponent(
			GetTotalCarsQueryHandler cars,
			GetActiveLocationsCountQueryHandler activeLocations,
			GetTotalReservationQueryHandler reservations,
			GetTotalCustomersCountQueryHandler customers)
		{
			_cars = cars;
			_activeLocations = activeLocations;
			_reservations = reservations;
			_customers = customers;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var vm = new DefaultStatisticsViewModel
			{
				TotalCustomers = (await _customers.Handle()).Count,
				NumberOfCars = (await _cars.Handle()).Count,
				CarCenters = (await _activeLocations.Handle()).Count,
				TotalReservations = (await _reservations.Handle()).Count
			};

			return View(vm);
		}
	}
}