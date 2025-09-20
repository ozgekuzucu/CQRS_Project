using CQRS_Project.CQRS.Commands.ContactCommands;
using CQRS_Project.CQRS.Handlers.ContactHandlers;
using CQRS_Project.CQRS.Queries.ContactQueries;
using CQRS_Project.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Project.Controllers
{
	public class ContactController : Controller
	{
		private readonly GetContactByIdQueryHandler _getContactByIdQueryHandler;
		private readonly GetContactQueryHandler _getContactQueryHandler;
		private readonly CreateContactCommandHandler _createContactCommandHandler;
		private readonly UpdateContactCommandHandler _updateContactCommandHandler;
		private readonly RemoveContactCommandHandler _removeContactCommandHandler;
		private readonly IContactService _contactService;
		private readonly IEmailService _emailService;
		private readonly IConfiguration _configuration;

		public ContactController(
			GetContactByIdQueryHandler getContactByIdQueryHandler,
			GetContactQueryHandler getContactQueryHandler,
			CreateContactCommandHandler createContactCommandHandler,
			UpdateContactCommandHandler updateContactCommandHandler,
			RemoveContactCommandHandler removeContactCommandHandler,
			IContactService contactService,
			IEmailService emailService,
			IConfiguration configuration)
		{
			_getContactByIdQueryHandler = getContactByIdQueryHandler;
			_getContactQueryHandler = getContactQueryHandler;
			_createContactCommandHandler = createContactCommandHandler;
			_updateContactCommandHandler = updateContactCommandHandler;
			_removeContactCommandHandler = removeContactCommandHandler;
			_contactService = contactService;
			_emailService = emailService;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _getContactQueryHandler.Handle();
			return View(values);
		}

		[HttpGet]
		public IActionResult CreateContact()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateContact(CreateContactCommand command)
		{
			await _createContactCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> SubmitContact(CreateContactCommand command)
		{
			Console.WriteLine($"Email: {command.Email}");
			Console.WriteLine($"Subject: {command.Subject}");
			Console.WriteLine($"Message: {command.Message.Substring(0, Math.Min(50, command.Message.Length))}...");

			try
			{
				// 1. Mesajı kaydet
				Console.WriteLine("1. ADIM: Mesaj veritabanına kaydediliyor...");
				await _createContactCommandHandler.Handle(command);
				Console.WriteLine("✓ Mesaj başarıyla kaydedildi");

				// 2. AI cevabını üret
				Console.WriteLine("2. ADIM: AI yanıt oluşturuluyor...");
				var aiReply = await _contactService.GenerateAutoReplyAsync(command.Message, command.Subject);
				Console.WriteLine($"✓ AI yanıtı alındı: {aiReply.Substring(0, Math.Min(100, aiReply.Length))}...");

				// 3. Veritabanında güncelle
				Console.WriteLine("3. ADIM: Contact güncelleniyor...");
				var contacts = await _getContactQueryHandler.Handle();
				Console.WriteLine($"Toplam contact sayısı: {contacts.Count()}");

				var contact = contacts.LastOrDefault(c =>
					c.Email == command.Email &&
					c.Message == command.Message);

				if (contact != null)
				{
					Console.WriteLine($"Contact bulundu: ID={contact.ContactId}");

					var updateCommand = new UpdateContactCommand
					{
						ContactId = contact.ContactId,
						Name = contact.Name,
						Email = contact.Email,
						Phone = contact.Phone,
						Subject = contact.Subject,
						Message = contact.Message,
						IsReplied = true,
						ReplyMessage = aiReply
					};

					Console.WriteLine("UpdateCommand oluşturuldu, güncelleme yapılıyor...");
					await _updateContactCommandHandler.Handle(updateCommand);
					Console.WriteLine(" Contact başarıyla güncellendi");
				}
				else
				{
					Console.WriteLine(" UYARI: Contact bulunamadı, güncelleme yapılamadı");
					// Debug: Kayıtlı contactları listele
					foreach (var c in contacts.TakeLast(5))
					{
						Console.WriteLine($"Contact: ID={c.ContactId}, Email={c.Email}, Subject={c.Subject}");
					}
				}

				// 4. Mail gönder
				Console.WriteLine("4. ADIM: Email gönderiliyor...");

				try
				{
					Console.WriteLine("Otomatik yanıt emaili gönderiliyor...");
					await _emailService.SendAutoReplyAsync(command.Email, aiReply);
					Console.WriteLine(" Otomatik yanıt emaili gönderildi");

					Console.WriteLine("Admin bilgilendirme emaili gönderiliyor...");
					await _emailService.SendNotificationToAdminAsync(command.Name, command.Email, command.Subject, command.Message);
					Console.WriteLine(" Admin bilgilendirme emaili gönderildi");
				}
				catch (Exception emailEx)
				{
					Console.WriteLine($" EMAIL HATASI: {emailEx.Message}");
					Console.WriteLine($"Email Stack Trace: {emailEx.StackTrace}");
					// Email hatası işlemi durdurmaz
				}

				TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi ve AI yanıtı e-posta adresinize iletildi. Teşekkür ederiz!";
				return RedirectToAction("Index", "Default");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				Console.WriteLine($"Stack Trace: {ex.StackTrace}");
				Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

				TempData["ErrorMessage"] = "Mesaj gönderilirken bir hata oluştu: " + ex.Message;
				return RedirectToAction("Index", "Default");
			}
		}

		public async Task<IActionResult> DeleteContact(int id)
		{
			await _removeContactCommandHandler.Handle(new RemoveContactCommand(id));
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateContact(int id)
		{
			var value = await _getContactByIdQueryHandler.Handle(new GetContactByIdQuery(id));
			return View(value);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateContact(UpdateContactCommand command)
		{
			await _updateContactCommandHandler.Handle(command);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> GenerateAIReply(int contactId)
		{
			Console.WriteLine($"=== MANUEL AI YANIT: ContactId={contactId} ===");

			try
			{
				var contact = await _getContactByIdQueryHandler.Handle(new GetContactByIdQuery(contactId));
				if (contact == null)
				{
					Console.WriteLine("Contact bulunamadı");
					TempData["ErrorMessage"] = "Contact bulunamadı.";
					return RedirectToAction("Index");
				}

				Console.WriteLine($"Contact bulundu: {contact.Subject}");
				var aiReply = await _contactService.GenerateAutoReplyAsync(contact.Message, contact.Subject);

				var updateCommand = new UpdateContactCommand
				{
					ContactId = contact.ContactId,
					Name = contact.Name,
					Email = contact.Email,
					Phone = contact.Phone,
					Subject = contact.Subject,
					Message = contact.Message,
					IsReplied = true,
					ReplyMessage = aiReply
				};

				await _updateContactCommandHandler.Handle(updateCommand);
				Console.WriteLine("Manuel AI yanıt başarıyla güncellendi");

				TempData["SuccessMessage"] = "AI yanıtı başarıyla oluşturuldu.";
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Manuel AI yanıt hatası: {ex.Message}");
				TempData["ErrorMessage"] = "AI yanıtı oluşturulurken hata oluştu: " + ex.Message;
			}

			return RedirectToAction("Index");
		}


	}
}