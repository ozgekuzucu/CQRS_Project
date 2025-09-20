namespace CQRS_Project.CQRS.Commands.ContactCommands
{
	public class CreateContactCommand
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsReplied { get; set; }
		public string ReplyMessage { get; set; } = "";
	}
}
