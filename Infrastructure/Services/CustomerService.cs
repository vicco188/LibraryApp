using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository)
{
	private readonly CustomerRepository _customerRepository = customerRepository;

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

	public CustomerEntity GetCustomer(int customerId)
	{
		try
		{
			return _customerRepository.Read(c => c.Id == customerId);
		}
		catch (Exception ex) { Debug.Write("Error in method GetCustomer : " + ex.Message); }
		return null!;

	}

	public IEnumerable<CustomerEntity> GetAllCustomers() 
	{
		try
		{
			return _customerRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllCustomers : " + ex.Message); }
		return null!;
	}

	public CustomerEntity DeleteCustomer(int customerId)
	{
		try
		{
			return _customerRepository.Delete(c => c.Id == customerId);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteCustomer : " + ex.Message); }
		return null!;
	}

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
