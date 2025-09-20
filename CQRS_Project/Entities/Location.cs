namespace CQRS_Project.Entities
{
	public class Location
	{
		public int LocationId { get; set; }   
		public string City { get; set; }     
		public string District { get; set; }  
		public string Address { get; set; }  
		public bool IsActive { get; set; }

		//km hesaplamaları için
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}

}
