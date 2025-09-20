using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.AboutQueries;
using CQRS_Project.CQRS.Results.AboutResults;
using CQRS_Project.CQRS.Results.CategoryResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS_Project.CQRS.Handlers.AboutHandlers
{
	public class GetAboutByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetAboutByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery query)
		{
			var values = await _context.Abouts.FindAsync(query.AboutId);
			return new GetAboutByIdQueryResult
			{
				AboutId =values.AboutId,
				Title = values.Title,
				Description = values.Description,
				SubDescription = values.SubDescription,
				ImageUrl = values.ImageUrl,
				ImageUrl2 = values.ImageUrl2,
				Mission = values.Mission,
				Vision = values.Vision
			};
		}
	}
}
