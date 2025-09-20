using CQRS_Project.CQRS.Results.LocationResults;

public interface IDistanceCalculationService
{
	Task<DistanceResult> CalculateDistanceAsync(int pickUpLocationId, int dropOffLocationId);

	Task<decimal> CalculateFuelCost(double distanceKm);
}
