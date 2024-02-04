using Infrastructure.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
	private readonly LibDbContext _context;
	protected BaseRepository(LibDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Adds a TEntity to the database
	/// </summary>
	/// <param name="entity">The TEntity to be added</param>
	/// <returns>The created entity if successful, otherwise null</returns>
	public virtual TEntity Create(TEntity entity)
	{
		try
		{
			_context.Add(entity);
			_context.SaveChanges();
			return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method Create: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Gets a TEntity from the database that fits a query expression
	/// </summary>
	/// <param name="expression">The query expression</param>
	/// <returns>The first fitting TEntity if successful, otherwise null</returns>
	public virtual TEntity Read(Expression<Func<TEntity, bool>> expression)
	{
		try
		{
			var entity = _context.Set<TEntity>().FirstOrDefault(expression);
			if (entity != null)
				return entity;
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method Read: " + ex.Message);
		}

		return null!;

	}

}
