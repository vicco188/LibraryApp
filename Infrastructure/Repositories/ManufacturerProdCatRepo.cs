using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

internal class ManufacturerProdCatRepo(ProductDbContext context) : BaseProdCatRepo<Manufacturer>(context)
{
	private readonly ProductDbContext _context = context;
}