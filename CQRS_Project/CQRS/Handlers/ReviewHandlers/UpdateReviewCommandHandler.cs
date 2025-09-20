using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;
using CQRS_Project.CQRS.Commands.ReviewCommands;

namespace CQRS_Project.CQRS.Handlers.ReviewHandlers
{
	public class UpdateReviewCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateReviewCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateReviewCommand command)
		{
			var values = await _context.Reviews.FindAsync(command.ReviewId);
			values.Comment = command.Comment;
			values.Rating = command.Rating;
			values.CarId = command.CarId;
			values.CustomerId=command.CustomerId;
			await _context.SaveChangesAsync();
		}
	}
}
