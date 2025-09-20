using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands;
using CQRS_Project.CQRS.Commands.CustomerCommands;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class RemoveCustomerCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveCustomerCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveCustomerCommand command)
		{
			var values = await _context.Customers.FindAsync(command.CustomerId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
