namespace CQRS_Project.CQRS.Queries.ContactQueries
{
	public class GetContactByIdQuery
	{
		public int ContactId { get; set; }

		public GetContactByIdQuery(int contactId)
		{
			ContactId = contactId;
		}
	}
}
