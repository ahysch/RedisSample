using Microsoft.EntityFrameworkCore;
using RedisSampleApp.API.Data;
using RedisSampleApp.API.Models;
using RedisSampleApp.API.Repositories.Abstract;

namespace RedisSampleApp.API.Repositories.Concrete
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _dbContext;

		public ProductRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			await _dbContext.Products.AddAsync(product);
			await _dbContext.SaveChangesAsync();

			return product;
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await _dbContext.Products.ToListAsync();
		}

		public async Task<ProductDto> GetByIdAsync(int id)
		{
			var product = await _dbContext.Products.FindAsync(id);
			return DtoExtensions<ProductDto>.ToDto(product);
		}

	}
}
