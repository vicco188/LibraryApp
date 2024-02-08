using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class LoanService(LoanRepository loanRepository)
{
	public readonly LoanRepository _loanRepository = loanRepository;

	/// <summary>
	/// Registers a LoanEntity (pair of customer and book)
	/// </summary>
	/// <param name="bookId">Id of book to loan</param>
	/// <param name="customerId">Id of customer</param>
	/// <returns>LoanEntity if successful, otherwise null</returns>
	public LoanEntity CreateLoan(int bookId, int customerId)
	{
		try
		{
			if (_loanRepository.Exists(l => l.Book.Id == bookId)) return null!;
			return _loanRepository.Create(new LoanEntity { CustomerId = customerId, BookId = bookId });
		}
		catch (Exception ex) { Debug.Write("Error in method CreateLoan : " + ex.Message); }
		return null!;


	}

	/// <summary>
	/// Deletes loan (returns book)
	/// </summary>
	/// <param name="bookId">Id of book to return</param>
	/// <returns>Deleted loan entity if successful, otherwise null</returns>
	public LoanEntity DeleteLoan(int bookId)
	{
		try
		{
			return _loanRepository.Delete(l=>l.Book.Id == bookId);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteLoan : " + ex.Message); }
		return null!;


	}

	/// <summary>
	/// Gets loan entity
	/// </summary>
	/// <param name="bookId">Id of book in loan</param>
	/// <returns>LoanEntity if found, otherwise null</returns>
	public LoanEntity GetLoan(int bookId)
	{
		try
		{
			return _loanRepository.Read(l => l.BookId == bookId);
		}
		catch (Exception ex) { Debug.Write("Error in method GetLoan : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets a list of loans
	/// </summary>
	/// <returns>An IEnumerable of all LoanEntity entries in database</returns>
	public IEnumerable<LoanEntity> GetAllLoans()
	{
		try
		{
			return _loanRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllLoans : " + ex.Message); }
		return null!;
	}
}
