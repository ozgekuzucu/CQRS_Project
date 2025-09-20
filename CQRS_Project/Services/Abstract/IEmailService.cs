namespace CQRS_Project.Services.Abstract
{
	public interface IEmailService
	{
		Task SendAutoReplyAsync(string toEmail, string message, string subject = "Mesajınız Alındı");
		Task SendNotificationToAdminAsync(string contactName, string contactEmail, string subject, string message);

	}
}
