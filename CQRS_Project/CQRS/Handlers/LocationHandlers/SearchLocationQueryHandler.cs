using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.LocationQueries;
using CQRS_Project.CQRS.Results.LocationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class SearchLocationQueryHandler
	{
		private readonly CqrsContext _context;

		public SearchLocationQueryHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task<List<SearchLocationQueryResult>> Handle(SearchLocationQuery query)
		{
			var values = await _context.Locations
				.Where(x => x.IsActive &&
						   (x.City.Contains(query.SearchTerm) ||
							x.District.Contains(query.SearchTerm) ||
							x.Address.Contains(query.SearchTerm)))
				.Take(10)
				.ToListAsync();

			return values.Select(x => new SearchLocationQueryResult
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
