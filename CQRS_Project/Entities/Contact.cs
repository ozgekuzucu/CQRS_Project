namespace CQRS_Project.Entities
{
	public class Contact
	{
		public int ContactId { get; set; }   
		public string Name { get; set; }            
		public string Email { get; set; }          
		public string Phone { get; set; }          
		public string Subject { get; set; }         
		public string Message { get; set; }      
		public DateTime CreatedDate { get; set; }   
		public bool IsReplied { get; set; }         
		public string ReplyMessage { get; set; }    
	}

}
