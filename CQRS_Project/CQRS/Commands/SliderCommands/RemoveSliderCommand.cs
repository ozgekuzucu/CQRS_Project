namespace CQRS_Project.CQRS.Commands.SliderCommands
{
	public class RemoveSliderCommand
	{
		public int SliderId { get; set; }

		public RemoveSliderCommand(int sliderId)
		{
			SliderId = sliderId;
		}
	}
}
