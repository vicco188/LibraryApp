using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<BookEntity>
{
	private readonly LibDbContext _context;

	public BookRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}

