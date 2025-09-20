using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.AboutCommands;

namespace CQRS_Project.CQRS.Handlers.AboutHandlers
{
	public class RemoveAboutCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveAboutCommandHandler(CqrsContext context)
		{
			_context = context;
		}

		public async Task Handle(RemoveAboutCommand command)
		{
			var values = await _context.Abouts.FindAsync(command.AboutId);

			if (values != null)
			{
				_context.Abouts.Remove(values);
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception($"About with Id {command.AboutId} bulunamadı.");
			}
		}

	}
}
