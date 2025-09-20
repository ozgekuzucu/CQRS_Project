using CQRS_Project.CQRS.Commands.SliderCommands;
using CQRS_Project.CQRS.Handlers.SliderHandlers;
using CQRS_Project.CQRS.Queries.SliderQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class SliderController : Controller
	{
		private readonly GetSliderByIdQueryHandler _getSliderByIdQueryHandler;
		private readonly GetSliderQueryHandler _getSliderQueryHandler;
		private readonly CreateSliderCommandHandler _createSliderCommandHandler;
		private readonly UpdateSliderCommandHandler _updateSliderCommandHandler;
		private readonly RemoveSliderCommandHandler _removeSliderCommandHandler;

		public SliderController(GetSliderByIdQueryHandler getSliderByIdQueryHandler, GetSliderQueryHandler getSliderQueryHandler, CreateSliderCommandHandler createSliderCommandHandler, UpdateSliderCommandHandler updateSliderCommandHandler, RemoveSliderCommandHandler removeSliderCommandHandler)
		{
			_getSliderByIdQueryHandler = getSliderByIdQueryHandler;
			_getSliderQueryHandler = getSliderQueryHandler;
			_createSliderCommandHandler = createSliderCommandHandler;
			_updateSliderCommandHandler = updateSliderCommandHandler;
			_removeSliderCommandHandler = removeSliderCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getSliderQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateSlider()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateSlider(CreateSliderCommand command)
		{
			await _createSliderCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteSlider(int id)
		{
			await _removeSliderCommandHandler.Handle(new RemoveSliderCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateSlider(int id)
		{
			var value = await _getSliderByIdQueryHandler.Handle(new GetSliderByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateSlider(UpdateSliderCommand command)
		{
			await _updateSliderCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
