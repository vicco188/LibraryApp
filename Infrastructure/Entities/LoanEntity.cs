using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;
[Index(nameof(BookId), IsUnique = true)]
public class LoanEntity
{
	[Key]
	public int LoanNumber { get; set; }


	[Required]
	[ForeignKey(nameof(BookEntity))]
	public int BookId { get; set; }


	[Required]
	[ForeignKey(nameof(CustomerEntity))]
	public int CustomerId { get; set; }


	public virtual BookEntity Book { get; set; } = null!;
	public virtual CustomerEntity Customer { get; set;} = null!;
}