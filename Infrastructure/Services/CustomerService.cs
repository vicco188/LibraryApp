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
}
