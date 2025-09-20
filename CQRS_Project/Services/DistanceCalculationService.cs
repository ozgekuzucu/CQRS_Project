using CQRS_Project.Context;
using CQRS_Project.CQRS.Results.LocationResults;
using CQRS_Project.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Project.Services
{
	public class DistanceCalculationService : IDistanceCalculationService
	{
		private readonly CqrsContext _context;
		private readonly IFuelPriceService _fuelPriceService;
		private const double FUEL_CONSUMPTION_PER_100KM = 7.0; // litre

		public DistanceCalculationService(CqrsContext context, IFuelPriceService fuelPriceService)
		{
			_context = context;
			_fuelPriceService = fuelPriceService;
		}

		// Mesafe ve yakıt maliyeti hesaplama
		public async Task<DistanceResult> CalculateDistanceAsync(int pickUpLocationId, int dropOffLocationId)
		{
			var pickUp = await _context.Locations.FindAsync(pickUpLocationId);
			var dropOff = await _context.Locations.FindAsync(dropOffLocationId);

			if (pickUp == null || dropOff == null)
			{
				return new DistanceResult
				{
					DistanceKm = 50,
					EstimatedFuelLiters = (50.0 / 100) * FUEL_CONSUMPTION_PER_100KM,
					EstimatedFuelCost = await GetFuelCost(50, "Benzin"),
					IsEstimated = true
				};
			}

			double distanceKm = CalculateHaversineDistance(pickUp.Latitude, pickUp.Longitude, dropOff.Latitude, dropOff.Longitude);
			double fuelLiters = (distanceKm / 100) * FUEL_CONSUMPTION_PER_100KM;

			decimal fuelCost = await GetFuelCost(distanceKm, "Benzin"); // default fuel type benzin

			return new DistanceResult
			{
				DistanceKm = distanceKm,
				EstimatedFuelLiters = fuelLiters,
				EstimatedFuelCost = fuelCost,
				IsEstimated = false
			};
		}

		// Yakıt maliyetini döndür
		public async Task<decimal> GetFuelCost(double distanceKm, string fuelType)
		{
			decimal fuelPricePerLiter = await GetCurrentFuelPrice(fuelType);
			double fuelConsumptionPer100Km = 7; // default
			if (fuelType.ToLower().Contains("elektrik"))
			{
				fuelConsumptionPer100Km = 15; // kWh/100km
				fuelPricePerLiter = 1.5m; // ₺/kWh
			}

			double liters = (distanceKm / 100) * fuelConsumptionPer100Km;
			return Math.Round((decimal)liters * fuelPricePerLiter, 2);
		}

		// Gerçek yakıt fiyatını servis üzerinden al
		private async Task<decimal> GetCurrentFuelPrice(string fuelType)
		{
			var prices = await _fuelPriceService.GetCurrentFuelPriceAsync();
			return fuelType.ToLower() switch
			{
				"benzin" => prices.Benzin,
				"dizel" => prices.Diesel,
				"lpg" => prices.Lpg,
				_ => prices.Benzin
			};
		}

		// Haversine mesafe hesaplama
		private double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
		{
			const double R = 6371; // km
			var dLat = ToRadians(lat2 - lat1);
			var dLon = ToRadians(lon2 - lon1);

			var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
					Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return R * c;
		}

		private double ToRadians(double degrees) => degrees * (Math.PI / 180);
		
		public async Task<decimal> CalculateFuelCost(double distanceKm)
		{
			// API üzerinden benzin fiyatını al
			var prices = await _fuelPriceService.GetCurrentFuelPriceAsync();
			decimal fuelPricePerLiter = prices.Benzin; // Sadece benzin fiyatı kullanılıyor

			double fuelConsumptionPer100Km = 7; // Sabit değer
			double liters = (distanceKm / 100) * fuelConsumptionPer100Km;

			return Math.Round((decimal)liters * fuelPricePerLiter, 2);
		}

	}
}
