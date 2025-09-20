using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.BrandCommands;

namespace CQRS_Project.CQRS.Handlers.BrandHandlers
{
	public class RemoveBrandCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveBrandCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveBrandCommand command)
		{
			var values = await _context.Brands.FindAsync(command.BrandId);
			_context.Brands.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
