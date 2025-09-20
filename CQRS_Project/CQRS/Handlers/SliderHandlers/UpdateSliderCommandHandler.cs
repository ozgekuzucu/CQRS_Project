using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.SliderCommands;

namespace CQRS_Project.CQRS.Handlers.SliderHandlers
{
	public class UpdateSliderCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateSliderCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateSliderCommand command)
		{
			var values = await _context.Sliders.FindAsync(command.SliderId);
			values.Title = command.Title;
			values.Description = command.Description;
			values.ImageUrl = command.ImageUrl;
			await _context.SaveChangesAsync();
		}
	}
}
