namespace CQRS_Project.Entities
{
	public class Review
	{
		public int ReviewId { get; set; }
		public string Comment { get; set; }
		public int Rating { get; set; }
		public int CarId { get; set; }
		public int CustomerId { get; set; }

		public Car Car { get; set; }
		public Customer Customer { get; set; }
	}
}
