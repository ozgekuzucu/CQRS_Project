namespace CQRS_Project.CQRS.Results.LocationResults
{
	public class DistanceResult
	{
		public double DistanceKm { get; set; }
		public double EstimatedFuelLiters { get; set; }
		public decimal EstimatedFuelCost { get; set; }
		public bool IsEstimated { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
