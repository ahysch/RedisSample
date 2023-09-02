using RedisSampleApp.API.Models;
using RedisSampleApp.API.Repositories.Abstract;
using RedisSampleApp.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisSampleApp.API.Repositories.Concrete
{
	public class ProductRepositoryWithCache : IProductRepository
	{
		private readonly IProductRepository _productRepository;
		private readonly RedisService _redisService;
		private readonly IDatabase _database;

		protected const string ProductKey = "ProductKey";

		public ProductRepositoryWithCache(IProductRepository productRepository, RedisService redisService)
		{
			_productRepository = productRepository;
			_redisService = redisService;
			_database = _redisService.GetDb(2);
		}

		public async Task<Product> CreateAsync(Product product)
		{
			var newProduct = await _productRepository.CreateAsync(product);
			if (await IsCacheSetted(ProductKey))
			{
				await AddToCacheSingleProduct(newProduct);
			}
			return newProduct;
		}


		public async Task<List<Product>> GetAllAsync()
		{
			if (!await _database.KeyExistsAsync(ProductKey))
				return await LoadToCacheFromDbAsync();

			var cachedProducts = await _database.HashGetAllAsync(ProductKey);

			var products = new List<Product>();

			cachedProducts.ToList().ForEach(ch =>
			{
				products.Add(JsonSerializer.Deserialize<Product>(ch.Value));
			});

			return products;
		}

		public async Task<ProductDto> GetByIdAsync(int id)
		{
			if (await _database.KeyExistsAsync(ProductKey))
			{
				var product = await _database.HashGetAsync(ProductKey, id);

				return product.HasValue ? JsonSerializer.Deserialize<ProductDto>(product) : null;
			}

			var products = await LoadToCacheFromDbAsync();

			var cachedProduct = products.FirstOrDefault(x => x.Id == id);
			return DtoExtensions<ProductDto>.ToDto(cachedProduct);
		}

		private async Task<List<Product>> LoadToCacheFromDbAsync()
		{
			var products = await _productRepository.GetAllAsync();
			products.ForEach(p =>
			{
				_database.HashSetAsync(ProductKey, p.Id, JsonSerializer.Serialize(p));
			});

			return products;
		}
		private async Task<bool> IsCacheSetted(string key)
		{
			return await _database.KeyExistsAsync(key);
		}
		private async Task AddToCacheSingleProduct(Product newProduct)
		{
			await _database.HashSetAsync(ProductKey, newProduct.Id, JsonSerializer.Serialize(newProduct));
		}

		public Task<List<ProductDto>> GetAllDtoAsync()
		{
			throw new NotImplementedException();
		}
	}
}
