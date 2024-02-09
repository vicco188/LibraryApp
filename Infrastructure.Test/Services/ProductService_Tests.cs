using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services;

public class ProductService_Tests
{
	private readonly ProductDbContext context = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
	.UseInMemoryDatabase($"{Guid.NewGuid()}")
	.Options);

	[Fact]
	public void CreateProduct_ShouldAddProductToDatabase_AndReturnProduct()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		// Act
		var result = productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest");
		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.CategoryId);
	}

	[Fact]
	public void CreateProduct_ShouldUseCurrentCategory_IfCategoryAlreadyExist()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		categoryRepository.Create(new Category { Name = "categorytest" });
		manufacturerRepository.Create(new Manufacturer { Name = "manufacturertest" });
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		// Act
		var result = productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest2");
		// Assert
		Assert.Equal(1, result.CategoryId);
		Assert.Equal(2, result.ManufacturerId);
	}

	[Fact]
	public void GetProduct_ShouldReturnProductIfExists_OtherwiseReturnNull()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		categoryRepository.Create(new Category { Name = "categorytest" });
		manufacturerRepository.Create(new Manufacturer { Name = "manufacturertest" });
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest");
		// Act
		var result1 = productService.GetProduct(1);
		var result2 = productService.GetProduct(2);
		// Assert
		Assert.NotNull(result1);
		Assert.Equal("manufacturertest", result1.Manufacturer.Name);
		Assert.Null(result2);
	}

	[Fact]
	public void GetAllProducts_ShouldReturnAListOfProducts()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		categoryRepository.Create(new Category { Name = "categorytest" });
		manufacturerRepository.Create(new Manufacturer { Name = "manufacturertest" });
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest");
		// Act
		var result = productService.GetAllProducts();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<Product>>(result);
		Assert.Single(result);
		Assert.Equal(1, result.First().ArticleNumber);
	}

	[Fact]
	public void GetAllProducts_ShouldReturnEmptyListOfProducts_IfNoEntriesInDb()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		// Act
		var result = productService.GetAllProducts();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<Product>>(result);
		Assert.Empty(result);
	}


	[Fact]
	public void UpdateProduct_ShouldUpdateInDb_AndReturnUpdatedProduct()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest");
		var product = productService.GetProduct(1);
		// Act
		productService.UpdateProduct(product, "titletest2", "descriptiontest2", 222, "categorytest2", "manufacturertest");
		var result = productService.GetProduct(1);
		// Assert
		Assert.Equal(1, product.ManufacturerId);
		Assert.Equal(2, product.CategoryId);
		Assert.Equal("titletest2", product.Title);
	}

	[Fact]
	public void DeleteProduct_ShouldDeleteProductThenReturnEntity_OrReturnNullIfProductDoesNotExist()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "categorytest", "manufacturertest");
		// Act
		var result1 = productService.DeleteProduct(1);
		var result2 = productService.DeleteProduct(2);
		var result3 = productService.GetAllProducts();
		// Assert
		Assert.Equal(1, result1.ArticleNumber);
		Assert.Null(result2);
		Assert.Empty(result3);
	}

	[Fact]
	public void ViewCategory_ShouldReturnCategoryIfCategoryWithSameNameExists_OtherwiseReturnNull()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "Categorytest", "Manufacturertest");
		// Act
		var result1 = productService.ViewCategory("cAtEGoRyTeSt");
		var result2 = productService.ViewCategory("IDontExist");
		// Assert
		Assert.Equal("Categorytest", result1.Name);
		Assert.Null(result2);
	}

	[Fact]
	public void ViewManufacturer_ShouldReturnManufacturerIfManufacturerWithSameNameExists_OtherwiseReturnNull()
	{
		// Arrange
		var categoryRepository = new CategoryProdCatRepo(context);
		var manufacturerRepository = new ManufacturerProdCatRepo(context);
		var productRepository = new ProductProdCatRepo(context);
		var productService = new ProductService(productRepository, categoryRepository, manufacturerRepository);
		productService.CreateProduct("titletest", "descriptiontest", 111, "Categorytest", "Manufacturertest");
		// Act
		var result1 = productService.ViewManufacturer("mAnUfAcTuReRtEsT");
		var result2 = productService.ViewManufacturer("IDontExist");
		// Assert
		Assert.Equal("Manufacturertest", result1.Name);
		Assert.Null(result2);
	}
}
