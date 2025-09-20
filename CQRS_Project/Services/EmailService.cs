using CQRS_Project.Services.Abstract;
using System.Net;
using System.Net.Mail;

namespace CQRS_Project.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendAutoReplyAsync(string toEmail, string message, string subject = "Mesajınız Alındı")
		{
			try
			{
				var smtpClient = CreateSmtpClient();

				var mailMessage = new MailMessage()
				{
					From = new MailAddress(_configuration["Email:SenderEmail"], "Rentigo Müşteri İletişim Sistemi"),
					Subject = subject,
					Body = CreateAutoReplyHtml(message),
					IsBodyHtml = true
				};

				mailMessage.To.Add(toEmail);

				await smtpClient.SendMailAsync(mailMessage);
			}
			catch (Exception ex)
			{
				// Log the error
				Console.WriteLine($"Email sending error: {ex.Message}");
				throw;
			}
		}

		public async Task SendNotificationToAdminAsync(string contactName, string contactEmail, string subject, string message)
		{
			try
			{
				var smtpClient = CreateSmtpClient();

				var mailMessage = new MailMessage()
				{
					From = new MailAddress(_configuration["Email:SenderEmail"], "Rentigo Müşteri İletişim Sistemi"),
					Subject = $"Yeni İletişim Mesajı: {subject}",
					Body = CreateAdminNotificationHtml(contactName, contactEmail, subject, message),
					IsBodyHtml = true
				};

				// Admin email adreslerini configuration'dan al
				var adminEmails = _configuration["Email:AdminEmails"]?.Split(',') ?? new[] { "admin@domain.com" };

				foreach (var adminEmail in adminEmails)
				{
					mailMessage.To.Add(adminEmail.Trim());
				}

				await smtpClient.SendMailAsync(mailMessage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Admin notification email error: {ex.Message}");
				throw;
			}
		}

		private SmtpClient CreateSmtpClient()
		{
			return new SmtpClient(_configuration["Email:SmtpServer"])
			{
				Port = int.Parse(_configuration["Email:SmtpPort"]),
				Credentials = new NetworkCredential(
					_configuration["Email:SenderEmail"],
					_configuration["Email:SenderPassword"]),
				EnableSsl = true
			};
		}

		private string CreateAutoReplyHtml(string message)
		{
			return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Otomatik Yanıt</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
    </style>
</head>
<body>
    <p>{message}</p>
    <p>Saygılarımızla,<br>Rentigo Müşteri İletişim Ekibi</p>
    <br>
    <p style='font-size:12px; color:#777;'>&copy; {DateTime.Now:yyyy} Rentigo Müşteri İletişim Sistemi</p>
</body>
</html>";
		}


		private string CreateAdminNotificationHtml(string contactName, string contactEmail, string subject, string message)
		{
			return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Yeni İletişim Mesajı</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
    </style>
</head>
<body>
    <p><strong>Yeni bir iletişim mesajı alındı.</strong></p>
    <p><strong>Ad Soyad:</strong> {contactName}<br>
       <strong>E-posta:</strong> {contactEmail}<br>
       <strong>Konu:</strong> {subject}<br>
       <strong>Tarih:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
    <p><strong>Mesaj:</strong></p>
    <p>{message}</p>
</body>
</html>";
		}

	}
}
