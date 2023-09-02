namespace RedisSampleApp.API.Models
{
	public static class DtoExtensions<T> where T : class , IDto
	{
		/// <summary>
		/// Product => ProductDto dönüştürme işlemi.
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		public static ProductDto ToDto(Product product)
		{
			return new ProductDto()
			{
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock
			};
		}

		public static List<ProductDto> ToDtoList(List<Product> products)
		{
			return products.Select(p => ToDto(p)).ToList();
		}
	}
}
