using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.ViewComponents
{
	public class AvailableCarsViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var model = new AvailableCarsViewModel
			{
				Cars = new List<AvailableCarItem>(),
				IsSearchCompleted = false,
				HasResults = false,
				SearchCriteria = null
			};

			return View(model);
		}
	}

	public class AvailableCarsViewModel
	{
		public List<AvailableCarItem> Cars { get; set; } = new();
		public bool IsSearchCompleted { get; set; }
		public bool HasResults { get; set; }
		public SearchCriteriaModel? SearchCriteria { get; set; }
	}

	public class AvailableCarItem
	{
		public int CarId { get; set; }
		public string Brand { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public decimal DailyPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public int TotalDays { get; set; }
		public string? ImageUrl { get; set; }
		public string Year { get; set; }
		public string? FuelType { get; set; }
		public string? Transmission { get; set; }
		public int SeatCount { get; set; }
		public string? PlateNumber { get; set; }
	}

	public class SearchCriteriaModel
	{
		public int PickUpLocationId { get; set; }
		public int DropOffLocationId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string StartTime { get; set; } = "12:00";
		public string EndTime { get; set; } = "12:00";
		public int? CategoryId { get; set; }
		public string PickUpLocationText { get; set; } = string.Empty;
		public string DropOffLocationText { get; set; } = string.Empty;
		public int TotalDays { get; set; }
	}
}