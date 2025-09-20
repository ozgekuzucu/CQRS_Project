using CQRS_Project.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.ViewComponents
{
	public class DefaultReservationViewComponent : ViewComponent
	{
		private readonly CqrsContext _context;

		public DefaultReservationViewComponent(CqrsContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			// Lokasyonları getir
			ViewBag.Locations = await _context.Locations
				.Where(l => l.IsActive)
				.Select(l => new { l.LocationId, Text = l.City + (l.District != null ? " - " + l.District : "") })
				.ToListAsync();

			// Kategorileri getir
			ViewBag.Categories = await _context.Categories.ToListAsync();

			return View();
		}
	}
}
