using CQRS_Project.CQRS.Handlers.AboutHandlers;
using CQRS_Project.CQRS.Handlers.EmployeeHandlers;
using CQRS_Project.CQRS.Handlers.ReviewHandlers;
using CQRS_Project.CQRS.Queries.AboutQueries;
using CQRS_Project.CQRS.Results.AboutResults;
using CQRS_Project.CQRS.Results.EmployeeResults;
using CQRS_Project.CQRS.Results.ReviewResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRS_Project.Controllers
{
	public class DefaultController : Controller
	{
		private readonly GetAboutByIdQueryHandler _getAboutQueryhandler;
		private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
		private readonly GetReviewQueryHandler _getReviewQueryHandler;

		public DefaultController(GetAboutByIdQueryHandler getAboutQueryhandler, GetEmployeeQueryHandler getEmployeeQueryHandler, GetReviewQueryHandler getReviewQueryHandler)
		{
			_getAboutQueryhandler = getAboutQueryhandler;
			_getEmployeeQueryHandler = getEmployeeQueryHandler;
			_getReviewQueryHandler = getReviewQueryHandler;
		}
		public IActionResult Index()
		{
			return View();
		}
		public PartialViewResult PartialHead()
		{
			return PartialView();
		}

		public PartialViewResult PartialTopbar()
		{
			return PartialView();
		}
		public PartialViewResult PartialNavbar()
		{
			return PartialView();
		}
		public PartialViewResult PartialFeatures()
		{
			return PartialView();
		}
		public async Task<PartialViewResult> PartialAbout()
		{
			return PartialView();
		}
		public async Task<GetAboutByIdQueryResult> GetAboutModel()
		{
			var value = await _getAboutQueryhandler.Handle(new GetAboutByIdQuery(6));
			return value;
		}

		public PartialViewResult PartialServices()
		{
			return PartialView();
		}
		public PartialViewResult PartialCarProcess()
		{
			return PartialView();
		}
		public PartialViewResult PartialBanner()
		{
			return PartialView();
		}
		public async Task<PartialViewResult> PartialTeam()
		{
			var values = await _getEmployeeQueryHandler.Handle();
			return PartialView(values);
		}

		public async Task<PartialViewResult> PartialTestimonial()
		{
			var values = await _getReviewQueryHandler.Handle(); 
			return PartialView(values);
		}

		public PartialViewResult PartialFooter()
		{
			return PartialView();
		}
		public PartialViewResult PartialScript()
		{
			return PartialView();
		}

	}
}
