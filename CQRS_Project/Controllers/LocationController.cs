// 1. Updated Controller - Mevcut yapınıza yeni özellikler eklendi
using CQRS_Project.CQRS.Commands.LocationCommands;
using CQRS_Project.CQRS.Handlers.LocationHandlers;
using CQRS_Project.CQRS.Queries.LocationQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class LocationController : Controller
	{
		private readonly GetLocationByIdQueryHandler _getLocationByIdQueryHandler;
		private readonly GetLocationQueryHandler _getLocationQueryHandler;
		private readonly CreateLocationCommandHandler _createLocationCommandHandler;
		private readonly UpdateLocationCommandHandler _updateLocationCommandHandler;
		private readonly RemoveLocationCommandHandler _removeLocationCommandHandler;

		private readonly SearchLocationQueryHandler _searchLocationQueryHandler;
		private readonly SyncTurkishCitiesCommandHandler _syncTurkishCitiesCommandHandler;
		private readonly AddLocationFromApiCommandHandler _addLocationFromApiCommandHandler;

		public LocationController(
			GetLocationByIdQueryHandler getLocationByIdQueryHandler,
			GetLocationQueryHandler getLocationQueryHandler,
			CreateLocationCommandHandler createLocationCommandHandler,
			UpdateLocationCommandHandler updateLocationCommandHandler,
			RemoveLocationCommandHandler removeLocationCommandHandler,
			SearchLocationQueryHandler searchLocationQueryHandler,
			SyncTurkishCitiesCommandHandler syncTurkishCitiesCommandHandler,
			AddLocationFromApiCommandHandler addLocationFromApiCommandHandler)
		{
			_getLocationByIdQueryHandler = getLocationByIdQueryHandler;
			_getLocationQueryHandler = getLocationQueryHandler;
			_createLocationCommandHandler = createLocationCommandHandler;
			_updateLocationCommandHandler = updateLocationCommandHandler;
			_removeLocationCommandHandler = removeLocationCommandHandler;
			_searchLocationQueryHandler = searchLocationQueryHandler;
			_syncTurkishCitiesCommandHandler = syncTurkishCitiesCommandHandler;
			_addLocationFromApiCommandHandler = addLocationFromApiCommandHandler;
		}
		 
		public async Task<IActionResult> Index()
		{
			var values = await _getLocationQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateLocation()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateLocation(CreateLocationCommand command)
		{
			await _createLocationCommandHandler.Handle(command, CancellationToken.None);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> DeleteLocation(int id)
		{
			await _removeLocationCommandHandler.Handle(new RemoveLocationCommand(id));
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateLocation(int id)
		{
			var value = await _getLocationByIdQueryHandler.Handle(new GetLocationByIdQuery(id));
			return View(value);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateLocation(UpdateLocationCommand command)
		{
			await _updateLocationCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<JsonResult> SearchLocations(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
				return Json(new List<object>());

			var result = await _searchLocationQueryHandler.Handle(new SearchLocationQuery { SearchTerm = searchTerm });


			var formattedResult = result.Select(x => new
			{
				id = x.LocationId,
				text = $"{x.City} - {x.District}",
				city = x.City,
				district = x.District,
				address = x.Address,
				latitude = x.Latitude,
				longitude = x.Longitude
			});

			return Json(formattedResult);
		}

		[HttpPost]
		public async Task<IActionResult> SyncCities()
		{
			try
			{
				await _syncTurkishCitiesCommandHandler.Handle(new SyncTurkishCitiesCommand(), CancellationToken.None);
				TempData["SuccessMessage"] = "Türk şehirleri başarıyla senkronize edildi.";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Hata oluştu: {ex.Message}";
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult AddFromApi()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddFromApi(AddLocationFromApiCommand command)
		{
			try
			{
				var locationId = await _addLocationFromApiCommandHandler.Handle(command, CancellationToken.None);
				TempData["SuccessMessage"] = $"Lokasyon başarıyla eklendi. ID: {locationId}";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Hata oluştu: {ex.Message}";
				return View(command);
			}
		}

		[HttpGet]
		public IActionResult CreateLocationWithApi()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateLocationWithApi(CreateLocationCommand command)
		{
			await _createLocationCommandHandler.Handle(command, CancellationToken.None);
			TempData["SuccessMessage"] = "Lokasyon API'den koordinatlarla birlikte eklendi.";
			return RedirectToAction("Index");
		}
	}
}
