﻿using Azure;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.Services;

public class MenuService(BookService bookService, CustomerService customerService, LoanService loanService, ProductService productService)
{
	private readonly BookService _bookService = bookService;
	private readonly CustomerService _customerService = customerService;
	private readonly LoanService _loanService = loanService;
	private readonly ProductService _productService = productService;

	public void MainMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Huvudmeny\n=========");
			Console.WriteLine("1. Hantera bibliotekskunder\n2. Hantera böcker\n3. Hantera lån\n4. Hantera produkter\n9. Avsluta");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: HandleCustomersMenu(); break;
				case 2: HandleBooksMenu(); break;
				case 3: HandleLoansMenu(); break;
				case 4: HandleProductsMenu(); break;
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

	private void HandleLoansMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Hantera lån\n===========");
			Console.WriteLine("1. Låna bok\n2. Återlämna bok\n3. Visa alla lån\n9. Gå till huvudmenyn");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: AddLoanUi(); break;
				case 2: DeleteLoanUi(); break;
				case 3: GetAllLoansUi(); break;
			}
		} while (response != 9);
	}

	private void HandleProductsMenu()
	{
		int response;
		do
		{
			Console.Clear();
			Console.WriteLine("BIBLIOTEKSDATABAS\n\n");
			Console.WriteLine("Hantera bibliotekskunder\n========================");
			Console.WriteLine("1. Lägg till produkt\n2. Visa produkt\n3. Visa alla produkter\n4. Ta bort en produkt\n5. Uppdatera produkt\n9. Gå till huvudmenyn");
			Console.Write("Val: ");
			int.TryParse(Console.ReadLine()!, out response);
			switch (response)
			{
				case 1: AddProductUi(); break;
				case 2: GetProductUi(); break;
				case 3: GetAllProductsUi(); break;
				case 4: DeleteProductUi(); break;
				case 5: UpdateProductUi(); break;
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
		Console.Write("Ange författares efternamn: ");
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
		Console.Clear();
		Console.WriteLine("Visa bok\n========");
		Console.Write("Ange boknummer: ");


		try
		{
			int bookId = int.Parse(Console.ReadLine()!);
			var book = _bookService.GetBook(bookId);
			Console.Clear();
			if (book != null)
			{
				Console.WriteLine("Bokuppgifter \n============");
				Console.WriteLine($"Boknummer: {book.Id}");
				Console.WriteLine($"Titel: {book.Title}");
				Console.WriteLine($"Författare: {book.Author.LastName}, {book.Author.FirstName}");
				Console.WriteLine($"Förlag: {book.Publisher.Name}");
				Console.WriteLine($"Kategori: {book.Genre.Name}");
				Console.WriteLine($"Språk: {book.Language.Name}");
				if (book.Loan != null) Console.WriteLine($"Boken är utlånad till {book.Loan.Customer.FirstName} {book.Loan.Customer.LastName} (kundnummer {book.Loan.CustomerId})");
				else Console.WriteLine("Boken är inte utlånad.");

			}
			else
			{
				Console.WriteLine("Bpken hittades inte. ");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}

		GetKey();
	}
	private void GetAllBookUi()
		{
		try
		{
			var bookList = _bookService.GetAllBooks();
			Console.Clear();
			Console.WriteLine("Boklista\n========");
			foreach (var book in bookList)
			{
				Console.Write($"Boknummer {book.Id} | {book.Author.LastName}, {book.Author.FirstName} - {book.Title} ");
				if (book.Loan != null) Console.Write($"(utlånad till kundnummer {book.Loan.CustomerId})\n");
				else Console.Write("(ej utlånad)\n");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void UpdateBookUi()
	{
		try
		{
			Console.Clear();
			Console.WriteLine("Uppdatera bokuppgifter\n=======================");
			Console.Write("Ange boknummer att uppdatera: ");
			int bookId = int.Parse(Console.ReadLine()!);
			var bookEntity = _bookService.GetBook(bookId);
			Console.Clear();
			if (bookEntity == null)
			{
				Console.WriteLine("Boken hittades inte");
				GetKey();
				return;
			}
			Console.Write($"Ange title (befintlig titel: {bookEntity.Title}): ");
			var title = Console.ReadLine()!;
			Console.Write($"Ange författares förnamn (befintligt förnamn: {bookEntity.Author.FirstName}): ");
			var firstName = Console.ReadLine()!;
			Console.Write($"Ange författares efternamn (befintligt efternamn: {bookEntity.Author.LastName}): ");
			var lastName = Console.ReadLine()!;
			Console.Write($"Ange kategori (befintlig kategori: {bookEntity.Genre.Name}): ");
			var genre = Console.ReadLine()!;
			Console.Write($"Ange förlag (befintligt förlag: {bookEntity.Publisher.Name}): ");
			var publisher = Console.ReadLine()!;
			Console.Write($"Ange språk (befintligt språk: {bookEntity.Language.Name}): ");
			var language = Console.ReadLine()!;
			Console.Write("Bekräfta uppdatering (y/n): ");
			var response = Console.ReadLine();
			if (response != "y")
			{
				Console.WriteLine("Åtgärden avbruten");
				GetKey();
				return;
			}

			var result = _bookService.UpdateBook(bookEntity, title, firstName, lastName, publisher, genre, language);
			Console.Clear();
			if (result != null) Console.WriteLine("Boken uppdaterades");
			else Console.WriteLine("Något gick tyvärr fel");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void DeleteBookUi()
	{
		Console.Clear();
		Console.WriteLine("Ta bort bok\n===========");
		try
		{
			Console.Write("Ange boknummer: ");
			int bookId = int.Parse(Console.ReadLine()!);
			var book = _bookService.GetBook(bookId);
			Console.Clear();
			if (book == null)
			{
				Console.WriteLine("Boken hittades inte.");
				GetKey();
				return;
			}
			Console.Write($"Bekräfta borttagning av bok {bookId} [{book.Author.LastName}, {book.Author.FirstName} -  {book.Title}] (y/n): ");
			var response = Console.ReadLine();
			if (response != "y")
			{
				Console.WriteLine("Åtgärden avbruten");
				GetKey();
				return;
			}

			var result = _bookService.DeleteBook(bookId);
			Console.Clear();
			if (result != null)
				Console.WriteLine("Boken är borttagen");
			else
				Console.WriteLine("Något gick tyvärr fel.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}

		GetKey();
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
					Console.WriteLine($"Lånenummer {loan.LoanNumber} | {loan.Book.Author.ToString()} - {loan.Book.Title}");
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
			{
				Console.WriteLine("Åtgärden avbruten");
				GetKey();
				return;
			}

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
	private void AddLoanUi()
	{
		Console.Clear();
		Console.WriteLine("Låna bok \n========");
		try
		{
			Console.Write("Ange kundnummer: ");
			int customerId = int.Parse(Console.ReadLine()!);
			Console.Write("Ange boknummer: ");
			int bookId = int.Parse(Console.ReadLine()!);
			var customer = _customerService.GetCustomer(customerId);
			var book = _bookService.GetBook(bookId);
			Console.Clear();
			if (customer == null)
			{
				Console.WriteLine("Kunden hittades inte");
				GetKey();
				return;
			}
			if (book == null)
			{
				Console.WriteLine("Boken hittades inte");
				GetKey();
				return;
			}
			if (book.Loan != null)
			{
				Console.WriteLine($"Boken är redan utlånad till kundnummer {book.Loan.CustomerId} [{book.Loan.Customer.FirstName} {book.Loan.Customer.LastName}]");
				GetKey();
				return;
			}
			var result = _loanService.CreateLoan(bookId, customerId);
			Console.Clear();
			if (result != null) Console.WriteLine($"Lyckades. {result.Customer.FirstName} {result.Customer.LastName} har lånat {result.Book.Title}.");
			else Console.WriteLine("Något gick tyvärr fel. Kontrollera att boken är ledig.");
		}
		catch (Exception ex){ Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}"); }
		GetKey();
		
	}
	private void DeleteLoanUi()
	{
		Console.Clear();
		Console.WriteLine("Återlämna bok \n=============");
		Console.Write("Ange boknummer: ");
		try
		{
			int bookId = int.Parse(Console.ReadLine()!);
			var loan = _loanService.GetLoan(bookId);
			Console.Clear();
			if (loan == null)
			{
				Console.WriteLine("Boken är inte utlånad");
				GetKey();
				return;
			}
			var result = _loanService.DeleteLoan(bookId);
			Console.Clear();
			if (result != null) Console.WriteLine($"{result.Book.Title} är nu återlämnad.");
			else Console.WriteLine("Något gick tyvärr fel. ");
		}
		catch (Exception ex) { Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}"); }
		GetKey();
	}
	private void GetAllLoansUi()
	{
		try
		{
			IEnumerable<LoanEntity> loanList = _loanService.GetAllLoans();
			Console.Clear();
			Console.WriteLine("Lista över lån\n==============");
			foreach (var loan in loanList)
			{
				
				Console.WriteLine($"Lånenummer {loan.LoanNumber} | Bok {loan.BookId} [{loan.Book.Author.ToString()} - {loan.Book.Title}] är utlånad till kund {loan.CustomerId} [{loan.Customer.FirstName} {loan.Customer.LastName}]");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}

	private void AddProductUi()
	{
		Console.Clear();
		Console.WriteLine("Lägg till produkt \n=================");

		Console.Write("Ange produktnamn: ");
		string title = Console.ReadLine()!;
		Console.Write("Ange beskrivning: ");
		string description = Console.ReadLine()!;
		Console.Write("Ange kategori: ");
		string category = Console.ReadLine()!; ;
		Console.Write("Ange tillverkare: ");
		string manufacturer = Console.ReadLine()!;
		Console.Write("Ange pris: ");
		decimal price = decimal.Parse(Console.ReadLine()!);

		try
		{
			var result = _productService.CreateProduct(title, description, price, category, manufacturer);
			Console.Clear();
			if (result != null)
				Console.WriteLine($"Produkten skapades. Produktens artikelnummer är {result.ArticleNumber}");
			else
				Console.WriteLine("Något gick fel.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void GetProductUi()
	{
		Console.Clear();
		Console.WriteLine("Visa produkt\n============");
		Console.Write("Ange artikelnummer: ");

		try
		{
			int articleNumber = int.Parse(Console.ReadLine()!);
			var product = _productService.GetProduct(articleNumber);
			Console.Clear();
			if(product== null)
			{
				Console.WriteLine("Produkten hittades inte.");
				GetKey();
				return;
			}
			Console.WriteLine("Produkt\n=======");
			Console.WriteLine($"Artikelnummer: {product.ArticleNumber}");
			Console.WriteLine($"Produktnamn: {product.Title}");
			Console.WriteLine($"Pris: {product.Price:0.00} kr");
			Console.WriteLine($"Tillverkare: {product.Manufacturer.Name}");
			Console.WriteLine($"Kategori: {product.Category.Name}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void GetAllProductsUi()
	{
		try
		{
			var productList = _productService.GetAllProducts();
			Console.Clear();
			Console.WriteLine("Produktlista\n============");
			foreach (var product in productList)
			{
				Console.WriteLine($"Artikelnummer {product.ArticleNumber} | {product.Title} ({product.Manufacturer.Name}) - {product.Price:0.00} kronor");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Något gick fel. Felkod {ex}: {ex.Message}");
		}
		GetKey();
	}
	private void UpdateProductUi()
	{
		throw new NotImplementedException();
	}
	private void DeleteProductUi()
	{
		throw new NotImplementedException();
	}
}
