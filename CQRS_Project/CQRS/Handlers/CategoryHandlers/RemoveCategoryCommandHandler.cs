using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CategoryCommands;

namespace CQRS_Project.CQRS.Handlers.CategoryHandlers
{
	public class RemoveCategoryCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveCategoryCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveCategoryCommand command)
		{
			var values = await _context.Categories.FindAsync(command.CategoryId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
