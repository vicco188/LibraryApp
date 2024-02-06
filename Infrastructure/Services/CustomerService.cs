using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository)
{
	private readonly CustomerRepository _customerRepository = customerRepository;
}
