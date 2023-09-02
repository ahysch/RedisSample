using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.CompilerServices;

namespace RedisSampleApp.API.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public int Stock { get; set; }

	}

	public class ProductDto : IDto
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
	public interface IDto
	{

	}
}
