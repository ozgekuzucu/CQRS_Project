namespace CQRS_Project.CQRS.Commands.EmployeeCommands
{
	public class UpdateEmployeeCommand
	{
		public int EmployeeId { get; set; }
		public string EmployeeName { get; set; }
		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public string SocialMedia1 { get; set; }
		public string SocialMedia2 { get; set; }
		public string SocialMedia3 { get; set; }
		public string SocialMedia4 { get; set; }
	}
}
