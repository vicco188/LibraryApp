using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class PublisherRepository : BaseRepository<PublisherEntity>
{
	private readonly LibDbContext _context;

	public PublisherRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}