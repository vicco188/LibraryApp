using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;
[Index(nameof(Name), IsUnique = true)]
public class GenreEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string Name { get; set; } = null!;

	public virtual ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
}