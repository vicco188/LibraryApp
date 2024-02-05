using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class BookRepository_Tests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);


	[Fact]
	public void Read_ShouldReturnEntityIfEntryExists_OtherwiseReturnNull()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		var repo = new BookRepository(context);
		var bookEntity = new BookEntity
		{
			Title = "Test Title",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		};
		repo.Create(bookEntity);
		// Act
		var result1 = repo.Read(e => e.Id == 1);
		var result2 = repo.Read(e => e.AuthorId == 1);
		var result3 = repo.Read(e => e.Publisher.Name == "Publisher Test");
		var result4 = repo.Read(e => e.GenreId == 2);
		// Assert
		Assert.Equal("Genre Test", result1.Genre.Name);
		Assert.Equal(1, result2.Id);
		Assert.Equal(1, result3.LanguageId);
		Assert.Null(result4);
	}

	[Fact]
	public void ReadAll_ShouldReturnListOfBookEntities()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		var repo = new BookRepository(context);
		repo.Create(new BookEntity
		{
			Title = "Test Title 1",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		repo.Create(new BookEntity
		{
			Title = "Test Title 2",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.Equal(2, result.Count());
		Assert.IsAssignableFrom<IEnumerable<BookEntity>>(result);
		Assert.Equal("Test Title 1", result.First().Title);
	}
	[Fact]
	public void ReadAll_ShouldReturnEmptyList_IfNoEntriesInDatabase()
	{
		// Arrange
		var repo = new BookRepository(context);
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.Empty(result);
		Assert.IsAssignableFrom<IEnumerable<BookEntity>>(result);
	}
}
