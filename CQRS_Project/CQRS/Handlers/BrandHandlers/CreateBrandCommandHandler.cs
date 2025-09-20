using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.BrandCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.BrandHandlers
{
	public class CreateBrandCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateBrandCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateBrandCommand command)
		{
			_context.Brands.Add(new Brand
			{
				BrandName = command.BrandName,
			});
			await _context.SaveChangesAsync();
		}
	}
}
