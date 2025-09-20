using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.CategoryQueries;
using CQRS_Project.CQRS.Results.CategoryResults;

namespace CQRS_Project.CQRS.Handlers.CategoryHandlers
{
	public class GetCategoryByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCategoryByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery query)
		{
			var values = await _context.Categories.FindAsync(query.CategoryId);
			return new GetCategoryByIdQueryResult
			{
				CategoryId = values.CategoryId,
				CategoryName = values.CategoryName
			};
		}
	}
}
