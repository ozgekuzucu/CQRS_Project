using CQRS_Project.Entities;
using System.Dynamic;

namespace CQRS_Project.Services.Abstract
{
	public interface ICarRecommendationService
	{		Task<string> GetCarRecommendationAsync(string userQuery, List<Car> availableCars);
	}
}
