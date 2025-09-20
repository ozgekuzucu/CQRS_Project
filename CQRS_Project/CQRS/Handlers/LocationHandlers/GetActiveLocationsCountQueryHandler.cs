using CQRS_Project.Context;
using Microsoft.EntityFrameworkCore;
using CQRS_Project.CQRS.Results.LocationResults;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class GetActiveLocationsCountQueryHandler
	{
		private readonly CqrsContext _context;
		public GetActiveLocationsCountQueryHandler(CqrsContext context) => _context = context;

		public async Task<GetActiveLocationsCountQueryResult> Handle()
		{
			var count = await _context.Locations.CountAsync(x => x.IsActive);
			return new GetActiveLocationsCountQueryResult { Count = count };
		}
	}
}
