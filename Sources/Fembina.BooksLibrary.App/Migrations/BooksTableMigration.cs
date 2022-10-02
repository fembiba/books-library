using Fembina.BooksLibrary.App.Entities;
using Microsoft.Data.Sqlite;

namespace Fembina.BooksLibrary.App.Migrations;

public sealed class BooksTableMigration : IMigration
{
    public void Migrate(SqliteConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        using var createCommand = new SqliteCommand(BuildSqlCreateQuery(), connection);

        createCommand.ExecuteNonQuery();
    }

    private static string BuildSqlCreateQuery()
    {
        return $"CREATE TABLE IF NOT EXISTS {BookEntity.TableNameSql} (" +
               $"{Entity.IdSql} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
               $"{BookEntity.TitleSql} VARCHAR(32) NOT NULL, " +
               $"{BookEntity.DescriptionSql} VARCHAR(64) NOT NULL, " +
               $"{BookEntity.PublishYearSql} INTEGER NOT NULL, " +
               $"{BookEntity.AuthorFirstNameSql} VARCHAR(16) NOT NULL, " +
               $"{BookEntity.AuthorLastNameSql} VARCHAR(16) NOT NULL, " +
               $"{BookEntity.IsbnSql} VARCHAR(25) NOT NULL, " +
               $"{BookEntity.FileIdSql} TEXT NOT NULL, " +
               $"{BookEntity.FileNameSql} TEXT NOT NULL" +
               ");";
    }
}