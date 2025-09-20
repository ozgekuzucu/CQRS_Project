using CQRS_Project.Context;
using Microsoft.EntityFrameworkCore;
using CQRS_Project.CQRS.Results.LocationResults;

namespace CQRS_Project.CQRS.Handlers.LocationHandlers
{
	public class GetTotalReservationQueryHandler
	{
		private readonly CqrsContext _context;
		public GetTotalReservationQueryHandler(CqrsContext context) => _context = context;

		public async Task<GetTotalReservationsQueryResult> Handle()
		{
			var count = await _context.Reservations.CountAsync();
			return new GetTotalReservationsQueryResult { Count = count };
		}
	}
}