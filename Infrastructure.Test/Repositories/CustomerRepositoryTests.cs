using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class CustomerRepositoryTests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldCreateEntry_AndReturnCompleteEntity()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		var entity = new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" };
		// Act
		var result = repo.Create(entity);
		// Assert
		Assert.Equal(1, result.Id);
		Assert.Equal("Test", result.FirstName);
	}

	[Fact]
	public void Create_ShouldCreateSecondEntry_AndReturnCompleteEntity()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		var entity1 = new CustomerEntity { FirstName = "Test1", LastName = "Test1", Email="test1@test.com" };
		var entity2 = new CustomerEntity { FirstName = "Test2", LastName = "Test2", Email = "test2@test.com" };
		// Act
		var result1 = repo.Create(entity1);
		var result2 = repo.Create(entity2);
		// Assert
		Assert.Equal(1, result1.Id);
		Assert.Equal("Test1", result1.FirstName);
		Assert.Equal(2, result2.Id);
		Assert.Equal("Test2", result2.FirstName);
	}

	[Fact]
	public void Create_ShouldReturnNull_IfEntryAlreadyExists()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		var entity = new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" };
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
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result1 = repo.Read(e => e.FirstName == "Test");
		var result2 = repo.Read(e => e.Id == 1);
		// Assert
		Assert.Equal("Test", result1.FirstName);
		Assert.Equal(1, result1.Id);
		Assert.Equal("Test", result2.FirstName);
		Assert.Equal(1, result2.Id);
	}
	[Fact]
	public void Read_ShouldReturnNull_IfEntityDoesNotExist()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		// Act
		var result1 = repo.Read(e => e.FirstName == "Test");
		var result2 = repo.Read(e => e.Id == 1);
		// Assert
		Assert.Null(result1);
		Assert.Null(result2);
	}

	[Fact]
	public void ReadAll_ShouldReturnEmptyList_IfDbIsEmpty()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
		Assert.Empty(result);
	}

	[Fact]
	public void ReadAll_ShouldReturnList_WithAllEntries()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test1", LastName = "Test1", Email = "test1@test.com" });
		repo.Create(new CustomerEntity { FirstName = "Test2", LastName = "Test2", Email = "test2@test.com" });
		// Act
		var result = repo.ReadAll();
		// Assert
		Assert.Equal("Test1", result.First().FirstName);
		Assert.Equal(2, result.Count());
	}

	[Fact]
	public void Update_ShouldUpdateInDbAndReturnUpdatedEntity_IfEntryExists()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		var entityToUpdate = repo.Read(e => e.Id == 1);
		entityToUpdate.FirstName = "Test2";
		// Act
		var result = repo.Update(e => e.Id == 1, entityToUpdate);

		// Assert
		Assert.Equal("Test2", result.FirstName);
		Assert.Equal("Test2", repo.Read(e => e.Id == 1).FirstName);
	}

	[Fact]
	public void Update_ShoudNotUpdateInDbReturnNull_IfEntryDoesNotExist()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		var entityToUpdate = repo.Read(e => e.Id == 1);
		entityToUpdate.FirstName = "Test2";
		// Act
		var result = repo.Update(e => e.Id == 2, entityToUpdate);
		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Delete_ShouldDeleteInDbAndReturnEntity_IfEntryExists()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test1", LastName = "Test1", Email = "test1@test.com" });
		repo.Create(new CustomerEntity { FirstName = "Test2", LastName = "Test2", Email = "test2@test.com" });
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
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result = repo.Delete(e => e.Id == 2);
		var allEntries = repo.ReadAll();
		// Assert
		Assert.Null(result);
		Assert.Single(allEntries);
		Assert.Equal(1, allEntries.First().Id);
	}

	[Fact]
	public void Exists_ShouldReturnTrueIfEntryExists_ShouldReturnFalseIfEntryDoesNotExist()
	{
		// Arrange
		var repo = new CustomerRepository(context);
		repo.Create(new CustomerEntity { FirstName = "Test1", LastName = "Test1", Email = "test1@test.com" });
		// Act
		var result1a = repo.Exists(e => e.Id == 1);
		var result1b = repo.Exists(e => e.FirstName == "Test1");
		var result2a = repo.Exists(e => e.Id == 2);
		var result2b = repo.Exists(e => e.FirstName == "Test2");
		// Assert
		Assert.True(result1a);
		Assert.True(result1b);
		Assert.False(result2a);
		Assert.False(result2b);
	}
}
