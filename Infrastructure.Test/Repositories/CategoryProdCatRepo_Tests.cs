using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class CategoryProdCatRepo_Tests
{
	private readonly ProductDbContext context = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);
	[Fact]
	public void Read_ShouldReturnCategoryEntityIfExists_OtherwiseReturnNull()
	{
		// Arrange
		var repo = new CategoryProdCatRepo( context );
		repo.Create(new Category { Name = "Test" });
		// Act
		var result = repo.Read(x => x.Name.ToLower() == "test");
		// Assert
		Assert.Equal("Test", result.Name);

	}

	[Fact]
	public void Read_ShouldIncludeProducts()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test" });
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		manufacturerRepo.Create(new Manufacturer {  Name = "Testmanufacturer" });
		var productRepo= new ProductProdCatRepo(context);
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 100, CategoryId = 1, ManufacturerId = 1 });
		productRepo.Create(new Product { Title = "Test2", Description = "Test2", Price = 200, CategoryId = 1, ManufacturerId = 1 });
		// Act
		var result = categoryRepo.Read(x => x.Name.ToLower() == "test");
		// Assert
		Assert.Equal("Test", result.Products.First().Title);
		Assert.Equal("Testmanufacturer", result.Products.First().Manufacturer.Name);
		Assert.Equal(2, result.Products.Count());
	}
}
