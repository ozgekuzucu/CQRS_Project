using CQRS_Project.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class FuelPriceController : Controller
	{
		private readonly IFuelPriceService _fuelPriceService;

		public FuelPriceController(IFuelPriceService fuelPriceService)
		{
			_fuelPriceService = fuelPriceService;
		}

		[HttpGet]
		public async Task<IActionResult> GetWidget()
		{
			var result = await _fuelPriceService.GetTurkeyFuelPricesAsync();
			return Json(result);
		}

		public async Task<IActionResult> Index()
		{
			var result = await _fuelPriceService.GetTurkeyFuelPricesAsync();
			return View(result);
		}
	}
}