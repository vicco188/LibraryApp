using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<CustomerEntity>
{
	private readonly LibDbContext _context;

	public CustomerRepository(LibDbContext context) : base(context)
	{
		_context = context;
	}
}