using Fembina.BooksLibrary.App.Entities;

namespace Fembina.BooksLibrary.App.Repositories;

public interface IBookRepository
{
    IAsyncEnumerable<BookEntity> All(int limit = 0, int offset = 0);

    IAsyncEnumerable<BookEntity> Search(string pattern, int limit = 0, int offset = 0);

    Task<BookEntity?> Get(int id);

    Task<BookEntity> Save(BookEntity book);

    Task Update(BookEntity book);

    Task Remove(int id);
}