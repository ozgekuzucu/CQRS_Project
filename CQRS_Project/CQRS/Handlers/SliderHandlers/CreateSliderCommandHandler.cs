using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.SliderCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.SliderHandlers
{
	public class CreateSliderCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateSliderCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateSliderCommand command)
		{
			_context.Sliders.Add(new Slider
			{
				Title = command.Title,
				Description = command.Description,
				ImageUrl = command.ImageUrl
			});
			await _context.SaveChangesAsync();
		}
	}
}
