using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class LoanRepository : BaseRepository<LoanEntity>
{
	private readonly LibDbContext _context;

	public LoanRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}

	/// <summary>
	/// Gets a loan entity that fits a query expression
	/// </summary>
	/// <param name="expression">The query expression</param>
	/// <returns>A loan entity if successful, otherwise returns null</returns>
	public override LoanEntity Read(Expression<Func<LoanEntity, bool>> expression)
	{
		try
		{
			var entity = _context.Loans
				.Include(e => e.Book)
				.Include(e => e.Customer)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method LoanRepository.Read: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Gets an all loan entries in database
	/// </summary>
	/// <returns>An IEnumerable containing LoanEntity</returns>
	public override IEnumerable<LoanEntity> ReadAll()
	{
		try
		{
			return _context.Loans
				.Include(e => e.Book)
				.Include(e => e.Customer)
				.ToList();
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method LoanRepository.ReadAll: " + ex.Message);
		}
		return null!;
	}

}

