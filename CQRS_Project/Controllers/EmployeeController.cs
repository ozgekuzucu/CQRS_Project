using CQRS_Project.CQRS.Commands.EmployeeCommands;
using CQRS_Project.CQRS.Handlers.EmployeeHandlers;
using CQRS_Project.CQRS.Queries.EmployeeQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
		private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
		private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
		private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
		private readonly RemoveEmployeeCommandHandler _removeEmployeeCommandHandler;

		public EmployeeController(GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, GetEmployeeQueryHandler getEmployeeQueryHandler, CreateEmployeeCommandHandler createEmployeeCommandHandler, UpdateEmployeeCommandHandler updateEmployeeCommandHandler, RemoveEmployeeCommandHandler removeEmployeeCommandHandler)
		{
			_getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
			_getEmployeeQueryHandler = getEmployeeQueryHandler;
			_createEmployeeCommandHandler = createEmployeeCommandHandler;
			_updateEmployeeCommandHandler = updateEmployeeCommandHandler;
			_removeEmployeeCommandHandler = removeEmployeeCommandHandler;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getEmployeeQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateEmployee()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
		{
			await _createEmployeeCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			await _removeEmployeeCommandHandler.Handle(new RemoveEmployeeCommand(id));
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateEmployee(int id)
		{
			var value = await _getEmployeeByIdQueryHandler.Handle(new GetEmployeeByIdQuery(id));
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
		{
			await _updateEmployeeCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}
	}
}
