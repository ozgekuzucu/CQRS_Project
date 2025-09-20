using CQRS_Project.CQRS.Commands.AboutCommands;
using CQRS_Project.CQRS.Handlers.AboutHandlers;
using CQRS_Project.CQRS.Queries.AboutQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class AboutController : Controller
	{
		private readonly GetAboutByIdQueryHandler _getAboutByIdQueryHandler;
		private readonly GetAboutQueryHandler _getAboutQueryHandler;
		private readonly CreateAboutCommandHandler _createAboutCommandHandler;
		private readonly UpdateAboutCommandHandler _updateAboutCommandHandler;
		private readonly RemoveAboutCommandHandler _removeAboutCommandHandler;

		public AboutController(GetAboutByIdQueryHandler getAboutByIdQueryHandler, GetAboutQueryHandler getAboutQueryHandler, CreateAboutCommandHandler createAboutCommandHandler, UpdateAboutCommandHandler updateAboutCommandHandler, RemoveAboutCommandHandler removeAboutCommandHandler)
		{
			_getAboutByIdQueryHandler = getAboutByIdQueryHandler;
			_getAboutQueryHandler = getAboutQueryHandler;
			_createAboutCommandHandler = createAboutCommandHandler;
			_updateAboutCommandHandler = updateAboutCommandHandler;
			_removeAboutCommandHandler = removeAboutCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getAboutQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateAbout()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAbout(CreateAboutCommand command)
		{
			await _createAboutCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteAbout(int id)
		{
			await _removeAboutCommandHandler.Handle(new RemoveAboutCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateAbout(int id)
		{
			var value = await _getAboutByIdQueryHandler.Handle(new GetAboutByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAbout(UpdateAboutCommand command)
		{
			await _updateAboutCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
