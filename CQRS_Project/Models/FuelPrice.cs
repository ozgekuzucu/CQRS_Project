namespace CQRS_Project.Models
{
	public class FuelPrice
	{
		public string FuelType { get; set; } // "Benzin", "Motorin", "LPG"
		public decimal Price { get; set; }
		public string Currency { get; set; } = "TL";
		public DateTime LastUpdated { get; set; } = DateTime.Now;
		public string City { get; set; } = "İstanbul";
		public string Company { get; set; } = "Ortalama";
	}

	public class FuelPriceResponse
	{
		public decimal Benzin { get; set; }
		public decimal Motorin { get; set; }
		public decimal Lpg { get; set; }
		public string LastUpdate { get; set; } = "";

		// Backward compatibility için
		public decimal Petrol => Benzin;
		public decimal Diesel => Motorin;
	}
}