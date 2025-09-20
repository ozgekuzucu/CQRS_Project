using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.CategoryResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CategoryHandlers
{
	public class GetCategoryQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCategoryQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetCategoryQueryResult>> Handle()
		{
			var values = await _context.Categories.ToListAsync();
			return values.Select(x => new GetCategoryQueryResult
			{
				CategoryId = x.CategoryId,
				CategoryName = x.CategoryName,
			}).ToList();
		}
	}
}
