using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.AboutCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.AboutHandlers
{
	public class CreateAboutCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateAboutCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateAboutCommand command)
		{
			_context.Abouts.Add(new About
			{
				Title = command.Title,
				Description = command.Description,
				SubDescription = command.SubDescription,
				ImageUrl = command.ImageUrl,
				ImageUrl2 = command.ImageUrl2,
				Mission = command.Mission,
				Vision = command.Vision
			});
			await _context.SaveChangesAsync();
		}
	}
}
