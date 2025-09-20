using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.SliderCommands;

namespace CQRS_Project.CQRS.Handlers.SliderHandlers
{
	public class RemoveSliderCommandHandler
	{
		private readonly CqrsContext _context;

		public RemoveSliderCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(RemoveSliderCommand command)
		{
			var values = await _context.Sliders.FindAsync(command.SliderId);
			_context.Remove(values);
			await _context.SaveChangesAsync();
		}
	}
}
