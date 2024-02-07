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

	[Fact]
	public void GetCustomer_ShouldReturnCustomerIfCustomerExists_OtherwiseReturnNull()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result1 = customerService.GetCustomer(1);
		var result2 = customerService.GetCustomer(2);
		// Assert
		Assert.IsType<CustomerEntity>(result1);
		Assert.Equal(1, result1.Id);
		Assert.Null(result2);
	}

	[Fact]
	public void GetAllCustomers_ShouldReturnListOfCustomers() 
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result = customerService.GetAllCustomers();
		// Assert
		Assert.Single(result);
		Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
	}

	[Fact]
	public void GetAllCustomers_ShouldReturnEmptyListOfCustomers_IfNoCustomersInDb()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		// Act
		var result = customerService.GetAllCustomers();
		// Assert
		Assert.Empty(result);
		Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
	}

	[Fact]
	public void DeleteCustomer_ShouldDeleteCustomerThenReturnEntity_OrReturnNullIfCustomerDoesNotExist()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result1 = customerService.DeleteCustomer(1);
		var result2 = customerService.DeleteCustomer(2);
		var result3 = customerService.GetAllCustomers();
		// Assert
		Assert.Equal(1, result1.Id);
		Assert.Null(result2);
		Assert.Empty(result3);
	}
	[Fact]
	public void UpdateCustomer_ShouldUpdateCustomer_AndReturnCustomerEntity()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		var customer = customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		
		customer.FirstName = "Test2";
		// Act
		var result = customerService.UpdateCustomer(customer);
		var result2 = customerService.GetCustomer(1);
		// Assert
		Assert.Equal("Test2", result.FirstName);
		Assert.Equal("Test2", result2.FirstName);

	}

	[Fact]
	public void UpdateCustomer_ShoudReturnNull_IfCustomerDoesNotExist()
	{
		// Arrange
		var customerRepository = new CustomerRepository(context);
		var customerService = new CustomerService(customerRepository);
		// Act
		var result = customerService.UpdateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Assert
		Assert.Null(result);
	}
}
