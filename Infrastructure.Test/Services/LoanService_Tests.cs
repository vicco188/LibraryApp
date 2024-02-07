using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Services;

public class LoanService_Tests
{
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
	.UseInMemoryDatabase($"{Guid.NewGuid()}")
	.Options);

	[Fact]
	public void CreateLoan_ShouldAddLoan_AndReturnLoanEntity()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var customerRepository = new CustomerRepository(context);
		var loanRepository = new LoanRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		var customerService = new CustomerService(customerRepository);
		var loanService = new LoanService(loanRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		// Act
		var result = loanService.CreateLoan(1, 1);
		// Assert
		Assert.NotNull(result);
		Assert.Equal("King", result.Book.Author.LastName);
	}


	[Fact]
	public void GetLoan_ShouldReturnLoanEntityIfLoanExists_OtherwiseReturnNull()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var customerRepository = new CustomerRepository(context);
		var loanRepository = new LoanRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		var customerService = new CustomerService(customerRepository);
		var loanService = new LoanService(loanRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		bookService.CreateBook("Det", "Stephen", "King", "Månpocket", "Horror", "Svenska");
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		loanService.CreateLoan(1, 1);
		// Act
		var result1 = loanService.GetLoan(1);
		var result2 = loanService.GetLoan(2);
		// Assert
		Assert.Null(result2 );
		Assert.NotNull(result1);
	}

	[Fact]
	public void GetAllLoans_ShouldReturnListOfLoanEntity()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var customerRepository = new CustomerRepository(context);
		var loanRepository = new LoanRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		var customerService = new CustomerService(customerRepository);
		var loanService = new LoanService(loanRepository);
		bookService.CreateBook("Pet Sematary", "Stephen", "King", "Simon and Schuster", "Horror", "English");
		bookService.CreateBook("Det", "Stephen", "King", "Månpocket", "Horror", "Svenska");
		customerService.CreateCustomer(new CustomerEntity { FirstName = "Test", LastName = "Test", Email = "test@test.com" });
		loanService.CreateLoan(1, 1);
		// Act
		var result = loanService.GetAllLoans();
		// Assert
		Assert.NotNull(result);
		Assert.Single(result);
		Assert.IsAssignableFrom<IEnumerable<LoanEntity>>(result);
	}

	[Fact]
	public void GetAllLoans_ShouldReturnEmptyListOfLoanEntity_IfNoLoansInDb()
	{
		// Arrange
		var authorRepository = new AuthorRepository(context);
		var bookRepository = new BookRepository(context);
		var genreRepository = new GenreRepository(context);
		var languageRepository = new LanguageRepository(context);
		var publisherRepository = new PublisherRepository(context);
		var customerRepository = new CustomerRepository(context);
		var loanRepository = new LoanRepository(context);
		var bookService = new BookService(authorRepository, bookRepository, genreRepository, languageRepository, publisherRepository);
		var customerService = new CustomerService(customerRepository);
		var loanService = new LoanService(loanRepository);
		
		// Act
		var result = loanService.GetAllLoans();
		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
		Assert.IsAssignableFrom<IEnumerable<LoanEntity>>(result);
	}
}
