namespace CQRS_Project.CQRS.Results.AboutResults
{
	public class GetAboutQueryResult
	{
		public int AboutId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string SubDescription { get; set; }
		public string ImageUrl { get; set; }
		public string ImageUrl2 { get; set; }
		public string Mission { get; set; }
		public string Vision { get; set; }
	}
}
