using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.AboutCommands;
using CQRS_Project.CQRS.Commands.CarCommands;
using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class UpdateCarCommandHandler
	{
		private readonly CqrsContext _context;

		public UpdateCarCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(UpdateCarCommand command)
		{
			var values = await _context.Cars.FindAsync(command.CarId);
			values.Model = command.Model;
			values.ImageUrl = command.ImageUrl;
			values.PricePerDay = command.PricePerDay;
			values.IsAvailable = command.IsAvailable;
			values.BrandId = command.BrandId;
			values.CategoryId = command.CategoryId;
			values.SeatCount =command.SeatCount;
			values.FuelType = command.FuelType;
			values.ModelYear= command.ModelYear;
			values.Transmission =command.Transmission;
			values.Stars = command.Stars;
			await _context.SaveChangesAsync();
		}
	}
}
