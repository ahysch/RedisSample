using Microsoft.EntityFrameworkCore;
using RedisSampleApp.API.Models;

namespace RedisSampleApp.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData(
				new Product() { Id = 1, Name = "Cpu", Price = 100, Stock = 250 },
				new Product() { Id = 2, Name = "Ram", Price = 110, Stock = 260 },
				new Product() { Id = 3, Name = "Hdd", Price = 120, Stock = 270 },
				new Product() { Id = 4, Name = "Mouse", Price = 130, Stock = 280 },
				new Product() { Id = 5, Name = "Keyboard", Price = 140, Stock = 290 }
				);




			base.OnModelCreating(modelBuilder);
		}
	}
}
