using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;
using CQRS_Project.CQRS.Handlers.CarHandlers;
using CQRS_Project.CQRS.Handlers.ReservationHandlers;
using CQRS_Project.CQRS.Queries.CarQueries;
using CQRS_Project.CQRS.Queries.ReservationQueries;
using CQRS_Project.Services.Abstract;
using CQRS_Project.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.Controllers
{
	public class ReservationController : Controller
	{
		private readonly CqrsContext _context;
		private readonly GetReservationByIdQueryHandler _getReservationByIdQueryHandler;
		private readonly GetReservationQueryHandler _getReservationQueryHandler;
		private readonly CreateReservationCommandHandler _createReservationCommandHandler;
		private readonly UpdateReservationCommandHandler _updateReservationCommandHandler;
		private readonly RemoveReservationCommandHandler _removeReservationCommandHandler;
		private readonly GetAvailableCarsQueryHandler _getAvailableCarsHandler;
		private readonly IFuelPriceService _fuelPriceService;

		public ReservationController(
			GetReservationByIdQueryHandler getReservationByIdQueryHandler,
			GetReservationQueryHandler getReservationQueryHandler,
			CreateReservationCommandHandler createReservationCommandHandler,
			UpdateReservationCommandHandler updateReservationCommandHandler,
			RemoveReservationCommandHandler removeReservationCommandHandler,
			GetAvailableCarsQueryHandler getAvailableCarsHandler,
			CqrsContext context,
			IFuelPriceService fuelPriceService)
		{
			_getReservationByIdQueryHandler = getReservationByIdQueryHandler;
			_getReservationQueryHandler = getReservationQueryHandler;
			_createReservationCommandHandler = createReservationCommandHandler;
			_updateReservationCommandHandler = updateReservationCommandHandler;
			_removeReservationCommandHandler = removeReservationCommandHandler;
			_getAvailableCarsHandler = getAvailableCarsHandler;
			_context = context;
			_fuelPriceService = fuelPriceService;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getReservationQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateReservation()
		{
			ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
			ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
			ViewBag.Locations = new SelectList(_context.Locations, "LocationId", "City");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
		{
			await _createReservationCommandHandler.Handle(command, CancellationToken.None);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> DeleteReservation(int id)
		{
			await _removeReservationCommandHandler.Handle(new RemoveReservationCommand(id));
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateReservation(int id)
		{
			var value = await _getReservationByIdQueryHandler.Handle(new GetReservationByIdQuery(id));
			ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
			ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
			ViewBag.Locations = new SelectList(_context.Locations, "LocationId", "City");
			return View(value);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateReservation(UpdateReservationCommand command)
		{
			await _updateReservationCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> BookNow()
		{
			ViewBag.Locations = new SelectList(
				await _context.Locations.Where(l => l.IsActive)
					.Select(l => new { l.LocationId, Text = l.City + " - " + l.District })
					.ToListAsync(),
				"LocationId", "Text");

			ViewBag.Categories = new SelectList(
				await _context.Categories.ToListAsync(),
				"CategoryId", "CategoryName");

			return View();
		}

		[HttpPost]
		public async Task<JsonResult> GetAvailableCars([FromBody] GetAvailableCarsQuery query)
		{
			try
			{
				Console.WriteLine($"GetAvailableCars çağrıldı:");
				Console.WriteLine($"PickUpLocationId: {query.PickUpLocationId}");
				Console.WriteLine($"DropOffLocationId: {query.DropOffLocationId}");
				Console.WriteLine($"StartDate: {query.StartDate}");
				Console.WriteLine($"EndDate: {query.EndDate}");
				Console.WriteLine($"CategoryId: {query.CategoryId}");

				// Tarih formatını düzelt
				if (query.StartDate.Kind == DateTimeKind.Unspecified)
				{
					query.StartDate = DateTime.SpecifyKind(query.StartDate, DateTimeKind.Local);
				}
				if (query.EndDate.Kind == DateTimeKind.Unspecified)
				{
					query.EndDate = DateTime.SpecifyKind(query.EndDate, DateTimeKind.Local);
				}

				var availableCars = await _getAvailableCarsHandler.Handle(query);
				Console.WriteLine($"Handler'dan {availableCars.Count} araç döndü");

				var totalDays = (query.EndDate - query.StartDate).Days;
				if (totalDays <= 0) totalDays = 1;

				var result = availableCars.Select(c => new
				{
					carId = c.CarId,
					brand = c.Brand,
					model = c.Model,
					category = c.Category,
					pricePerDay = c.PricePerDay,
					totalPrice = c.PricePerDay * totalDays,
					totalDays = totalDays,
					imageUrl = c.ImageUrl ?? "/images/default-car.jpg",
					year = c.ModelYear,
					fuelType = c.FuelType,
					transmission = c.Transmission,
					seatCount = c.SeatCount
				}).ToList();

				return Json(new
				{
					success = true,
					data = result,
					message = $"{result.Count} adet müsait araç bulundu."
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"GetAvailableCars hatası: {ex.Message}");
				return Json(new
				{
					success = false,
					message = $"Hata oluştu: {ex.Message}"
				});
			}
		}

		[HttpPost]
		public async Task<IActionResult> CalculateFuelCost([FromBody] FuelCalculationRequest request)
		{
			try
			{
				Console.WriteLine($"CalculateFuelCost çağrıldı:");
				Console.WriteLine($"PickUpLocationId: {request.PickUpLocationId}");
				Console.WriteLine($"DropOffLocationId: {request.DropOffLocationId}");
				Console.WriteLine($"CarId: {request.CarId}");

				var pickUpLocation = await _context.Locations.FindAsync(request.PickUpLocationId);
				var dropOffLocation = await _context.Locations.FindAsync(request.DropOffLocationId);

				if (pickUpLocation == null || dropOffLocation == null)
				{
					return Json(new { success = false, message = "Lokasyon bulunamadı." });
				}

				double distanceKm = GetDistance(
					pickUpLocation.Latitude, pickUpLocation.Longitude,
					dropOffLocation.Latitude, dropOffLocation.Longitude
				);

				if (distanceKm < 5)
					distanceKm = 100; 

				// Yakıt türünü belirle
				string fuelType = "Benzin"; 
				if (request.CarId > 0)
				{
					var car = await _context.Cars.FindAsync(request.CarId);
					if (car != null && !string.IsNullOrEmpty(car.FuelType))
					{
						fuelType = car.FuelType;
					}
				}

				Console.WriteLine($"Yakıt türü: {fuelType}");

				// API'den fiyat al
				decimal fuelPricePerLiter = await _fuelPriceService.GetFuelPriceAsync(fuelType);
				Console.WriteLine($"Yakıt fiyatı: {fuelPricePerLiter} TL/L");

				// Yakıt tüketimi hesapla
				double fuelConsumptionPer100Km = fuelType.ToLower().Contains("elektrik") ? 15.0 : 7.0;
				double estimatedFuelLiters = (distanceKm / 100) * fuelConsumptionPer100Km;
				decimal estimatedFuelCost = Math.Round((decimal)estimatedFuelLiters * fuelPricePerLiter, 2);

				Console.WriteLine($"Hesaplanan maliyet: {estimatedFuelCost} TL");

				var result = new
				{
					success = true,
					data = new
					{
						distanceKm = Math.Round(distanceKm, 1),
						estimatedFuelLiters = Math.Round(estimatedFuelLiters, 1),
						estimatedFuelCost = estimatedFuelCost,
						pickUpLocationName = $"{pickUpLocation.City} - {pickUpLocation.District}",
						dropOffLocationName = $"{dropOffLocation.City} - {dropOffLocation.District}",
						fuelType = fuelType,
						fuelPrice = fuelPricePerLiter
					}
				};

				return Json(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"CalculateFuelCost error: {ex.Message}");
				return Json(new { success = false, message = "Hesaplama sırasında hata oluştu." });
			}
		}

		[HttpPost]
		public async Task<JsonResult> CreateReservationFromFrontend([FromBody] CreateReservationCommand command)
		{
			try
			{
				Console.WriteLine($"CreateReservationFromFrontend çağrıldı:");
				Console.WriteLine($"CarId: {command.CarId}");
				Console.WriteLine($"PickUpLocationId: {command.PickUpLocationId}");
				Console.WriteLine($"DropOffLocationId: {command.DropOffLocationId}");

				var carExists = await _context.Cars.AnyAsync(c => c.CarId == command.CarId);
				var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == 6);
				var pickupExists = await _context.Locations.AnyAsync(l => l.LocationId == command.PickUpLocationId);
				var dropoffExists = await _context.Locations.AnyAsync(l => l.LocationId == command.DropOffLocationId);

				if (!carExists) return Json(new { success = false, message = "Car not found" });
				if (!customerExists) return Json(new { success = false, message = "Customer not found" });
				if (!pickupExists) return Json(new { success = false, message = "PickUp Location not found" });
				if (!dropoffExists) return Json(new { success = false, message = "DropOff Location not found" });

				command.CustomerId = 6;
				command.Status = "Pending";

				var reservationId = await _createReservationCommandHandler.Handle(command, CancellationToken.None);

				return Json(new { success = true, reservationId = reservationId, message = "Rezervasyonunuz başarıyla oluşturuldu!" });
			}
			catch (Exception ex)
			{
				Console.WriteLine($"CreateReservationFromFrontend error: {ex.Message}");
				return Json(new
				{
					success = false,
					message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var reservation = await _getReservationByIdQueryHandler.Handle(new GetReservationByIdQuery(id));
			if (reservation == null)
				return NotFound("Rezervasyon bulunamadı.");

			var car = await _context.Cars
				.Include(c => c.Brand)
				.FirstOrDefaultAsync(c => c.CarId == reservation.CarId);

			var pickUp = await _context.Locations.FindAsync(reservation.PickUpLocationId);
			var dropOff = await _context.Locations.FindAsync(reservation.DropOffLocationId);

			// Mesafe hesapla
			double distanceKm = GetDistance(pickUp.Latitude, pickUp.Longitude, dropOff.Latitude, dropOff.Longitude);
			if (distanceKm < 5) distanceKm = 100;

			// Yakıt fiyatı al
			decimal fuelPricePerLiter = await _fuelPriceService.GetFuelPriceAsync(car.FuelType);

			// Hesaplamalar
			double fuelConsumptionPer100Km = car.FuelType.ToLower().Contains("elektrik") ? 15.0 : 7.0;
			double estimatedFuelLiters = (distanceKm / 100) * fuelConsumptionPer100Km;
			decimal estimatedFuelCost = Math.Round((decimal)estimatedFuelLiters * fuelPricePerLiter, 2);

			var vm = new ReservationSummaryViewModel
			{
				Brand = car.Brand.BrandName,
				Model = car.Model,
				FuelType = car.FuelType,
				PricePerDay = car.PricePerDay,
				TotalDays = (reservation.EndDate - reservation.StartDate).Days,
				TotalPrice = car.PricePerDay * (reservation.EndDate - reservation.StartDate).Days,
				StartDate = reservation.StartDate,
				EndDate = reservation.EndDate,
				StartTime = reservation.StartDate.ToString("HH:mm"),
				EndTime = reservation.EndDate.ToString("HH:mm"),
				PickUpText = pickUp.City,
				DropOffText = dropOff.City,
				DistanceKm = distanceKm,
				EstimatedFuelLiters = estimatedFuelLiters,
				EstimatedFuelCost = estimatedFuelCost
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateStatus(int id, string status)
		{
			try
			{
				var reservation = await _context.Reservations.FindAsync(id);
				if (reservation != null)
				{
					reservation.Status = status;
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Rezervasyon durumu başarıyla güncellendi.";
				}
				else
				{
					TempData["ErrorMessage"] = "Rezervasyon bulunamadı.";
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Hata oluştu: {ex.Message}";
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> MyReservations(int customerId = 6)
		{
			try
			{
				var reservations = await _context.Reservations
					.Include(r => r.Customer)
					.Include(r => r.Car)
					.ThenInclude(c => c.Brand)
					.Include(r => r.PickUpLocation)
					.Include(r => r.DropOffLocation)
					.Where(r => r.CustomerId == customerId)
					.OrderByDescending(r => r.ReservationId)
					.ToListAsync();

				return View(reservations);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return View(new List<Entities.Reservation>());
			}
		}

		private double GetDistance(double lat1, double lon1, double lat2, double lon2)
		{
			if (lat1 == 0 || lon1 == 0 || lat2 == 0 || lon2 == 0) return 0;
			double R = 6371; // km
			double dLat = (lat2 - lat1) * (Math.PI / 180);
			double dLon = (lon2 - lon1) * (Math.PI / 180);
			double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					   Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
					   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return R * c;
		}

		public class FuelCalculationRequest
		{
			public int PickUpLocationId { get; set; }
			public int DropOffLocationId { get; set; }
			public int CarId { get; set; }
		}
	}
}