using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<CustomerEntity>
{
	private readonly LibDbContext _context;

	public CustomerRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
	/// <summary>
	/// Gets a CustomerEntity from database that fits an lambda expression
	/// </summary>
	/// <param name="expression">The lambda expression that fits the customer</param>
	/// <returns>A CustomerEntity if successful, otherwise null</returns>
	public override CustomerEntity Read(Expression<Func<CustomerEntity, bool>> expression)
	{
		try
		{
			var entity = _context.Customers
				.Include(e => e.Loans)
				.ThenInclude(loan => loan.Book)
				.Include(e => e.Loans)
				.ThenInclude(loan => loan.Book.Author)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method CustomerRepository.Read: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Gets all CustomerEntity entries from database
	/// </summary>
	/// <returns>An IEnumerable of all CustomerEntity in database</returns>
	public override IEnumerable<CustomerEntity> ReadAll()
	{
		try
		{
			return _context.Customers
				.Include(e => e.Loans)
				.ThenInclude(loan => loan.Book)
				.Include(e => e.Loans)
				.ThenInclude(loan=>loan.Book.Author)
				
				.ToList();
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method CustomerRepository.ReadAll: " + ex.Message);
		}
		return null!;
	}
}