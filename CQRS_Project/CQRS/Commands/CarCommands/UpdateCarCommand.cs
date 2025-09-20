using CQRS_Project.Entities;

namespace CQRS_Project.CQRS.Commands.CarCommands
{
	public class UpdateCarCommand
	{
		public int CarId { get; set; }
		public string Model { get; set; }
		public string ImageUrl { get; set; }
		public decimal PricePerDay { get; set; }
		public bool IsAvailable { get; set; }

		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public int SeatCount { get; set; }
		public string FuelType { get; set; }
		public string ModelYear { get; set; }
		public string Transmission { get; set; }
		public int Stars { get; set; }
	}
}
