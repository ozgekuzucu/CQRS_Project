using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReviewCommands;

namespace CQRS_Project.CQRS.Handlers.ReviewHandlers
{
	public class RemoveReviewCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveReviewCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveReviewCommand command)
		{
			var values = await _context.Reviews.FindAsync(command.ReviewId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
