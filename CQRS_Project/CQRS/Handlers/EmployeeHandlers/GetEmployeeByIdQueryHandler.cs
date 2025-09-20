using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.EmployeeQueries;
using CQRS_Project.CQRS.Results.EmployeeResults;

namespace CQRS_Project.CQRS.Handlers.EmployeeHandlers
{
	public class GetEmployeeByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetEmployeeByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetEmployeeByIdQueryResult> Handle(GetEmployeeByIdQuery query)
		{
			var values = await _context.Employees.FindAsync(query.EmployeeId);
			return new GetEmployeeByIdQueryResult
			{
				EmployeeId = values.EmployeeId,
				EmployeeName = values.EmployeeName,
				Title = values.Title,
				ImageUrl = values.ImageUrl,
				SocialMedia1 = values.SocialMedia1,
				SocialMedia2 = values.SocialMedia2,
				SocialMedia3 = values.SocialMedia3,
				SocialMedia4 = values.SocialMedia4,
			};
		}
	}
}
