namespace CQRS_Project.CQRS.Commands.BrandCommands
{
	public class RemoveBrandCommand
	{
		public int BrandId { get; set; }

		public RemoveBrandCommand(int brandId)
		{
			BrandId = brandId;
		}
	}
}
