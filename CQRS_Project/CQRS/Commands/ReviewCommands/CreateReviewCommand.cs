using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.ReviewCommands
{
	public class CreateReviewCommand
	{
		public string Comment { get; set; }
		public int Rating { get; set; }
		public int CarId { get; set; }
		public int CustomerId { get; set; }
	}
}
