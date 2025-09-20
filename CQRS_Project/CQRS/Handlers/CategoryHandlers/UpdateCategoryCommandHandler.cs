using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CategoryCommands;

namespace CQRS_Project.CQRS.Handlers.CategoryHandlers
{
	public class UpdateCategoryCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateCategoryCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateCategoryCommand command)
		{
			var values = await _context.Categories.FindAsync(command.CategoryId);
			values.CategoryName = command.CategoryName;
			await _context.SaveChangesAsync();
		}
	}
}
