using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductProdCatRepo(ProductDbContext context) : BaseProdCatRepo<Product>(context)
{
	private readonly ProductDbContext _context = context;

	/// <summary>
	/// Gets a product from database that fits a lambda expression
	/// </summary>
	/// <param name="expression">The lambda expression that corresponds to the desired product</param>
	/// <returns>A Product entity if successful, otherwise null</returns>
	public override Product Read(Expression<Func<Product, bool>> expression)
	{
		try
		{
			var entity = _context.Products
				.Include(e => e.Category)
				.Include(e => e.Manufacturer)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method ProductProdCatRepo.Read: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Gets all products from database
	/// </summary>
	/// <returns>An IEnumerable of Product entitites</returns>
	public override IEnumerable<Product> ReadAll()
	{
		try
		{
			return _context.Products
				.Include(e => e.Category)
				.Include(e => e.Manufacturer)
				.ToList();
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method ProductProdCatRepo.ReadAll: " + ex.Message);
		}
		return null!;
	}



}