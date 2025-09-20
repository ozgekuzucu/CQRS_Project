using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.ContactResults;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.ContactHandlers
{
	public class GetContactQueryHandler
	{
		private readonly CqrsContext _context;

		public GetContactQueryHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task<List<GetContactQueryResult>> Handle()
		{
			var values = await _context.Contacts
				.Select(x => new GetContactQueryResult
				{
					ContactId = x.ContactId,
					Name = x.Name ?? "",
					Email = x.Email ?? "",
					Phone = x.Phone ?? "",
					Subject = x.Subject ?? "",
					Message = x.Message ?? "",
					CreatedDate = x.CreatedDate,
					IsReplied = x.IsReplied,
					ReplyMessage = x.ReplyMessage ?? ""
				})
				.ToListAsync();

			return values;
		}
	}
}
