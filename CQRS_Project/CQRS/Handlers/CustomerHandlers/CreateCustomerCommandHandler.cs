using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CustomerCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class CreateCustomerCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateCustomerCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateCustomerCommand command)
		{
			_context.Customers.Add(new Customer
			{
				FullName = command.FullName,
				Email = command.Email,
				PasswordHash = command.PasswordHash,
				Phone = command.Phone,
				DrivingLicenseNo = command.DrivingLicenseNo,
				CreatedDate = DateTime.Now,
			});
			await _context.SaveChangesAsync();
		}
	}
}
