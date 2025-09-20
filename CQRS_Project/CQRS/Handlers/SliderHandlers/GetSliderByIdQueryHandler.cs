using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.SliderQueries;
using CQRS_Project.CQRS.Results.SliderResults;

namespace CQRS_Project.CQRS.Handlers.SliderHandlers
{
	public class GetSliderByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetSliderByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetSliderByIdQueryResult> Handle(GetSliderByIdQuery query)
		{
			var values = await _context.Sliders.FindAsync(query.SliderId);
			return new GetSliderByIdQueryResult
			{
				SliderId = values.SliderId,
				Title = values.Title,
				Description = values.Description,
				ImageUrl = values.ImageUrl,
			};
		}
	}
}
