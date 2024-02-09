using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ProductService(ProductProdCatRepo productRepository, CategoryProdCatRepo categoryRepository, ManufacturerProdCatRepo manufacturerRepository)
{
	private readonly ProductProdCatRepo _productRepository = productRepository;
	private readonly CategoryProdCatRepo _categoryRepository = categoryRepository;
	private readonly ManufacturerProdCatRepo _manufacturerRepository = manufacturerRepository;

	/// <summary>
	/// Creates a new product entry in database
	/// </summary>
	/// <param name="title">Title of the product</param>
	/// <param name="description">Description of the product</param>
	/// <param name="price">The price of the product</param>
	/// <param name="category">The category of the product</param>
	/// <param name="manufacturer">The mannufacturer of the product</param>
	/// <returns>The Product entity that was entered into the database if successful, otherwise null</returns>
	public Product CreateProduct(string title, string description, decimal price, string category, string manufacturer)
	{
		try
		{
			int categoryId;
			if (_categoryRepository.Exists(c => c.Name == category)) categoryId = _categoryRepository.Read(c => c.Name == category).Id;
			else categoryId = _categoryRepository.Create(new Category { Name = category }).Id;

			int manufacturerId;
			if (_manufacturerRepository.Exists(m => m.Name == manufacturer)) manufacturerId = _manufacturerRepository.Read(m => m.Name == manufacturer).Id;
			else manufacturerId = _manufacturerRepository.Create(new Manufacturer { Name = manufacturer }).Id;

			return _productRepository.Create(new Product { Title = title, Description = description, Price = price, CategoryId = categoryId, ManufacturerId = manufacturerId });
		}
		catch (Exception ex) { Debug.Write("Error in method CreateProduct : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets a product from the database
	/// </summary>
	/// <param name="articleNumber">The article number of the product to get</param>
	/// <returns>The requested Product entry if successful, otherwise null</returns>
	public Product GetProduct(int articleNumber)
	{
		try
		{
			return _productRepository.Read(p => p.ArticleNumber == articleNumber);
		}
		catch (Exception ex) { Debug.Write("Error in method GetProduct : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets all products entered in database
	/// </summary>
	/// <returns>An IEnumerable of all Product entities in database</returns>
	public IEnumerable<Product> GetAllProducts()
	{
		try
		{
			return _productRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllProducts : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Updates a products information in database
	/// </summary>
	/// <param name="product">The Product entity to be updated</param>
	/// <param name="title">The new title</param>
	/// <param name="description">The new description</param>
	/// <param name="price">The new price</param>
	/// <param name="category">The new category name</param>
	/// <param name="manufacturer">The new manufacturer name</param>
	/// <returns>The updated Product entity that was enterd into database if successful, otherwise null</returns>
	public Product UpdateProduct(Product product, string title, string? description, decimal price, string category, string manufacturer)
	{
		try
		{
			int categoryId;
			if (_categoryRepository.Exists(c => c.Name == category)) categoryId = _categoryRepository.Read(c => c.Name == category).Id;
			else categoryId = _categoryRepository.Create(new Category { Name = category }).Id;

			int manufacturerId;
			if (_manufacturerRepository.Exists(m => m.Name == manufacturer)) manufacturerId = _manufacturerRepository.Read(m => m.Name == manufacturer).Id;
			else manufacturerId = _manufacturerRepository.Create(new Manufacturer { Name = manufacturer }).Id;

			product.Title = title;
			product.Description= description;
			product.Price = price;
			product.CategoryId= categoryId;
			product.ManufacturerId= manufacturerId;
			return _productRepository.Update(p => p.ArticleNumber == product.ArticleNumber, product);
		}
		catch (Exception ex) { Debug.Write("Error in method UpdateProduct : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Deletes a Product entry from database
	/// </summary>
	/// <param name="articleNumber">The article number of the product to delete</param>
	/// <returns>The deleted Product entity if successful, otherwise null</returns>
	public Product DeleteProduct(int articleNumber)
	{
		try
		{
			return _productRepository.Delete(p => p.ArticleNumber == articleNumber);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteProduct : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets a Category entity from database by name
	/// </summary>
	/// <param name="categoryName">The name of the category to fetch</param>
	/// <returns>A Category entity with the matching name if successful, otherwise null</returns>
	public Category ViewCategory(string categoryName)
	{
		try
		{
			return _categoryRepository.Read(c => c.Name.ToLower() == categoryName.ToLower());
		}
		catch (Exception ex) { Debug.Write("Error in method ViewCategory : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets a Manufacturer entity from database by name
	/// </summary>
	/// <param name="manufactruerName">The name of the manufacturer to fetch</param>
	/// <returns>A Manufacturer entity with the matching name if successful, otherwise null</returns>
	public Manufacturer ViewManufacturer(string manufacturerName)
	{
		try
		{
			return _manufacturerRepository.Read(m => m.Name.ToLower() == manufacturerName.ToLower());
		}
		catch (Exception ex) { Debug.Write("Error in method ViewCategory : " + ex.Message); }
		return null!;
	}
}
