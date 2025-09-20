using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ContactCommands;

namespace CQRS_Project.CQRS.Handlers.ContactHandlers
{
	public class RemoveContactCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveContactCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveContactCommand command)
		{
			var values = await _context.Contacts.FindAsync(command.ContactId);
			_context.Remove(values);
			await _context.SaveChangesAsync();	
		}
	}
}
