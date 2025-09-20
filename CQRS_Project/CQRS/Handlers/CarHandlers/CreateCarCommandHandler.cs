using CQRS_Project.Context;
using CQRS_Project.CQRS.Commands.CarCommands;
using CQRS_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.CQRS.Handlers.CarHandlers
{
	public class CreateCarCommandHandler
	{
		private readonly CqrsContext _context;

		public CreateCarCommandHandler(CqrsContext context)
		{
			_context = context;
		}
		public async Task Handle(CreateCarCommand command)
		{
			_context.Cars.Add(new Car
			{
				Model = command.Model,
				ImageUrl = command.ImageUrl,
				PricePerDay = command.PricePerDay,
				IsAvailable = command.IsAvailable,
				BrandId = command.BrandId,
				CategoryId = command.CategoryId,
				SeatCount = command.SeatCount,
				FuelType = command.FuelType,
				ModelYear = command.ModelYear,
				Transmission = command.Transmission,
				Stars = command.Stars,
			});
			await _context.SaveChangesAsync();
		}
	}
}
