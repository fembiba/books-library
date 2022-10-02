using System.IO;
using Fembina.BooksLibrary.App.Entities;
using Fembina.BooksLibrary.App.Repositories;
using Fembina.BooksLibrary.App.Validators;
using FluentValidation;
using Microsoft.Data.Sqlite;

namespace Fembina.BooksLibrary.App.Services;

public sealed class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    private readonly IValidator<BookEntity> _bookValidator;

    private readonly IFileRepository _fileRepository;

    private readonly IValidator<string> _fileValidator;

    public BookService(IBookRepository bookRepository, IFileRepository fileRepository,
        FileFormatValidator fileValidator, BookEntityValidator bookValidator)
    {
        _bookRepository = bookRepository;
        _fileRepository = fileRepository;
        _fileValidator = fileValidator;
        _bookValidator = bookValidator;
    }

    // TODO: Add form model.
    public async Task<BookEntity> CreateBook(string title, string description, int publishYear, string authorFirstName,
        string authorLastName, string isbn, string filePath)
    {
        var fileName = Path.GetFileName(filePath);

        await _fileValidator.ValidateAndThrowAsync(fileName);

        var fileId = _fileRepository.Save(filePath);

        var book = new BookEntity(title, description, publishYear, authorFirstName,
            authorLastName, isbn, fileId, fileName);

        try
        {
            await _bookValidator.ValidateAndThrowAsync(book);

            return await _bookRepository.Save(book);
        }
        catch (SqliteException)
        {
            _fileRepository.Remove(fileId);
            throw;
        }
    }

    public IAsyncEnumerable<BookEntity> GetAllBooks(int limit = 0, int offset = 0)
    {
        return _bookRepository.All(limit, offset);
    }

    public IAsyncEnumerable<BookEntity> SearchBooks(string pattern, int limit = 0, int offset = 0)
    {
        return _bookRepository.Search(pattern, limit, offset);
    }

    // TODO: Add form model.
    public async Task<BookEntity> ChangeBookInformation(int id, string title, string description, int publishYear,
        string authorFirstName,
        string authorLastName, string isbn, string? filePath = null)
    {
        // TODO: Optimize.
        var book = await _bookRepository.Get(id) ?? throw new ArgumentException(nameof(id));

        book.Title = title;
        book.Description = description;
        book.PublishYear = publishYear;
        book.AuthorFirstName = authorFirstName;
        book.AuthorLastName = authorLastName;
        book.Isbn = isbn;

        if (filePath is null)
        {
            await _bookRepository.Update(book);
            return book;
        }

        var fileName = Path.GetFileName(filePath);

        await _fileValidator.ValidateAndThrowAsync(fileName);

        var fileId = _fileRepository.Save(filePath);

        var oldFileId = book.FileId;

        book.FileId = fileId;
        book.FileName = fileName;

        try
        {
            await _bookRepository.Update(book);
            _fileRepository.Remove(oldFileId);
        }
        catch (SqliteException)
        {
            _fileRepository.Remove(fileId);
            throw;
        }

        return book;
    }

    public async Task RemoveBook(int id)
    {
        var book = await _bookRepository.Get(id) ?? throw new ArgumentException(nameof(id));

        await _bookRepository.Remove(id);

        _fileRepository.Remove(book.FileId);
    }

    public string GetBookCover(BookEntity book)
    {
        return _fileRepository.Get(book.FileId);
    }

    public async Task<BookEntity> GetBookInformation(int id)
    {
        return await _bookRepository.Get(id) ?? throw new ArgumentException(nameof(id));
    }
}