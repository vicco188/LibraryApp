using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CategoryProdCatRepo(ProductDbContext context) : BaseProdCatRepo<Category>(context)
{
	private readonly ProductDbContext _context = context;

	/// <summary>
	/// Gets a category from database that fits a lambda expression
	/// </summary>
	/// <param name="expression">The lambda expression that corresponds to the desired category</param>
	/// <returns>A Category entity if successful, otherwise null</returns>
	public override Category Read(Expression<Func<Category, bool>> expression)
	{
		try
		{
			var entity = _context.Categories
				.Include(c=>c.Products)
				.ThenInclude(product => product.Manufacturer)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method CategoryRepository.Read: " + ex.Message);
		}
		return null!;
	}
}
