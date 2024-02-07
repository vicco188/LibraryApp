using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<BookEntity>
{
	private readonly LibDbContext _context;

	public BookRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}

	/// <summary>
	/// Gets a book entity that fits a query expression
	/// </summary>
	/// <param name="expression">The query expression</param>
	/// <returns>A book entity if successful, otherwise returns null</returns>
	public override BookEntity Read(Expression<Func<BookEntity, bool>> expression)
	{
		try
		{
			var entity = _context.Books
				.Include(e => e.Author)
				.Include(e => e.Publisher)
				.Include(e => e.Genre)
				.Include(e => e.Language)
				.Include(e => e.Loan)
				.ThenInclude(loan => loan!.Customer)
				.FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch(Exception ex)
		{
			Debug.Write("Error in method BookRepository.Read: " + ex.Message);
		}
		return null!;
	}


	/// <summary>
	/// Gets an all book entries in database
	/// </summary>
	/// <returns>An IEnumerable containing BookEntity</returns>
	public override IEnumerable<BookEntity> ReadAll()
	{
		try
		{
			return _context.Books
				.Include(e => e.Author)
				.Include(e => e.Publisher)
				.Include(e => e.Genre)
				.Include(e => e.Language)
				.Include(e => e.Loan)
				.ThenInclude(loan => loan!.Customer)
				.ToList();
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method BookRepository.ReadAll: " + ex.Message);
		}
		return null!;
	}
}

