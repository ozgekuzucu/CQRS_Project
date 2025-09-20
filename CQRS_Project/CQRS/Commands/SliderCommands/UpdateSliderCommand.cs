namespace CQRS_Project.CQRS.Commands.SliderCommands
{
	public class UpdateSliderCommand
	{
		public int SliderId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
	}
}
