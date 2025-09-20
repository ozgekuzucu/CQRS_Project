namespace CQRS_Project.Services.Abstract
{
	public interface IContactService
	{
		Task<string> GenerateAutoReplyAsync(string message, string subject);
	}
}
