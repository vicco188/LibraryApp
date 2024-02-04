using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string FirstName { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string LastName { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string Email { get; set; } = null!;

	public virtual ICollection<LoanEntity> Loans { get; set; } = new List<LoanEntity>();
}
