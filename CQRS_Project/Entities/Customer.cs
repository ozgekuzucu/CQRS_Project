namespace CQRS_Project.Entities
{
	public class Customer
	{
		public int CustomerId { get; set; }          
		public string FullName { get; set; }         
		public string Email { get; set; }           
		public string PasswordHash { get; set; }     
		public string Phone { get; set; }           
		public string DrivingLicenseNo { get; set; } 
		public DateTime CreatedDate { get; set; }     
	}
}
