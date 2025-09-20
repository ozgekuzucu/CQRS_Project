namespace CQRS_Project.CQRS.Queries.SliderQueries
{
	public class GetSliderByIdQuery
	{
		public int SliderId { get; set; }

		public GetSliderByIdQuery(int sliderId)
		{
			SliderId = sliderId;
		}
	}
}
