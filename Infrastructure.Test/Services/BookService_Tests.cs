using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services;

public class BookService_Tests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldAddBookToDb_AndReturnBookEntity()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		// Act
		var result = bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		var result2 = bookService.CreateBook("Det", "Stephen", "King", "Simon and Schuster", "Horror", "Swedish");
		// Assert
		Assert.Equal("Pet Sematary", result.Title);
		Assert.Equal(1, result.Publisher.Id);
		Assert.Equal(1, result2.Author.Id); // Check that a new author entry was NOT created for the second book
		Assert.Equal(2, result2.Language.Id); // Check that a new language entry WAS created for the second book
	}
}
