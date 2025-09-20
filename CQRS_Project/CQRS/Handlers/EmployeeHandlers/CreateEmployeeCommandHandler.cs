using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.EmployeeCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.EmployeeHandlers
{
	public class CreateEmployeeCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateEmployeeCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateEmployeeCommand command)
		{
			_context.Employees.Add(new Employee
			{
				EmployeeName = command.EmployeeName,
				Title = command.Title,
				ImageUrl = command.ImageUrl,
				SocialMedia1 = command.SocialMedia1,
				SocialMedia2 = command.SocialMedia2,
				SocialMedia3 = command.SocialMedia3,
				SocialMedia4 = command.SocialMedia4,
			});
			await _context.SaveChangesAsync();
		}
	}
}
