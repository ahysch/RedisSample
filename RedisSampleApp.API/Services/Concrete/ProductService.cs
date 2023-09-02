using RedisSampleApp.API.Models;
using RedisSampleApp.API.Repositories.Abstract;
using RedisSampleApp.API.Services.Abstract;

namespace RedisSampleApp.API.Services.Concrete
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			return await _productRepository.CreateAsync(product);
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await _productRepository.GetAllAsync();
		}

		public async Task<ProductDto> GetByIdAsync(int id)
		{
			return await _productRepository.GetByIdAsync(id);
		}
	}
}
