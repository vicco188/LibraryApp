using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

internal class CategoryProdCatRepo(ProductDbContext context) : BaseProdCatRepo<Category>(context)
{
	private readonly ProductDbContext _context = context;
}
