namespace CQRS_Project.CQRS.Commands.EmployeeCommands
{
	public class RemoveEmployeeCommand
	{
		public int EmployeeId { get; set; }

		public RemoveEmployeeCommand(int employeeId)
		{
			EmployeeId = employeeId;
		}
	}
}
