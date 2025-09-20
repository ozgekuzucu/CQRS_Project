namespace CQRS_Project.CQRS.Commands.LocationCommands
{
	public class CreateLocationCommand
	{
		public string City { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public bool IsActive { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
