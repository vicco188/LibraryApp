﻿
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
}
