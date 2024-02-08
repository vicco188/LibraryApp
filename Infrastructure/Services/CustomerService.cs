using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository)
{
	private readonly CustomerRepository _customerRepository = customerRepository;

	/// <summary>
	/// Creates a new customer
	/// </summary>
	/// <param name="customer">A CustomerEntity containing customer information</param>
	/// <returns>The CustomerEntity that was added in database if successful, otherwise null</returns>
	public CustomerEntity CreateCustomer(CustomerEntity customer)
	{
		
		try
		{
			if (!_customerRepository.Exists(c => c.Email == customer.Email))
			{
				return _customerRepository.Create(customer);
			}
		}
		catch(Exception ex) { Debug.Write("Error in method CreateCustomer : " + ex.Message); }
		return null!;

	}

	/// <summary>
	/// Gets customer by id
	/// </summary>
	/// <param name="customerId">Id of customer to get</param>
	/// <returns>A CustomerEntity</returns>
	public CustomerEntity GetCustomer(int customerId)
	{
		try
		{
			return _customerRepository.Read(c => c.Id == customerId);
		}
		catch (Exception ex) { Debug.Write("Error in method GetCustomer : " + ex.Message); }
		return null!;

	}

	/// <summary>
	/// Gets a list of all customers
	/// </summary>
	/// <returns>An IEnumerable with all CustomerEntity entries in database</returns>
	public IEnumerable<CustomerEntity> GetAllCustomers() 
	{
		try
		{
			return _customerRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllCustomers : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Deletes a customer from database
	/// </summary>
	/// <param name="customerId">The Id of the customer to delete</param>
	/// <returns>The CustomerEntity that was deleted from database if successful, otherwise null</returns>
	public CustomerEntity DeleteCustomer(int customerId)
	{
		try
		{
			return _customerRepository.Delete(c => c.Id == customerId);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteCustomer : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Updates a customer in database
	/// </summary>
	/// <param name="customer">The customer entity to be updated (with modified fields)</param>
	/// <returns>The updated CustomerEntity that was entered into the database if successful, otherwise null</returns>
	public CustomerEntity UpdateCustomer(CustomerEntity customer)
	{
		try
		{
			return _customerRepository.Update(c => c.Id == customer.Id, customer);
		}
		catch (Exception ex) { Debug.Write("Error in method UpdateCustomer : " + ex.Message); }
		return null!;
	}
}
