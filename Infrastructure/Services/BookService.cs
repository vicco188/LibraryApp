
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class BookService(AuthorRepository authorRepository, BookRepository bookRepository, GenreRepository genreRepository, LanguageRepository languageRepository, PublisherRepository publisherRepository)
{
	private readonly AuthorRepository _authorRepository = authorRepository;
	private readonly BookRepository _bookRepository = bookRepository;
	private readonly GenreRepository _genreRepository = genreRepository;
	private readonly LanguageRepository _languageRepository = languageRepository;
	private readonly PublisherRepository _publisherRepository = publisherRepository;

	/// <summary>
	/// Creates a new Book entry into the database
	/// </summary>
	/// <param name="title">The book's title</param>
	/// <param name="authorFirstName">The authors first name</param>
	/// <param name="authorLastName">The authors last name</param>
	/// <param name="publisher">The name of the book's publisher</param>
	/// <param name="genre">The genre of the book</param>
	/// <param name="language">The language of the book</param>
	/// <returns>A BookEntity of the new entry</returns>
	public BookEntity CreateBook(string title, string authorFirstName, string authorLastName, string publisher, string genre, string language)
	{
		try
		{
			AuthorEntity authorEntity;
			if (_authorRepository.Exists(a => a.FirstName == authorFirstName && a.LastName == authorLastName))
				authorEntity = _authorRepository.Read(a => a.FirstName == authorFirstName && a.LastName == authorLastName);
			else
				authorEntity = _authorRepository.Create(new AuthorEntity { FirstName = authorFirstName, LastName = authorLastName });

			PublisherEntity publisherEntity;
			if (_publisherRepository.Exists(p => p.Name == publisher))
				publisherEntity = _publisherRepository.Read(p => p.Name == publisher);
			else
				publisherEntity = _publisherRepository.Create(new PublisherEntity { Name = publisher });

			GenreEntity genreEntity;
			if (_genreRepository.Exists(p => p.Name == genre))
				genreEntity = _genreRepository.Read(p => p.Name == genre);
			else
				genreEntity = _genreRepository.Create(new GenreEntity { Name = genre });

			LanguageEntity languageEntity;
			if (_languageRepository.Exists(p => p.Name == language))
				languageEntity = _languageRepository.Read(p => p.Name == language);
			else
				languageEntity = _languageRepository.Create(new LanguageEntity { Name = language });

			var result = _bookRepository.Create(new BookEntity
			{
				Title = title,
				AuthorId = authorEntity.Id,
				PublisherId = publisherEntity.Id,
				GenreId = genreEntity.Id,
				LanguageId = languageEntity.Id
			});

			return result;
		}
		catch (Exception ex) { Debug.Write("Error in method CreateCustomer : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets a book by Id
	/// </summary>
	/// <param name="bookId">The Id of the book to get</param>
	/// <returns>The BookEntity requested if found, otherwise null</returns>
	public BookEntity GetBook(int bookId)
	{
		try
		{
			return _bookRepository.Read(b => b.Id == bookId);
		}
		catch (Exception ex) { Debug.Write("Error in method GetBook : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Gets all book entries from database
	/// </summary>
	/// <returns>An IEnumerable of BookEntities of all entries in database</returns>
	public IEnumerable<BookEntity> GetAllBooks()
	{
		try
		{
			return _bookRepository.ReadAll();
		}
		catch (Exception ex) { Debug.Write("Error in method GetAllBooks : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Updates a book entry's information in database
	/// </summary>
	/// <param name="book">BookEntity corresponding to the entry to update</param>
	/// <param name="title">New title</param>
	/// <param name="authorFirstName">New author first name</param>
	/// <param name="authorLastName">New author last name</param>
	/// <param name="publisher">New publisher name</param>
	/// <param name="genre">New genre</param>
	/// <param name="language">New language</param>
	/// <returns>A BookEntity correspoding to the new db entry if successful, otherwise null</returns>
	public BookEntity UpdateBook(BookEntity book, string title, string authorFirstName, string authorLastName, string publisher, string genre, string language)
	{
		try
		{
			AuthorEntity authorEntity;
			if (_authorRepository.Exists(a => a.FirstName == authorFirstName && a.LastName == authorLastName))
				authorEntity = _authorRepository.Read(a => a.FirstName == authorFirstName && a.LastName == authorLastName);
			else
				authorEntity = _authorRepository.Create(new AuthorEntity { FirstName = authorFirstName, LastName = authorLastName });
			PublisherEntity publisherEntity;
			if (_publisherRepository.Exists(p => p.Name == publisher))
				publisherEntity = _publisherRepository.Read(p => p.Name == publisher);
			else
				publisherEntity = _publisherRepository.Create(new PublisherEntity { Name = publisher });
			GenreEntity genreEntity;
			if (_genreRepository.Exists(p => p.Name == genre))
				genreEntity = _genreRepository.Read(p => p.Name == genre);
			else
				genreEntity = _genreRepository.Create(new GenreEntity { Name = genre });
			LanguageEntity languageEntity;
			if (_languageRepository.Exists(p => p.Name == language))
				languageEntity = _languageRepository.Read(p => p.Name == language);
			else
				languageEntity = _languageRepository.Create(new LanguageEntity { Name = language });

			book.Title = title;
			book.AuthorId = authorEntity.Id;
			book.PublisherId = publisherEntity.Id;
			book.GenreId = genreEntity.Id;
			book.LanguageId = languageEntity.Id;
			return _bookRepository.Update(b => b.Id == book.Id, book);

		}
		catch (Exception ex) { Debug.Write("Error in method UpdateBook : " + ex.Message); }
		return null!;
	}

	/// <summary>
	/// Deletes a book entry from database
	/// </summary>
	/// <param name="bookId">The Id of the book to delete</param>
	/// <returns>A BookEntity from the deleted database entry if successful, otherwise null</returns>
	public BookEntity DeleteBook(int bookId)
	{
		try
		{
			return _bookRepository.Delete(b => b.Id == bookId);
		}
		catch (Exception ex) { Debug.Write("Error in method DeleteBook : " + ex.Message); }
		return null!;
	}

}


