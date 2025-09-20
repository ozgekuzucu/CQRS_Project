using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.AboutCommands;
using System;
using System.Reflection;

namespace CQRS_Project.CQRS.Handlers.AboutHandlers
{
	public class UpdateAboutCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateAboutCommandHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task Handle(UpdateAboutCommand command)
		{
			var values = await _context.Abouts.FindAsync(command.AboutId);
			values.Title = command.Title;
			values.Description = command.Description;
			values.SubDescription = command.SubDescription;
			values.ImageUrl = command.ImageUrl;
			values.ImageUrl2 = command.ImageUrl2;
			values.Mission = command.Mission;
			values.Vision = command.Vision;
			await _context.SaveChangesAsync();
		}
	}
}
