using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.EmployeeCommands;

namespace CQRS_Project.CQRS.Handlers.EmployeeHandlers
{
	public class RemoveEmployeeCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveEmployeeCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveEmployeeCommand command)
		{
			var values = await _context.Employees.FindAsync(command.EmployeeId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
