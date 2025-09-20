using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.BrandCommands;

namespace CQRS_Project.CQRS.Handlers.BrandHandlers
{
	public class UpdateBrandCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateBrandCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateBrandCommand command)
		{
			var values = await _context.Brands.FindAsync(command.BrandId);
			values.BrandName = command.BrandName;
			await _context.SaveChangesAsync();
		}
	}
}
