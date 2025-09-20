using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.CustomerQueries;
using CQRS_Project.CQRS.Results.CustomerResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class GetCustomerByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetCustomerByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetCustomerByIdQueryResult> Handle(GetCustomerByIdQuery query)
		{
			var values = await _context.Customers.FindAsync(query.CustomerId);
			return new GetCustomerByIdQueryResult
			{
				CustomerId = values.CustomerId,
				FullName = values.FullName,
				Email = values.Email,
				PasswordHash = values.PasswordHash,
				Phone = values.Phone,
				DrivingLicenseNo = values.DrivingLicenseNo,
				CreatedDate = values.CreatedDate,
			};
		}
	}
}
