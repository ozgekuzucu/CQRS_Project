using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.CustomerResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class GetCustomerQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCustomerQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetCustomerQueryResult>> Handle()
		{
			var values = await _context.Customers.ToListAsync();
			return values.Select(x => new GetCustomerQueryResult
			{
				CustomerId = x.CustomerId,
				FullName = x.FullName,
				Email = x.Email,
				PasswordHash = x.PasswordHash,
				Phone = x.Phone,
				DrivingLicenseNo = x.DrivingLicenseNo,
				CreatedDate = x.CreatedDate,
			}).ToList();
		}
	}
}
