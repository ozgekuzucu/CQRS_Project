using CQRS_Project.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CQRS_Project.Context
{
	public class CqrsContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=DESKTOP-PUNASPD;database=CQRSProjectDB;integrated security=true;trust server certificate=true");
		}

		public DbSet<About> Abouts { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Slider> Sliders { get; set; }
	}
}
