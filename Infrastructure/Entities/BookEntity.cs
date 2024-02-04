using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class BookEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(200)")]
	public string Title { get; set; } = null!;

	[Required]
	[ForeignKey(nameof(AuthorEntity))]
	public int AuthorId { get; set; }

	[Required]
	[ForeignKey(nameof(PublisherEntity))]
	public int PublisherId { get; set; }

	[Required]
	[ForeignKey(nameof(GenreEntity))]
	public int GenreId { get; set; }

	[Required]
	[ForeignKey(nameof(LanguageEntity))]
	public int LanguageId { get; set; }


	public virtual AuthorEntity Author { get; set; } = null!;
	public virtual PublisherEntity Publisher { get; set; } = null!;
	public virtual LanguageEntity Language { get; set; } = null!;
	public virtual GenreEntity Genre { get; set; } = null!;	
	public virtual LoanEntity? Loan { get; set; }
}
