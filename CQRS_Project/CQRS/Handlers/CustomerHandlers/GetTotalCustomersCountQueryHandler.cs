using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.CustomerResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class GetTotalCustomersCountQueryHandler
	{
		private readonly CqrsContext _context;

		public GetTotalCustomersCountQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetTotalCustomersCountQueryResult> Handle()
		{
			var count = await _context.Reservations
				.Select(r => r.CustomerId)
				.Distinct()
				.CountAsync();

			return new GetTotalCustomersCountQueryResult { Count = count };
		}
	}
}
