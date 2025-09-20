using CQRS_Project.Context;
using CQRS_Project.Entities;
using CQRS_Project.Services.Abstract; // FuelPriceService için
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.ViewComponents
{
	public class ReservationSummaryViewComponent : ViewComponent
	{
		private readonly IFuelPriceService _fuelPriceService;
		private readonly CqrsContext _context;

		public ReservationSummaryViewComponent(IFuelPriceService fuelPriceService, CqrsContext context)
		{
			_fuelPriceService = fuelPriceService;
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(
			int carId, string brand, string model, decimal pricePerDay,
			decimal totalPrice, int totalDays,
			int pickUpLocationId, int dropOffLocationId,
			DateTime startDate, DateTime endDate,
			string pickUpText, string dropOffText,
			string startTime, string endTime,
			double pickUpLat, double pickUpLon,
			double dropOffLat, double dropOffLon)
		{
			// Aracı DB’den alıyoruz (FuelType için)
			var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

			// Koordinatları temizle
			pickUpLat = double.IsNaN(pickUpLat) ? 0 : pickUpLat;
			pickUpLon = double.IsNaN(pickUpLon) ? 0 : pickUpLon;
			dropOffLat = double.IsNaN(dropOffLat) ? 0 : dropOffLat;
			dropOffLon = double.IsNaN(dropOffLon) ? 0 : dropOffLon;

			// Mesafe hesapla
			double distanceKm = GetDistance(pickUpLat, pickUpLon, dropOffLat, dropOffLon);

			// Eğer 0 km ise ortalama şehir içi değer ver
			if (distanceKm == 0)
			{
				distanceKm = 12;
			}

			// API’den fiyatları al
			var fuelPrices = await _fuelPriceService.GetTurkeyFuelPricesAsync();

			double fuelConsumptionPer100Km = 7; // varsayılan
			decimal fuelPricePerUnit = 0;

			// Yakıt türüne göre hesap
			switch (car?.FuelType?.ToLower())
			{
				case "benzin":
					fuelPricePerUnit = fuelPrices.Benzin;
					break;
				case "dizel":
					fuelPricePerUnit = fuelPrices.Motorin;
					break;
				case "lpg":
					fuelPricePerUnit = fuelPrices.Lpg;
					break;
				case "elektrik":
					fuelConsumptionPer100Km = 15; // kWh/100km
					fuelPricePerUnit = 1.5m; // ₺/kWh (örnek)
					break;
			}

			// Hesaplama
			double estimatedFuelLiters = (distanceKm / 100) * fuelConsumptionPer100Km;
			decimal estimatedFuelCost = Math.Round((decimal)estimatedFuelLiters * fuelPricePerUnit, 2);

			var modelData = new ReservationSummaryViewModel
			{
				CarId = carId,
				Brand = brand,
				Model = model,
				PricePerDay = pricePerDay,
				TotalPrice = totalPrice,
				TotalDays = totalDays,
				PickUpLocationId = pickUpLocationId,
				DropOffLocationId = dropOffLocationId,
				StartDate = startDate,
				EndDate = endDate,
				PickUpText = pickUpText,
				DropOffText = dropOffText,
				StartTime = startTime,
				EndTime = endTime,
				DistanceKm = distanceKm,
				EstimatedFuelLiters = estimatedFuelLiters,
				EstimatedFuelCost = estimatedFuelCost
			};

			return View(modelData);
		}

		private double GetDistance(double lat1, double lon1, double lat2, double lon2)
		{
			if (lat1 == 0 || lon1 == 0 || lat2 == 0 || lon2 == 0)
				return 0;

			double R = 6371; // km
			double dLat = (lat2 - lat1) * (Math.PI / 180);
			double dLon = (lon2 - lon1) * (Math.PI / 180);
			double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					   Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
					   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return R * c;
		}
	}

	public class ReservationSummaryViewModel
	{
		public int CarId { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string FuelType { get; set; }
		public decimal PricePerDay { get; set; }
		public decimal TotalPrice { get; set; }
		public int TotalDays { get; set; }
		public int PickUpLocationId { get; set; }
		public int DropOffLocationId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string PickUpText { get; set; }
		public string DropOffText { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public double DistanceKm { get; set; }
		public double EstimatedFuelLiters { get; set; }
		public decimal EstimatedFuelCost { get; set; }
	}
}
