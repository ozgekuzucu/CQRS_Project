using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.SliderResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.SliderHandlers
{
	public class GetSliderQueryHandler
	{
		private readonly CqrsContext _context;

		public GetSliderQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetSliderQueryResult>> Handle()
		{
			var values = await _context.Sliders.ToListAsync();
			return values.Select(x => new GetSliderQueryResult
			{
				SliderId = x.SliderId,
				Title = x.Title,
				Description = x.Description,
				ImageUrl = x.ImageUrl,
			}).ToList();
		}
	}
}