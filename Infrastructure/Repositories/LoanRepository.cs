using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class LoanRepository : BaseRepository<LoanEntity>
{
	private readonly LibDbContext _context;

	public LoanRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}