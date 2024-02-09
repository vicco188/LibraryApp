using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ManufacturerProdCatRepo(ProductDbContext context) : BaseProdCatRepo<Manufacturer>(context)
{
	private readonly ProductDbContext _context = context;

	/// <summary>
	/// Gets a manufacturer from database that fits a lambda expression
	/// </summary>
	/// <param name="expression">The lambda expression that corresponds to the desired manufacturer</param>
	/// <returns>A Manufacturer entity if successful, otherwise null</returns>
	public override Manufacturer Read(Expression<Func<Manufacturer, bool>> expression)
	{
		try
		{
			var entity = _context.Manufacturers
				.Include(c => c.Products)
				.ThenInclude(product => product.Category)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method ManufacturerRepository.Read: " + ex.Message);
		}
		return null!; ;
	}
}