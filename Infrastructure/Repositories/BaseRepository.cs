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

	/// <summary>
	/// Reads all entities from database
	/// </summary>
	/// <returns>A IEnumerable with all entities</returns>
	public virtual IEnumerable<TEntity> ReadAll()
	{
		try
		{
			return _context.Set<TEntity>().ToList();
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method ReadAll: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Updates first entity that fits a query expression to the values in a provided entity
	/// </summary>
	/// <param name="expression">The query expression of the entity to update</param>
	/// <param name="updatedEntity"></param>
	/// <returns>The updated entity if the entity is found and updated, otherwise null</returns>
	public virtual TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity) 
	{
		try
		{
			var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
			if (entityToUpdate != null)
			{
				_context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
				_context.SaveChanges();
				return entityToUpdate;
			}
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method Update: " + ex.Message);
		}
		return null!;
	}

	/// <summary>
	/// Deletes first entry that fits a query expression from database
	/// </summary>
	/// <param name="expression">Query expression</param>
	/// <returns>The entity that was deleted from database if successful, otherwise null</returns>
	public virtual TEntity Delete(Expression<Func<TEntity, bool>> expression)
	{
		try
		{
			var entity = _context.Set<TEntity>().FirstOrDefault(expression);
			if(entity != null)
			{
				_context.Remove(entity);
				_context.SaveChanges();
				return entity;
			}
		}
		catch (Exception ex)
		{
			Debug.Write("Error in method Delete: " + ex.Message);
		}
		return null!;
	}

}
