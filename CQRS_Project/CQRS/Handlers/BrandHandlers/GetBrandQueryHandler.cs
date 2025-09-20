using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.BrandResults;
using CQRS_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.BrandHandlers
{
	public class GetBrandQueryHandler
	{
		private readonly CqrsContext _context;

		public GetBrandQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetBrandQueryResult>> Handle()
		{
			var values = await _context.Brands.ToListAsync();
			return values.Select(x => new GetBrandQueryResult
			{
				BrandId = x.BrandId,
				BrandName = x.BrandName
			}).ToList();
		}
	}
}
