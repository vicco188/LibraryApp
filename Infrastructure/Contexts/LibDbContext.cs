using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Contexts;

public class LibDbContext(DbContextOptions<LibDbContext> options) : DbContext(options)
{
	public virtual DbSet<AuthorEntity> Authors { get; set; }
	public virtual DbSet<GenreEntity> Genres { get; set; }
	public virtual DbSet<LanguageEntity> Languages { get; set; }
	public virtual DbSet<PublisherEntity> Publishers { get; set; }
	public virtual DbSet<BookEntity> Books { get; set; }
	public virtual DbSet<CustomerEntity> Customers { get; set; }
	public virtual DbSet<LoanEntity> Loans { get; set; }
}
