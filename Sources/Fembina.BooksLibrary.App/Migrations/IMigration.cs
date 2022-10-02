using Microsoft.Data.Sqlite;

namespace Fembina.BooksLibrary.App.Migrations;

public interface IMigration
{
    void Migrate(SqliteConnection connection);
}