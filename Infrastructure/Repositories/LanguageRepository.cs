using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class LanguageRepository : BaseRepository<LanguageEntity>
{
	private readonly LibDbContext _context;

	public LanguageRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}