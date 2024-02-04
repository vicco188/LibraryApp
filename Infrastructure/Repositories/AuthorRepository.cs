using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AuthorRepository : BaseRepository<AuthorEntity>
{
	private readonly LibDbContext _context;

	public AuthorRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}

