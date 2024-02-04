using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthorEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string FirstName { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string LastName { get; set; } = null!;

	public virtual ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
}
