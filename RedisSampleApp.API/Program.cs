using Microsoft.EntityFrameworkCore;
using RedisSampleApp.API.Data;
using RedisSampleApp.API.Models;
using RedisSampleApp.API.Repositories.Abstract;
using RedisSampleApp.API.Repositories.Concrete;
using RedisSampleApp.API.Services.Abstract;
using RedisSampleApp.API.Services.Concrete;
using RedisSampleApp.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(sp =>
{
	return new RedisService(builder.Configuration["CacheOptions:Url"]!);
});
builder.Services.AddSingleton(sp =>
{
	var redisService = sp.GetRequiredService<RedisService>();
	return redisService.GetDb(0);
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseInMemoryDatabase("TestDatabase");
});

builder.Services.AddScoped<IProductRepository>(sp =>
{
	var appDbContext = sp.GetRequiredService<AppDbContext>();
	var productRepository = new ProductRepository(appDbContext);
	var redisService = sp.GetRequiredService<RedisService>();

	return new ProductRepositoryWithCache(productRepository, redisService);
});

builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
