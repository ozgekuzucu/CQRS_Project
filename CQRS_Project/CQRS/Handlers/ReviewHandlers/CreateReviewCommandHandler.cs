using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ReservationCommands;
using CQRS_Project.CQRS.Commands.ReviewCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.ReviewHandlers
{
	public class CreateReviewCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateReviewCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateReviewCommand command)
		{
			_context.Reviews.Add(new Review
			{
				Comment=command.Comment,
				Rating=command.Rating,
				CarId= command.CarId,
				CustomerId=command.CustomerId,
			});
			await _context.SaveChangesAsync();
		}
	}
}
