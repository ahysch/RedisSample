using RedisSampleApp.API.Models;

namespace RedisSampleApp.API.Services.Abstract
{
	public interface IProductService
	{
		Task<List<Product>> GetAllAsync();
		Task<ProductDto> GetByIdAsync(int id);
		Task<Product> CreateAsync(Product product);
	}
}
