using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.BrandQueries;
using CQRS_Project.CQRS.Results.BrandResults;
using Microsoft.AspNetCore.Identity;

namespace CQRS_Project.CQRS.Handlers.BrandHandlers
{
	public class GetBrandByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetBrandByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetBrandByIdQueryResult> Handle(GetBrandByIdQuery query) 
		{
			var values = await _context.Brands.FindAsync(query.BrandId);
			return new GetBrandByIdQueryResult
			{ 
				BrandId = values.BrandId,
				BrandName = values.BrandName
			};

		}
	}
}
