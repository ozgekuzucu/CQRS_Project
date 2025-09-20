using CQRS_Project.Services.Abstract;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Threading.Tasks;
using CQRS_Project.Models;


namespace CQRS_Project.Services.Abstract
{
	public interface IFuelPriceService
	{
		Task<decimal> GetFuelPriceAsync(string fuelType);
		Task<FuelPriceResponse> GetTurkeyFuelPricesAsync();
		Task<FuelPriceResponse> GetCurrentFuelPriceAsync(); // DistanceCalculationService için
	}
}
