using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CustomerCommands;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace CQRS_Project.CQRS.Handlers.CustomerHandlers
{
	public class UpdateCustomerCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateCustomerCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateCustomerCommand command)
		{
			var values = await _context.Customers.FindAsync(command.CustomerId);
			values.FullName = command.FullName;
			values.Email = command.Email;
			values.PasswordHash = command.PasswordHash;
			values.Phone = command.Phone;
			values.DrivingLicenseNo = command.DrivingLicenseNo;
			await _context.SaveChangesAsync();
		}
	}
}
