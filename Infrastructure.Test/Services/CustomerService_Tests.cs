using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services;

public class CustomerService_Tests
{

	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void CreateCustomer_ShouldAddCustomerToDb_AndReturnNewCustomer()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		var customer = new CustomerEntity { FirstName = "Test", LastName ="Test", Email ="test@test.com" };
		// Act
		var result = customerService.CreateCustomer(customer);
		// Assert
		Assert.Equal(1, result.Id);
	}

	[Fact]
	public void CreateCustomer_ShouldReturnNull_IfEntryWithSameEmailExists()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result = customerService.CreateCustomer(new CustomerEntity { FirstName = "Test2", LastName = "Test2", Email = "test@test.com" });
		// Assert
		Assert.Null(result);
	}

}
