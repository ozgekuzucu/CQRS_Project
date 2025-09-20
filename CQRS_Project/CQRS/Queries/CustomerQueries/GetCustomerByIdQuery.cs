namespace CQRS_Project.CQRS.Queries.CustomerQueries
{
	public class GetCustomerByIdQuery
	{
		public int CustomerId { get; set; }
		public GetCustomerByIdQuery(int customerId)
		{
			CustomerId = customerId;
		}
	}
}
