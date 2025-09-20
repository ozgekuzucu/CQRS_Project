using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class AdminLayoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
