using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.ReviewCommands
{
	public class RemoveReviewCommand
	{
		public int ReviewId { get; set; }

		public RemoveReviewCommand(int reviewId)
		{
			ReviewId = reviewId;
		}
	}
}
