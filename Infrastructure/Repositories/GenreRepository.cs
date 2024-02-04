using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class GenreRepository : BaseRepository<GenreEntity>
{
	private readonly LibDbContext _context;

	public GenreRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}
