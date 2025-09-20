using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.CarCommands
{
	public class RemoveCarCommand
	{
		public int CarId { get; set; }

		public RemoveCarCommand(int carId)
		{
			CarId = carId;
		}
	}
}
