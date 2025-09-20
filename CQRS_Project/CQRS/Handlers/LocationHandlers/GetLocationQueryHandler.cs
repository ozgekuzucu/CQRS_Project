using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.LocationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class GetLocationQueryHandler
	{
		private readonly CqrsContext _context;

		public GetLocationQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<List<GetLocationQueryResult>> Handle()
		{
			var values = await _context.Locations.ToListAsync();
			return values.Select(x => new GetLocationQueryResult
			{
				LocationId = x.LocationId,
				City = x.City,
				District = x.District,
				Address = x.Address,
				IsActive = x.IsActive,
				Latitude = x.Latitude,
				Longitude = x.Longitude,

			}).ToList();
		}
	}
}
