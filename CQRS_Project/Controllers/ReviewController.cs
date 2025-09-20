using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReviewCommands;
using CQRS_Project.CQRS.Handlers.ReviewHandlers;
using CQRS_Project.CQRS.Queries.ReviewQueries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.Controllers
{
	public class ReviewController : Controller
	{
		private readonly CqrsContext _context;
		private readonly GetReviewByIdQueryHandler _getReviewByIdQueryHandler;
		private readonly GetReviewQueryHandler _getReviewQueryHandler;
		private readonly CreateReviewCommandHandler _createReviewCommandHandler;
		private readonly UpdateReviewCommandHandler _updateReviewCommandHandler;
		private readonly RemoveReviewCommandHandler _removeReviewCommandHandler;

		public ReviewController(GetReviewByIdQueryHandler getReviewByIdQueryHandler, GetReviewQueryHandler getReviewQueryHandler, CreateReviewCommandHandler createReviewCommandHandler, UpdateReviewCommandHandler updateReviewCommandHandler, RemoveReviewCommandHandler removeReviewCommandHandler, CqrsContext context)
		{
			_getReviewByIdQueryHandler = getReviewByIdQueryHandler;
			_getReviewQueryHandler = getReviewQueryHandler;
			_createReviewCommandHandler = createReviewCommandHandler;
			_updateReviewCommandHandler = updateReviewCommandHandler;
			_removeReviewCommandHandler = removeReviewCommandHandler;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getReviewQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateReview()
		{
			ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
			ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateReview(CreateReviewCommand command)
		{
			await _createReviewCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteReview(int id)
		{
			await _removeReviewCommandHandler.Handle(new RemoveReviewCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateReview(int id)
		{
			var value = await _getReviewByIdQueryHandler.Handle(new GetReviewByIdQuery(id));
			ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
			ViewBag.Cars = new SelectList(_context.Cars, "CarId", "Model");
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
		{
			await _updateReviewCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
