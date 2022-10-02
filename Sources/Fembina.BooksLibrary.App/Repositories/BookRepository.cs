using Fembina.BooksLibrary.App.Entities;
using Microsoft.Data.Sqlite;

namespace Fembina.BooksLibrary.App.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly SqliteConnection _connection;

    public BookRepository(SqliteConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _connection = connection;
    }

    public async Task Update(BookEntity book)
    {
        ArgumentNullException.ThrowIfNull(book);

        if (book.Id < 0) return;

        await using var updateCommand = new SqliteCommand(BuildSqlUpdateQuery(book), _connection);

        await updateCommand.ExecuteNonQueryAsync();
    }

    public async Task Remove(int id)
    {
        if (id < 0) return;

        await using var removeCommand = new SqliteCommand(BuildSqlDeleteQuery(id), _connection);

        await removeCommand.ExecuteNonQueryAsync();
    }

    public async Task<BookEntity?> Get(int id)
    {
        if (id < 0) return null;

        await using var readCommand = new SqliteCommand(BuildSqlReadQuery(id), _connection);

        await using var reader = await readCommand.ExecuteReaderAsync();

        if (!await reader.ReadAsync() || !reader.HasRows) return null;

        return ParseBookEntityFromSql(reader);
    }

    public async Task<BookEntity> Save(BookEntity book)
    {
        ArgumentNullException.ThrowIfNull(book);

        await using var saveCommand = new SqliteCommand(BuildSqlSaveQuery(book), _connection);

        var id = (int)(long)(await saveCommand.ExecuteScalarAsync())!;

        return CopyBookWithId(id, book);
    }

    public IAsyncEnumerable<BookEntity> All(int limit = 0, int offset = 0)
    {
        return Search(string.Empty, limit, offset);
    }

    public async IAsyncEnumerable<BookEntity> Search(string pattern, int limit = 0, int offset = 0)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        await using var searchCommand = new SqliteCommand(BuildSqlSearchQuery(pattern), _connection);

        await using var reader = await searchCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync()) yield return ParseBookEntityFromSql(reader);
    }

    private static BookEntity CopyBookWithId(int id, BookEntity book)
    {
        return book with { Id = id };
    }

    private static BookEntity ParseBookEntityFromSql(SqliteDataReader reader)
    {
        try
        {
            var id = (int)(long)reader[Entity.IdSql];
            var title = (string)reader[BookEntity.TitleSql];
            var description = (string)reader[BookEntity.DescriptionSql];
            var firstName = (string)reader[BookEntity.AuthorFirstNameSql];
            var lastName = (string)reader[BookEntity.AuthorLastNameSql];
            var isbn = (string)reader[BookEntity.IsbnSql];
            var fileId = Guid.Parse((string)reader[BookEntity.FileIdSql]);
            var fileName = (string)reader[BookEntity.FileNameSql];
            var year = (int)(long)reader[BookEntity.PublishYearSql];

            return new BookEntity(title, description, year,
                firstName, lastName, isbn, fileId, fileName, id);
        }
        catch (Exception ex)
        {
            throw new FormatException(null, ex);
        }
    }

    private static string BuildSqlSearchQuery(string pattern, int limit = 0, int offset = 0)
    {
        if (pattern == string.Empty || pattern.Replace(" ", "") == string.Empty)
            return $"SELECT * FROM {BookEntity.TableNameSql} " +
                   $"ORDER BY {BookEntity.TitleSql} " +
                   (limit > 0 ? $"LIMIT {limit} OFFSET {offset};" : ";");

        if (int.TryParse(pattern, out var yearOrId))
            return $"SELECT * FROM {BookEntity.TableNameSql} " +
                   $"WHERE {Entity.IdSql} = {yearOrId} " +
                   $"OR {BookEntity.PublishYearSql} = {yearOrId} " +
                   $"ORDER BY {BookEntity.TitleSql} " +
                   (limit > 0 ? $"LIMIT {limit} OFFSET {offset};" : ";");

        return $"SELECT * FROM {BookEntity.TableNameSql} " +
               $"WHERE instr(lower({BookEntity.TitleSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.DescriptionSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.AuthorFirstNameSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.AuthorLastNameSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.IsbnSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.FileIdSql}), lower('{pattern}')) > 0 " +
               $"OR instr(lower({BookEntity.FileNameSql}), lower('{pattern}')) > 0 " +
               $"ORDER BY {BookEntity.TitleSql} " +
               (limit > 0 ? $"LIMIT {limit} OFFSET {offset};" : ";");
    }

    private static string BuildSqlReadQuery(int id)
    {
        return $"SELECT * FROM {BookEntity.TableNameSql} " +
               $"WHERE {Entity.IdSql} = {id} " +
               "LIMIT 1;";
    }

    private static string BuildSqlSaveQuery(BookEntity book)
    {
        return $"INSERT INTO {BookEntity.TableNameSql} " +
               $"({BookEntity.TitleSql}, " +
               $"{BookEntity.DescriptionSql}, " +
               $"{BookEntity.AuthorFirstNameSql}, " +
               $"{BookEntity.AuthorLastNameSql}, " +
               $"{BookEntity.IsbnSql}, " +
               $"{BookEntity.FileIdSql}, " +
               $"{BookEntity.FileNameSql}, " +
               $"{BookEntity.PublishYearSql}) " +
               $"VALUES ('{book.Title}', " +
               $"'{book.Description}', " +
               $"'{book.AuthorFirstName}', " +
               $"'{book.AuthorLastName}', " +
               $"'{book.Isbn}', " +
               $"'{book.FileId}', " +
               $"'{book.FileName}', " +
               $"{book.PublishYear}); " +
               "SELECT last_insert_rowid();";
    }

    private static string BuildSqlUpdateQuery(BookEntity book)
    {
        // TODO: Optimize.
        return $"UPDATE {BookEntity.TableNameSql} " +
               $"SET {BookEntity.TitleSql} = '{book.Title}', " +
               $"{BookEntity.DescriptionSql} = '{book.Description}', " +
               $"{BookEntity.AuthorFirstNameSql} = '{book.AuthorFirstName}', " +
               $"{BookEntity.AuthorLastNameSql} = '{book.AuthorLastName}', " +
               $"{BookEntity.IsbnSql} = '{book.Isbn}', " +
               $"{BookEntity.FileIdSql} = '{book.FileId}', " +
               $"{BookEntity.FileNameSql} = '{book.FileName}', " +
               $"{BookEntity.PublishYearSql} = {book.PublishYear} " +
               $"WHERE {Entity.IdSql} = {book.Id};";
    }

    private static string BuildSqlDeleteQuery(int id)
    {
        return $"DELETE FROM {BookEntity.TableNameSql} WHERE id = {id};";
    }
}