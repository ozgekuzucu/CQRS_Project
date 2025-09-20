using CQRS_Project.CQRS.Commands.CategoryCommands;
using CQRS_Project.CQRS.Handlers.CategoryHandlers;
using CQRS_Project.CQRS.Queries.CategoryQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class CategoryController : Controller
	{
		private readonly GetCategoryByIdQueryHandler _getCategoryByIdQueryHandler;
		private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
		private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
		private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
		private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;

		public CategoryController(GetCategoryByIdQueryHandler getCategoryByIdQueryHandler, GetCategoryQueryHandler getCategoryQueryHandler, CreateCategoryCommandHandler createCategoryCommandHandler, UpdateCategoryCommandHandler updateCategoryCommandHandler, RemoveCategoryCommandHandler removeCategoryCommandHandler)
		{
			_getCategoryByIdQueryHandler = getCategoryByIdQueryHandler;
			_getCategoryQueryHandler = getCategoryQueryHandler;
			_createCategoryCommandHandler = createCategoryCommandHandler;
			_updateCategoryCommandHandler = updateCategoryCommandHandler;
			_removeCategoryCommandHandler = removeCategoryCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getCategoryQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
		{
			await _createCategoryCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteCategory(int id)
		{
			await _removeCategoryCommandHandler.Handle(new RemoveCategoryCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateCategory(int id)
		{
			var value = await _getCategoryByIdQueryHandler.Handle(new GetCategoryByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
		{
			await _updateCategoryCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
