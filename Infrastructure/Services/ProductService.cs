using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ProductService(ProductProdCatRepo productRepository, CategoryProdCatRepo categoryRepository, ManufacturerProdCatRepo manufacturerRepository)
{
	private readonly ProductProdCatRepo _productRepository = productRepository;
	private readonly CategoryProdCatRepo _categoryRepository = categoryRepository;
	private readonly ManufacturerProdCatRepo _manufacturerRepository = manufacturerRepository;

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

	public Product GetProduct(int articleNumber)
	{
		try
		{
			return _productRepository.Read(p => p.ArticleNumber == articleNumber);
		}
		catch (Exception ex) { Debug.Write("Error in method GetProduct : " + ex.Message); }
		return null!;
	}

	public IEnumerable<Product> GetAllProducts()
	{
		try
		{
			return _productRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllProducts : " + ex.Message); }
		return null!;
	}

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

	public Product DeleteProduct(int articleNumber)
	{
		return null!;
	}
}
