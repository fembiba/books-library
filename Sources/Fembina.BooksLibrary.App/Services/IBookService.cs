using Fembina.BooksLibrary.App.Entities;

namespace Fembina.BooksLibrary.App.Services;

public interface IBookService
{
    Task<BookEntity> CreateBook(string title, string description, int publishYear,
        string authorFirstName, string authorLastName, string isbn, string filePath);

    IAsyncEnumerable<BookEntity> GetAllBooks(int limit = 0, int offset = 0);

    IAsyncEnumerable<BookEntity> SearchBooks(string pattern, int limit = 0, int offset = 0);

    Task<BookEntity> ChangeBookInformation(int id, string title, string description, int publishYear,
        string authorFirstName, string authorLastName, string isbn, string? filePath = null);

    Task RemoveBook(int id);

    string GetBookCover(BookEntity book);

    Task<BookEntity> GetBookInformation(int id);
}