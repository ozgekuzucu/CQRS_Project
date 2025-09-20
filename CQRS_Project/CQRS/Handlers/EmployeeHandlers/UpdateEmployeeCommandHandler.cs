using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.EmployeeCommands;

namespace CQRS_Project.CQRS.Handlers.EmployeeHandlers
{
	public class UpdateEmployeeCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateEmployeeCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateEmployeeCommand command)
		{
			var values = await _context.Employees.FindAsync(command.EmployeeId);
			values.EmployeeName = command.EmployeeName;
			values.Title = command.Title;
			values.ImageUrl = command.ImageUrl;
			values.SocialMedia1 = command.SocialMedia1;
			values.SocialMedia2 = command.SocialMedia2;
			values.SocialMedia3 = command.SocialMedia3;
			values.SocialMedia4 = command.SocialMedia4;
			await _context.SaveChangesAsync();
		}
	}
}
