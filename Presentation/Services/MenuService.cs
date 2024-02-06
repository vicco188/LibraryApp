using Azure;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.Services;

public class MenuService(BookService bookService, CustomerService customerService, LoanService loanService)
{
	private readonly BookService _bookService = bookService;
	private readonly CustomerService _customerService = customerService;
	private readonly LoanService _loanService = loanService;

	public void MainMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Huvudmeny\n=========");
			Console.WriteLine("1. Hantera bibliotekskunder\n2. Hantera Böcker\n3. Hantera lån\n4. Hantera produkter\n9. Avsluta");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: HandleCustomersMenu(); break;
				case 2: HandleBooksMenu(); break;
			}
		} while (response != 9);
	}
	private void HandleCustomersMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Hantera bibliotekskunder\n========================");
			Console.WriteLine("1. Lägg till kund\n2. Visa kund\n3. Visa alla kunder\n4. Ta bort en kund\n5. Uppdatera kund\n9. Gå till huvudmenyn");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: AddCustomerUi(); break;
				case 2: GetCustomerUi(); break;
				case 3: GetAllCustomersUi(); break;
				case 4: DeleteCustomerUi(); break;
				case 5: UpdateCustomerUi(); break;
			}
		} while (response != 9);
	}

	private void HandleBooksMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Hantera böcker\n==============");
			Console.WriteLine("1. Lägg till bok\n2. Visa bok\n3. Visa alla böcker\n4. Ta bort en bok\n5. Uppdatera bok\n9. Gå till huvudmenyn");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: AddBookUi(); break;
				case 2: GetBookUi(); break;
				case 3: GetAllBookUi(); break;
				case 4: DeleteBookUi(); break;
				case 5: UpdateBookUi(); break;
			}
		} while (response != 9);
	}
	private void AddBookUi()
	{
		Console.Clear();
		Console.WriteLine("Lägg till bok \n=============");

		Console.Write("Ange titel: ");
		string title = Console.ReadLine()!;
		Console.Write("Ange författares förnamn: ");
		string authorFirstName = Console.ReadLine()!;
		Console.Write("Ange författares förnamn: ");
		string authorLastName = Console.ReadLine()!; ;
		Console.Write("Ange förlag: ");
		string publisher = Console.ReadLine()!;
		Console.Write("Ange språk: ");
		string language = Console.ReadLine()!;
		Console.Write("Ange kategori: ");
		string genre = Console.ReadLine()!;

		try
		{
			var result = _bookService.CreateBook(title, authorFirstName, authorLastName, publisher, genre, language);
			Console.Clear();
			if (result != null)
				Console.WriteLine($"Boken skapades. Bokens id är {result.Id}");
			else
				Console.WriteLine("Något gick fel.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();

	}
	private void GetBookUi()
	{
		throw new NotImplementedException();
	}
	private void GetAllBookUi()
		{
			throw new NotImplementedException();
		}
	private void UpdateBookUi()
	{
		throw new NotImplementedException();
	}

	private void DeleteBookUi()
	{
		throw new NotImplementedException();
	}

	

	

	



	private void GetKey()
	{
		Console.Write("Tryck någon tangent för att fortsätta . . .");
		Console.ReadKey();
		Console.Clear();
	}
	private void AddCustomerUi()
	{
		Console.Clear();
		Console.WriteLine("Lägg till kund \n==============");
		Console.Write("Ange förnamn: ");
		string firstName = Console.ReadLine()!;
		Console.Write("Ange efterrnamn: ");
		string lastName = Console.ReadLine()!;
		Console.Write("Ange e-post: ");
		string email = Console.ReadLine()!;

		try
		{
			var entity = new CustomerEntity { FirstName = firstName, LastName = lastName, Email = email };
			var result = _customerService.CreateCustomer(entity);
			Console.Clear();
			if (result != null)
				Console.WriteLine($"Kunden skapades. Kundnummer {result.Id}");
			else
				Console.WriteLine("Något gick fel. Kontrollera att ingen kund med samma epost existerar");
		}	
		catch(Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void GetCustomerUi()
	{
		Console.Clear();
		Console.WriteLine("Visa kunduppgifter\n==================");
		Console.Write("Ange kundnummer: ");
		

		try
		{
			int customerId = int.Parse(Console.ReadLine()!);
			var customer = _customerService.GetCustomer(customerId);
			Console.Clear();
			if (customer != null)
			{
				Console.WriteLine("Kunduppgifter \n=============");
				Console.WriteLine($"Kundnummer: {customer.Id}");
				Console.WriteLine($"Förnamn: {customer.FirstName}");
				Console.WriteLine($"Efternamn: {customer.LastName}");
				Console.WriteLine($"E-post: {customer.Email}");
				Console.WriteLine($"Antal lån: {customer.Loans.Count()}");
				foreach (var loan in customer.Loans)
				{
					Console.WriteLine($"{loan.Book.Author.LastName}, {loan.Book.Author.FirstName} - {loan.Book.Title}");
				}
			}
			else
			{
				Console.WriteLine("Kunden hittades inte. ");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}

		GetKey();

	}
	private void GetAllCustomersUi()
	{
		try
		{
			var customerList = _customerService.GetAllCustomers();
			Console.Clear();
			Console.WriteLine("Kundlista\n=========");
			foreach (var customer in customerList)
			{
				Console.WriteLine($"Kundnummer {customer.Id} | {customer.FirstName} {customer.LastName} <{customer.Email}> ({customer.Loans.Count} lån)");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	
	private void UpdateCustomerUi()
	{
		try
		{
			Console.Clear();
			Console.WriteLine("Uppdatera kunduppgifter\n========================");
			Console.Write("Ange kundnummer att uppdatera: ");
			int customerId = int.Parse(Console.ReadLine()!);
			var customerEntity = _customerService.GetCustomer(customerId);
			Console.Clear();
			if (customerEntity == null)
			{
				Console.WriteLine("Kunden hittades inte");
				GetKey();
				return;
			}
			Console.Write($"Ange förnamn (befintligt förnamn: {customerEntity.FirstName}): ");
			var firstName = Console.ReadLine()!;
			Console.Write($"Ange efternamn (befintligt efternamn: {customerEntity.LastName}): ");
			var lastName = Console.ReadLine()!;
			Console.Write($"Ange e-post (befintlig e-post: {customerEntity.Email}): ");
			var email = Console.ReadLine()!;
			Console.Write("Bekräfta uppdatering (y/n): ");
			var response = Console.ReadLine();
			if (response != "y")
			{
				Console.WriteLine("Åtgärden avbruten");
				GetKey();
				return;
			}
			customerEntity.FirstName = firstName;
			customerEntity.LastName = lastName;
			customerEntity.Email = email;
			var result = _customerService.UpdateCustomer(customerEntity);
			Console.Clear();
			if (result != null) Console.WriteLine("Kunden uppdaterades");
			else Console.WriteLine("Något gick tyvärr fel");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void DeleteCustomerUi()
	{
		Console.Clear();
		Console.WriteLine("Ta bort kund\n============");
		try
		{
			Console.Write("Ange kundnummer: ");
			int customerId = int.Parse(Console.ReadLine()!);
			var customer = _customerService.GetCustomer(customerId);
			Console.Clear();
			if (customer == null)
			{
				Console.WriteLine("Kunden hittades inte.");
				GetKey();
				return;
			}
			Console.Write($"Bekräfta borttagning av kund {customerId} {customer.FirstName} {customer.LastName} (y/n): ");
			var response = Console.ReadLine();
			if (response != "y")
				return;

			var result = _customerService.DeleteCustomer(customerId);
			Console.Clear();
			if (result != null)
				Console.WriteLine("Kunden är borttagen");
			else
				Console.WriteLine("Något gick tyvärr fel.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}

		GetKey();


	}
}
