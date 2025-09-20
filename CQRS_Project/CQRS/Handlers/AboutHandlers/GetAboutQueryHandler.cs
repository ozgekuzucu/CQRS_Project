using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.AboutResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.AboutHandlers
{
	public class GetAboutQueryHandler
	{
		private readonly CqrsContext _context;

		public GetAboutQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetAboutQueryResult>> Handle()
		{
			var values = await _context.Abouts.ToListAsync();
			return values.Select(x => new GetAboutQueryResult
			{
				AboutId = x.AboutId,
				Title = x.Title,
				Description = x.Description,
				SubDescription = x.SubDescription,
				ImageUrl = x.ImageUrl,
				ImageUrl2 = x.ImageUrl2,
				Mission = x.Mission,
				Vision = x.Vision
			}).ToList();
		}
	}
}
