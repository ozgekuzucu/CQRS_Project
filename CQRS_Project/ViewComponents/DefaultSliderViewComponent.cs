using CQRS_Project.CQRS.Handlers.SliderHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.ViewComponents
{
	public class DefaultSliderViewComponent : ViewComponent
	{
		private readonly GetSliderQueryHandler _sliderHandler;

		public DefaultSliderViewComponent(GetSliderQueryHandler sliderHandler)
		{
			_sliderHandler = sliderHandler;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var sliders = await _sliderHandler.Handle();
			return View(sliders);
		}
	}
}
