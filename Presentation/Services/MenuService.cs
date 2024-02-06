using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.Services;

public class MenuService(BookService bookService, CustomerService customerService, LoanService loanService)
{
	private readonly BookService _bookService = bookService;
	private readonly CustomerService _customerService = customerService;
	private readonly LoanService _loanService = loanService;

	public void AddCustomer()
	{
		var entity = new CustomerEntity { FirstName = "Test FName", LastName = "Test LName", Email = "Test Email2" };
		var result = _customerService.CreateCustomer(entity);

		if (result != null)
			Console.WriteLine($"Kunden {result.Id} skapades.");
		else
			Console.WriteLine("Något gick fel. Kontrollera att ingen kund med samma epost existerar");

		
	}

}
