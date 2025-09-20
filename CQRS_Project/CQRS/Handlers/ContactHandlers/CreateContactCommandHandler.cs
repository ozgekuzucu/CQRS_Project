// CreateContactCommandHandler.cs - BU DOSYAYI DEĞİŞTİRİN
using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.ContactCommands;
using CQRS_Project.Services.Abstract;

namespace CQRS_Project.CQRS.Handlers.ContactHandlers
{
	public class CreateContactCommandHandler
	{
		private readonly CqrsContext _context;
		private readonly IContactService? _contactService;
		private readonly IEmailService? _emailService;

		public CreateContactCommandHandler(
			CqrsContext context,
			IContactService? contactService = null,
			IEmailService? emailService = null)
		{
			_context = context;
			_contactService = contactService;
			_emailService = emailService;
		}

		public async Task Handle(CreateContactCommand command)
		{
			Console.WriteLine($"Subject: {command.Subject}");
			Console.WriteLine($"Message: {command.Message?.Substring(0, Math.Min(50, command.Message.Length))}...");

			string autoReply = "";
			bool aiSuccess = false;

			try
			{
				// 1. AI yanıtı oluştur (ContactService kullanarak)
				if (_contactService != null)
				{
					Console.WriteLine("AI yanıtı oluşturuluyor...");
					try
					{
						autoReply = await _contactService.GenerateAutoReplyAsync(command.Message ?? "", command.Subject ?? "");
						aiSuccess = true;
						Console.WriteLine($"AI yanıtı oluşturuldu: {autoReply.Substring(0, Math.Min(100, autoReply.Length))}...");
					}
					catch (Exception aiEx)
					{
						Console.WriteLine($"AI servis hatası: {aiEx.Message}");
						autoReply = GetDefaultReply();
					}
				}
				else
				{
					Console.WriteLine("ContactService bulunamadı, default yanıt kullanılıyor");
					autoReply = GetDefaultReply();
				}

				// 2. Veritabanına kaydet
				var contact = new Entities.Contact
				{
					Name = command.Name ?? "",
					Email = command.Email ?? "",
					Phone = command.Phone ?? "",
					Subject = command.Subject ?? "",
					Message = command.Message ?? "",
					CreatedDate = DateTime.Now,
					IsReplied = !string.IsNullOrEmpty(autoReply), // AI yanıtı varsa true
					ReplyMessage = autoReply ?? ""
				};

				Console.WriteLine("Veritabanına kaydediliyor...");
				_context.Contacts.Add(contact);
				await _context.SaveChangesAsync();

				Console.WriteLine($"Contact kaydedildi - ID: {contact.ContactId}, IsReplied: {contact.IsReplied}");

				// 3. Email gönder (eğer AI yanıtı varsa)
				if (_emailService != null && !string.IsNullOrEmpty(command.Email) && !string.IsNullOrEmpty(autoReply))
				{
					try
					{
						Console.WriteLine("Email gönderiliyor...");
						await _emailService.SendAutoReplyAsync(command.Email, autoReply);
						Console.WriteLine("Email gönderildi");

						// Admin bildirimi
						await _emailService.SendNotificationToAdminAsync(
							command.Name ?? "",
							command.Email,
							command.Subject ?? "",
							command.Message ?? "");
						Console.WriteLine("Admin bildirimi gönderildi");
					}
					catch (Exception emailEx)
					{
						Console.WriteLine($"Email hatası: {emailEx.Message}");
					}
				}

				Console.WriteLine($"=== CreateContactCommandHandler TAMAMLANDI - AI: {aiSuccess} ===");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"CreateContactCommandHandler genel hatası: {ex.Message}");
				throw;
			}
		}

		private string GetDefaultReply()
		{
			return @"Sayın müşterimiz,

					Mesajınız için teşekkür ederiz. Talebiniz alınmış olup, uzman ekibimiz en kısa sürede size geri dönecektir.

					İyi günler dileriz.

					Saygılarımızla,
					Müşteri Hizmetleri";
		}
	}
}