using CQRS_Project.CQRS.Commands.BrandCommands;
using CQRS_Project.CQRS.Handlers.BrandHandlers;
using CQRS_Project.CQRS.Queries.BrandQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class BrandController : Controller
	{
		private readonly GetBrandByIdQueryHandler _getBrandByIdQueryHandler;
		private readonly GetBrandQueryHandler _getBrandQueryHandler;
		private readonly CreateBrandCommandHandler _createBrandCommandHandler;
		private readonly UpdateBrandCommandHandler _updateBrandCommandHandler;
		private readonly RemoveBrandCommandHandler _removeBrandCommandHandler;

		public BrandController(GetBrandByIdQueryHandler getBrandByIdQueryHandler, GetBrandQueryHandler getBrandQueryHandler, CreateBrandCommandHandler createBrandCommandHandler, UpdateBrandCommandHandler updateBrandCommandHandler, RemoveBrandCommandHandler removeBrandCommandHandler)
		{
			_getBrandByIdQueryHandler = getBrandByIdQueryHandler;
			_getBrandQueryHandler = getBrandQueryHandler;
			_createBrandCommandHandler = createBrandCommandHandler;
			_updateBrandCommandHandler = updateBrandCommandHandler;
			_removeBrandCommandHandler = removeBrandCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getBrandQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateBrand()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateBrand(CreateBrandCommand command)
		{
			await _createBrandCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteBrand(int id)
		{
			await _removeBrandCommandHandler.Handle(new RemoveBrandCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateBrand(int id)
		{
			var value = await _getBrandByIdQueryHandler.Handle(new GetBrandByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateBrand(UpdateBrandCommand command)
		{
			await _updateBrandCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
