using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ContactCommands;
using System.Numerics;

namespace CQRS_Project.CQRS.Handlers.ContactHandlers
{
	public class UpdateContactCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateContactCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateContactCommand command)
		{
			var values = await _context.Contacts.FindAsync(command.ContactId);
			values.Name = command.Name;
			values.Email = command.Email;
			values.Phone = command.Phone;
			values.Subject = command.Subject;
			values.Message = command.Message;
			values.CreatedDate = command.CreatedDate;
			values.IsReplied = command.IsReplied;
			values.ReplyMessage = command.ReplyMessage;
			await _context.SaveChangesAsync();
		}
	}
}
