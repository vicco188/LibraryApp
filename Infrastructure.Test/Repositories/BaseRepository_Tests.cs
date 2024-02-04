using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class BaseRepository_Tests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldCreateEntry_AndReturnCompleteEntity()
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
	public void Create_ShouldCreateSecondEntry_AndReturnCompleteEntity()
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
	public void Create_ShouldReturnNull_IfEntryAlreadyExists()
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

	[Fact]
	public void Read_ShouldReturnEntity_IfEntryExists()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name" });
		// Act
		var result1 = repo.Read(e => e.Name == "Test name");
		var result2 = repo.Read(e => e.Id == 1);
		// Assert
		Assert.Equal("Test name", result1.Name);
		Assert.Equal(1, result1.Id);
		Assert.Equal("Test name", result2.Name);
		Assert.Equal(1, result2.Id);
	}
	[Fact]
	public void Read_ShouldReturnNull_IfEntityDoesNotExist()
	{
		// Arrange
		var repo = new GenreRepository(context);
		// Act
		var result1 = repo.Read(e => e.Name == "Test name");
		var result2 = repo.Read(e => e.Id == 1);
		// Assert
		Assert.Null(result1);
		Assert.Null(result2);
	}

	[Fact]
	public void ReadAll_ShouldReturnEmptyList_IfDbIsEmpty()
	{
		// Arrange
		var repo = new GenreRepository(context);
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<GenreEntity>>(result);
		Assert.Empty(result);
	}

	[Fact]
	public void ReadAll_ShouldReturnList_WithAllEntries()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name 1" });
		repo.Create(new GenreEntity { Name = "Test name 2" });
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.Equal("Test name 1", result.First().Name);
		Assert.Equal(2, result.Count());
	}

	[Fact]
	public void Update_ShouldUpdateInDbAndReturnUpdatedEntity_IfEntryExists()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name 1" });
		var entityToUpdate = repo.Read(e => e.Id == 1);
		entityToUpdate.Name = "Test name 2";
		// Act
		var result = repo.Update(e => e.Id == 1, entityToUpdate);

		// Assert
		Assert.Equal("Test name 2", result.Name);
		Assert.Equal("Test name 2", repo.Read(e => e.Id == 1).Name);
	}

	[Fact]
	public void Update_ShoudNotUpdateInDbReturnNull_IfEntryDoesNotExist()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name 1" });
		var entityToUpdate = repo.Read(e => e.Id == 1);
		entityToUpdate.Name = "Test name 2";
		// Act
		var result = repo.Update(e => e.Id == 2, entityToUpdate);
		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Delete_ShouldDeleteInDbAndReturnEntity_IfEntryExists()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name 1" });
		repo.Create(new GenreEntity { Name = "Test name 2" });
		// Act
		var result = repo.Delete(e => e.Id == 1);
		var allEntries = repo.ReadAll();
		// Assert
		Assert.Equal(1, result.Id);
		Assert.Single(allEntries);
		Assert.Equal(2, allEntries.First().Id);
	}

	[Fact]
	public void Delete_ShouldNotChangeDbAndReturnNull_IfEntryDoesNotExist()
	{
		// Arrange
		var repo = new GenreRepository(context);
		repo.Create(new GenreEntity { Name = "Test name 1" });
		// Act
		var result = repo.Delete(e => e.Id == 2);
		var allEntries = repo.ReadAll();
		// Assert
		Assert.Null(result);
		Assert.Single(allEntries);
		Assert.Equal(1, allEntries.First().Id);
	}

}
