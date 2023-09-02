using RedisSampleApp.API.Models;

namespace RedisSampleApp.API.Repositories.Abstract
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllAsync();
		Task<ProductDto> GetByIdAsync(int id);
		Task<Product> CreateAsync(Product product);
	}
}
