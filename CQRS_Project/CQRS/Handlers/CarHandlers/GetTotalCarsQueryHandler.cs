using CQRS_Project.Context;
using Microsoft.EntityFrameworkCore;
using CQRS_Project.CQRS.Results.CarResults;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class GetTotalCarsQueryHandler
	{
		private readonly CqrsContext _context;
		public GetTotalCarsQueryHandler(CqrsContext context) => _context = context;

		public async Task<GetTotalCarsQueryResult> Handle()
		{
			var count = await _context.Cars.CountAsync();
			return new GetTotalCarsQueryResult { Count = count };
		}
	}
}
