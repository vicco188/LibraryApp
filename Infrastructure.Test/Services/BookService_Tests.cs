using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;

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

	[Fact]
	public void GetBook_ShouldReturnBookIfExists_OtherwiseReturnNull()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		// Act
		var result = bookService.GetBook(1);
		var result2 = bookService.GetBook(2);
		// Assert
		Assert.NotNull(result);
		Assert.Equal("Pet Sematary", result.Title);
		Assert.Null(result2);
	}

	[Fact]
	public void GetAllBooks_ShouldReturnListOfBooks()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		// Act
		var result = bookService.GetAllBooks();
		// Assert
		Assert.Single(result);
		Assert.IsAssignableFrom<IEnumerable<BookEntity>>(result);
	}

	[Fact]
	public void GetAllBooks_ShouldReturnEmptyListOfBooks_IfNoBooksInDb()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		// Act
		var result = bookService.GetAllBooks();
		// Assert
		Assert.Empty(result);
		Assert.IsAssignableFrom<IEnumerable<BookEntity>>(result);
	}

	[Fact]
	public void UpdateBook_ShouldUpdateinDb_AndReturnUpdatedBook()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		var book = bookService.GetBook(1);
		// Act
		bookService.UpdateBook(book, "Jurtjyrkogården", "Stephen", "King", "Månpocket", "Horror", "Swedish");
		var result = bookService.GetBook(1);
		// Assert
		Assert.Equal(1, book.AuthorId);
		Assert.Equal(2, book.PublisherId);
		Assert.Equal("Jurtjyrkogården", book.Title);
	}


	[Fact]
	public void DeleteBook_ShouldDeleteBookThenReturnEntity_OrReturnNullIfBookDoesNotExist()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		// Act
		var result1 = bookService.DeleteBook(1);
		var result2 = bookService.DeleteBook(2);
		var result3 = bookService.GetAllBooks();
		// Assert
		Assert.Equal(1, result1.Id);
		Assert.Null(result2);
		Assert.Empty(result3);
	}
}
