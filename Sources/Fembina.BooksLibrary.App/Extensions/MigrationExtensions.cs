using Fembina.BooksLibrary.App.Migrations;
using Microsoft.Data.Sqlite;

namespace Fembina.BooksLibrary.App.Extensions;

public static class MigrationExtensions
{
    public static void RunAllMigrations(this IEnumerable<IMigration> migrations, SqliteConnection connection)
    {
        foreach (var migration in migrations) migration.Migrate(connection);
    }
}