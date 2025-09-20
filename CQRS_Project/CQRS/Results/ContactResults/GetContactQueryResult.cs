namespace CQRS_Project.CQRS.Results.ContactResults
{
	public class GetContactQueryResult
	{
		public int ContactId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Subject { get; set; }
		public string? Message { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsReplied { get; set; }
		public string? ReplyMessage { get; set; }
	}
}
