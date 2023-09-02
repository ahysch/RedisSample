using Microsoft.AspNetCore.Mvc;
using RedisSampleApp.API.Models;
using RedisSampleApp.API.Services.Abstract;

namespace RedisSampleApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _productService.GetAllAsync());
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAById(int id)
		{
			return Ok(await _productService.GetByIdAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			return Created(String.Empty, await _productService.CreateAsync(product));
		}
	}
}
