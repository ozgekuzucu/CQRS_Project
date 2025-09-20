using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.EmployeeResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.EmployeeHandlers
{
	public class GetEmployeeQueryHandler
	{
		private readonly CqrsContext _context;

		public GetEmployeeQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetEmployeeQueryResult>> Handle()
		{
			var values = await _context.Employees.ToListAsync();
			return values.Select(x => new GetEmployeeQueryResult
			{
				EmployeeId = x.EmployeeId,	
				EmployeeName = x.EmployeeName,
				Title = x.Title,
				ImageUrl = x.ImageUrl,
				SocialMedia1 = x.SocialMedia1,
				SocialMedia2 = x.SocialMedia2,
				SocialMedia3 = x.SocialMedia3,
				SocialMedia4 = x.SocialMedia4,
			}).ToList();
		}
	}
}
