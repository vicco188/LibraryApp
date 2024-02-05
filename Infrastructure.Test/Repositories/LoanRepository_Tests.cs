using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class LoanRepository_Tests
{
	
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);





	[Fact]
	public void Create_ShouldCreateLoan_IfBookIsAvailable()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Cust First Test",
			LastName = "Cust First Test",
			Email = "test@test.com"
		});
		new BookRepository(context).Create(new BookEntity
		{
			Title = "Test Title 1",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		var repo = new LoanRepository(context);

		// Act
		var result = repo.Create(new LoanEntity { BookId = 1, CustomerId = 1 });

		//
		Assert.Equal(1, result.BookId);
		Assert.Equal(1, result.CustomerId);
	}





}
