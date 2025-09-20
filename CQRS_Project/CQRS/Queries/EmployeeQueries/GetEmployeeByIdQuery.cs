namespace CQRS_Project.CQRS.Queries.EmployeeQueries
{
	public class GetEmployeeByIdQuery
	{
		public int EmployeeId { get; set; }

		public GetEmployeeByIdQuery(int employeeId)
		{
			EmployeeId = employeeId;
		}
	}
}
