using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Repositories;

public class LoanRepository_Tests
{
	
	private readonly LibDbContext context = new LibDbContext(new DbContextOptionsBuilder<LibDbContext>()
		.UseInMemoryDatabase($"{Guid.NewGuid()}")
		.Options);

	[Fact]
	public void Create_ShouldCreateLoan_AndReturnLoanEntity()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Cust First Test",
			LastName = "Cust First Test",
			Email = "test@test.com"
		});
		new BookRepository(context).Create(new BookEntity
		{
			Title = "Test Title 1",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		var repo = new LoanRepository(context);

		// Act
		var result = repo.Create(new LoanEntity { BookId = 1, CustomerId = 1 });

		//
		Assert.Equal(1, result.BookId);
	}

	[Fact]
	public void Create_ShouldCreateLoanEntity_AndBookShouldHaveLoan()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Cust First Test",
			LastName = "Cust First Test",
			Email = "test@test.com"
		});
		var bookRepository = new BookRepository(context);
		
		bookRepository.Create(new BookEntity
		{
			Title = "Test Title 1",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		var loanRepository = new LoanRepository(context);
		loanRepository.Create(new LoanEntity { BookId = 1, CustomerId = 1 });
		// Act
		var result = bookRepository.Read(x => x.Id == 1);

		//
		Assert.NotNull(result.Loan);
	}

	[Fact]
	public void Delete_ShouldReturnLoanEntity_AndBookShouldHaveNoLoans()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Author First Test", LastName = "Author Last Test" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Publisher Test" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Genre Test" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		var bookRepository = new BookRepository(context);
		bookRepository.Create(new BookEntity
		{
			Title = "Test Title 1",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Cust First Test",
			LastName = "Cust First Test",
			Email = "test@test.com"
		});
		var loanRepository = new LoanRepository(context);
		loanRepository.Create(new LoanEntity { BookId = 1, CustomerId = 1 });
		var loanResult = loanRepository.Delete(e => e.BookId == 1);
		// Act
		var bookResult = bookRepository.Read(x => x.Id == 1);
		// Assert
		Assert.Null(bookResult.Loan);
		Assert.Equal(1, loanResult.BookId);
	}

	[Fact]
	public void Read_ShouldReturnBookAndCustomer_IfLoanEntryExists()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Authorfirstname", LastName = "Authorlastname" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Testpublisher" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Testgenre" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		var bookRepository = new BookRepository(context);
		bookRepository.Create(new BookEntity
		{
			Title = "Booktitle",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Customerfirstname",
			LastName = "Customerlastname",
			Email = "test@test.com"
		});
		var loanRepository = new LoanRepository(context);
		loanRepository.Create(new LoanEntity { BookId = 1, CustomerId = 1 });
		// Act
		var result1 = loanRepository.Read(l => l.LoanNumber == 1);
		var result2 = loanRepository.Read(l => l.Book.Title == "Booktitle" && l.Customer.FirstName == "Customerfirstname" && l.Customer.LastName == "Customerlastname");
		// Assert
		Assert.Equal("Booktitle", result1.Book.Title);
		Assert.Equal(1, result2.LoanNumber);
	}

	[Fact]
	public void ReadAll_ShouldReturnListOfLoanEntities_IfNotEmptyDb()
	{
		// Arrange
		new AuthorRepository(context).Create(new AuthorEntity { FirstName = "Authorfirstname", LastName = "Authorlastname" });
		new PublisherRepository(context).Create(new PublisherEntity { Name = "Testpublisher" });
		new GenreRepository(context).Create(new GenreEntity { Name = "Testgenre" });
		new LanguageRepository(context).Create(new LanguageEntity { Name = "Swedish" });
		var bookRepository = new BookRepository(context);
		bookRepository.Create(new BookEntity
		{
			Title = "Booktitle",
			AuthorId = 1,
			PublisherId = 1,
			GenreId = 1,
			LanguageId = 1
		});
		new CustomerRepository(context).Create(new CustomerEntity
		{
			FirstName = "Customerfirstname",
			LastName = "Customerlastname",
			Email = "test@test.com"
		});
		var loanRepository = new LoanRepository(context);
		loanRepository.Create(new LoanEntity { BookId = 1, CustomerId = 1 });
		// Act
		var result = loanRepository.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<LoanEntity>>(result);
		Assert.Single(result);
	}
	[Fact]
	public void ReadAll_ShouldReturnListOfLoanEntities_IfNEmptyDb()
	{
		// Arrange
		var loanRepository = new LoanRepository(context);
		// Act
		var result = loanRepository.ReadAll();
		// Assert
		Assert.IsAssignableFrom<IEnumerable<LoanEntity>>(result);
		Assert.Empty(result);
	}
}
