namespace CQRS_Project.CQRS.Results.CarResults
{
	public class GetCarQueryResult
	{
		public int CarId { get; set; }
		public string Model { get; set; }
		public string ImageUrl { get; set; }
		public decimal PricePerDay { get; set; }
		public bool IsAvailable { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public string BrandName { get; set; }
		public string CategoryName { get; set; }
		public string Transmission { get; set; }
		public string FuelType { get; set; }
		public int Stars { get; set; }
		public int SeatCount { get; set; }
		public string ModelYear { get; set; }
	}
}
