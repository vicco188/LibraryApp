using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Test.Repositories;

public class BaseRepository_Tests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldCreateEntity_AndReturnCompleteEntity()
	{
		// Arrange
		var repo = new GenreRepository(context);
		var entity = new GenreEntity { Name = "Test genre" };
		// Act
		var result = repo.Create(entity);
		// Assert
		Assert.Equal(1, result.Id);
		Assert.Equal("Test genre", result.Name);
	}
	[Fact]
	public void Create_ShouldCreateSecondEntity_AndReturnCompleteEntity()
	{
		// Arrange
		var repo = new GenreRepository(context);
		var entity1 = new GenreEntity { Name = "Test genre 1" };
		var entity2 = new GenreEntity { Name = "Test genre 2" };
		// Act
		var result1 = repo.Create(entity1);
		var result2 = repo.Create(entity2);
		// Assert
		Assert.Equal(1, result1.Id);
		Assert.Equal("Test genre 1", result1.Name);
		Assert.Equal(2, result2.Id);
		Assert.Equal("Test genre 2", result2.Name);
	}

	[Fact]
	public void Create_ShouldReturnNull_IfEntityAlreadeExists()
	{
		// Arrange
		var repo = new GenreRepository(context);
		var entity = new GenreEntity { Name = "Test genre" };
		repo.Create(entity);
		// Act
		var result = repo.Create(entity);
		// Assert
		Assert.Null(result);
	}


}
