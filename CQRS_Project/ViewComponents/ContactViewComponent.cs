using CQRS_Project.CQRS.Commands.ContactCommands;
using CQRS_Project.CQRS.Handlers.ContactHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.ViewComponents
{
	public class ContactViewComponent : ViewComponent
	{
		private readonly CreateContactCommandHandler _createContactCommandHandler;

		public ContactViewComponent(CreateContactCommandHandler createContactCommandHandler)
		{
			_createContactCommandHandler = createContactCommandHandler;
		}

		public IViewComponentResult Invoke()
		{
			return View(new CreateContactCommand());
		}
	}
}
