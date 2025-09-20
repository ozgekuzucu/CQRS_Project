using CQRS_Project.CQRS.Commands.CustomerCommands;
using CQRS_Project.CQRS.Handlers.CustomerHandlers;
using CQRS_Project.CQRS.Queries.CustomerQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class CustomerController : Controller
	{
		private readonly GetCustomerByIdQueryHandler _getCustomerByIdQueryHandler;
		private readonly GetCustomerQueryHandler _getCustomerQueryHandler;
		private readonly CreateCustomerCommandHandler _createCustomerCommandHandler;
		private readonly UpdateCustomerCommandHandler _updateCustomerCommandHandler;
		private readonly RemoveCustomerCommandHandler _removeCustomerCommandHandler;

		public CustomerController(GetCustomerByIdQueryHandler getCustomerByIdQueryHandler, GetCustomerQueryHandler getCustomerQueryHandler, CreateCustomerCommandHandler createCustomerCommandHandler, UpdateCustomerCommandHandler updateCustomerCommandHandler, RemoveCustomerCommandHandler removeCustomerCommandHandler)
		{
			_getCustomerByIdQueryHandler = getCustomerByIdQueryHandler;
			_getCustomerQueryHandler = getCustomerQueryHandler;
			_createCustomerCommandHandler = createCustomerCommandHandler;
			_updateCustomerCommandHandler = updateCustomerCommandHandler;
			_removeCustomerCommandHandler = removeCustomerCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getCustomerQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateCustomer()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
		{
			await _createCustomerCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			await _removeCustomerCommandHandler.Handle(new RemoveCustomerCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateCustomer(int id)
		{
			var value = await _getCustomerByIdQueryHandler.Handle(new GetCustomerByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command)
		{
			await _updateCustomerCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
