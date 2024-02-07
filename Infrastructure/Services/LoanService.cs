using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class LoanService(LoanRepository loanRepository)
{
	public readonly LoanRepository _loanRepository = loanRepository;

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
	public LoanEntity DeleteLoan(int bookId)
	{
		try
		{
			return _loanRepository.Delete(l=>l.Book.Id == bookId);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteLoan : " + ex.Message); }
		return null!;


	}
}
