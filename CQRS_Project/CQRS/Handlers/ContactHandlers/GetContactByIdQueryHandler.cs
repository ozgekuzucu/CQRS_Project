using CQRS_Project.Context;
using CQRS_Project.CQRS.Queries.ContactQueries;
using CQRS_Project.CQRS.Results.ContactResults;

namespace CQRS_Project.CQRS.Handlers.ContactHandlers
{
	public class GetContactByIdQueryHandler
	{
		private readonly CqrsContext _context;

		public GetContactByIdQueryHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task<GetContactByIdQueryResult> Handle(GetContactByIdQuery query)
		{
			var values = await _context.Contacts.FindAsync(query.ContactId);
			return new GetContactByIdQueryResult
			{
				ContactId = values.ContactId,
				Name = values.Name,
				Email = values.Email,
				Phone = values.Phone,
				Subject = values.Subject,
				Message = values.Message,
				CreatedDate = values.CreatedDate,
				IsReplied = values.IsReplied,
				ReplyMessage = values.ReplyMessage,
			};
		}
	}
}
