using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CategoryCommands;

namespace CQRS_Project.CQRS.Handlers.CategoryHandlers
{
	public class CreateCategoryCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateCategoryCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateCategoryCommand command)
		{
			_context.Categories.Add(new Entities.Category
			{
				CategoryName = command.CategoryName,
			});
			await _context.SaveChangesAsync();
		}
	}
}
