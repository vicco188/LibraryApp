using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class ProductProdCatRepo_Tests
{
	private readonly ProductDbContext context = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldCreateProduct_AndReturnProduct()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo( context );
		var manufacturerRepo = new ManufacturerProdCatRepo( context );
		var productRepo = new ProductProdCatRepo( context );
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		// Act
		var result = productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 }) ;
		// Assert
		Assert.NotNull( result );
		Assert.Equal("Test", result.Title);
		Assert.Equal("Test Manufacturer", result.Manufacturer.Name);
	}

	[Fact]
	public void Read_ShouldReturnProductIfExists_OtherwiseReturnNull()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		// Act
		var result1 = productRepo.Read(p => p.ArticleNumber == 1);
		var result2 = productRepo.Read(p => p.ArticleNumber == 2);
		// Assert
		Assert.NotNull(result1);
		Assert.Equal("Test", result1.Title);
		Assert.Null(result2);
	}
	[Fact]
	public void ReadAll_ShouldReturnAListOfProducts()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		// Act
		var result = productRepo.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<Product>>(result);
		Assert.Single(result);
	}

	[Fact]
	public void ReadAll_ShouldReturnEmptyListOfProducts_IfNoProductsInDb()
	{
		// Arrange
		var productRepo = new ProductProdCatRepo(context);
		// Act
		var result = productRepo.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<Product>>(result);
		Assert.Empty(result);
	}

	[Fact]
	public void Update_ShouldUpdateInDbAndReturnUpdatedEntity_IfEntryExists()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		var entityToUpdate = productRepo.Read(e => e.ArticleNumber == 1);
		entityToUpdate.Price = 49;
		// Act
		var result = productRepo.Update(p => p.ArticleNumber == 1, entityToUpdate);
		// Assert
		Assert.Equal(49, result.Price);
	}


	[Fact]
	public void Update_ShoudNotUpdateInDbReturnNull_IfEntryDoesNotExist()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		var entityToUpdate = productRepo.Read(e => e.ArticleNumber == 1);
		entityToUpdate.Price = 49;
		// Act
		var result = productRepo.Update(p => p.ArticleNumber == 2, entityToUpdate);
		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Delete_ShouldDeleteInDbAndReturnEntity_IfEntryExists()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		productRepo.Create(new Product { Title = "Test2", Description = "Test2", Price = 199, CategoryId = 1, ManufacturerId = 1 });
		// Act
		var result = productRepo.Delete(p => p.ArticleNumber == 1);
		var allEntries = productRepo.ReadAll();
		// Assert 
		Assert.Equal(1, result.ArticleNumber);
		Assert.Single(allEntries);
		Assert.Equal(2, allEntries.First().ArticleNumber);
	}

	[Fact]
	public void Delete_ShouldNotChangeDbAndReturnNull_IfEntryDoesNotExist()
	{
		// Arrange
		var categoryRepo = new CategoryProdCatRepo(context);
		var manufacturerRepo = new ManufacturerProdCatRepo(context);
		var productRepo = new ProductProdCatRepo(context);
		categoryRepo.Create(new Category { Name = "Test Category" });
		manufacturerRepo.Create(new Manufacturer { Name = "Test Manufacturer" });
		productRepo.Create(new Product { Title = "Test", Description = "Test", Price = 99, CategoryId = 1, ManufacturerId = 1 });
		// Act
		var result = productRepo.Delete(p => p.ArticleNumber == 2);
		var allEntries = productRepo.ReadAll();
		// Assert
		Assert.Null(result);
		Assert.Single(allEntries);
		Assert.Equal(1, allEntries.First().ArticleNumber);
	}

}
