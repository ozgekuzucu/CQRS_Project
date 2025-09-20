using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.LocationQueries;
using CQRS_Project.CQRS.Results.LocationResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class GetLocationByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetLocationByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery query)
		{
			var values = await _context.Locations.FindAsync(query.LocationId);
			return new GetLocationByIdQueryResult
			{
				LocationId = values.LocationId,
				City = values.City,
				District = values.District,
				Address = values.Address,
				IsActive = values.IsActive,
				Latitude = values.Latitude,
				Longitude = values.Longitude,
			};
		}
	}
}
