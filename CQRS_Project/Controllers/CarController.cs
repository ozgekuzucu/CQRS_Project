using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CarCommands;
using CQRS_Project.CQRS.Handlers.CarHandlers;
using CQRS_Project.CQRS.Queries.CarQueries;
using CQRS_Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.Controllers
{
	public class CarController : Controller
	{
		private readonly CqrsContext _context;
		private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
		private readonly GetCarQueryHandler _getCarQueryHandler;
		private readonly CreateCarCommandHandler _createCarCommandHandler;
		private readonly UpdateCarCommandHandler _updateCarCommandHandler;
		private readonly RemoveCarCommandHandler _removeCarCommandHandler;

		public CarController(GetCarByIdQueryHandler getCarByIdQueryHandler, GetCarQueryHandler getCarQueryHandler, CreateCarCommandHandler createCarCommandHandler, UpdateCarCommandHandler updateCarCommandHandler, RemoveCarCommandHandler removeCarCommandHandler, CqrsContext context)
		{
			_getCarByIdQueryHandler = getCarByIdQueryHandler;
			_getCarQueryHandler = getCarQueryHandler;
			_createCarCommandHandler = createCarCommandHandler;
			_updateCarCommandHandler = updateCarCommandHandler;
			_removeCarCommandHandler = removeCarCommandHandler;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getCarQueryHandler.Handle();
			return View(values);
		}
		public async Task<IActionResult> CarList()
		{
			var values = await _getCarQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateCar()
		{
			var brands = _context.Brands.Select(b => new { b.BrandId, b.BrandName }).ToList();

			var categories = _context.Categories.Select(c => new { c.CategoryId, c.CategoryName }).ToList();

			var fuelTypes = _context.Cars.Select(c => c.FuelType).Distinct().ToList();

			var transmissionTypes = _context.Cars.Select(c => c.Transmission).Distinct().ToList();

			ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName");
			ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
			ViewBag.FuelTypes = new SelectList(fuelTypes);
			ViewBag.TransmissionTypes = new SelectList(transmissionTypes);

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCar(CreateCarCommand command)
		{
			await _createCarCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteCar(int id)
		{
			await _removeCarCommandHandler.Handle(new RemoveCarCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateCar(int id)
		{
			var value = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));

			var brands = _context.Brands.Select(x => new { x.BrandId, x.BrandName }).ToList();
			ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName", value.BrandId);

			var categories = _context.Categories.Select(x => new { x.CategoryId, x.CategoryName }).ToList();
			ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", value.CategoryId);

			var fuelTypes = _context.Cars.Select(c => c.FuelType).Distinct().ToList();
			ViewBag.FuelTypes = new SelectList(fuelTypes);

			var transmissionTypes = _context.Cars.Select(c => c.Transmission).Distinct().ToList();
			ViewBag.TransmissionTypes = new SelectList(transmissionTypes);

			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
		{
			await _updateCarCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
