using System.IO;
using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Injection.Providers;
using Fembina.BooksLibrary.App.Extensions;
using Fembina.BooksLibrary.App.Migrations;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace Fembina.BooksLibrary.App.Providers;

public sealed class SqlConnectionProvider : MethodProvider<SqliteConnection>
{
    private readonly string _databaseFile;

    public SqlConnectionProvider(string databaseFile)
    {
        _databaseFile = databaseFile;
    }

    protected override SqliteConnection CreateInstanceCore(IReadOnlyContainer container)
    {
        var logger = container.FirstInstance<ILogger>();

        try
        {
            TryCreateDatabaseFile(_databaseFile);

            var connection = CreateDatabaseConnection(_databaseFile);

            RunAllMigrations(container, connection);

            return connection;
        }
        catch (Exception)
        {
            logger.LogError("Can't connect to SQL database.");

            throw;
        }
    }

    private static void TryCreateDatabaseFile(string path)
    {
        // TODO: Do more fluent.
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.Open(path, FileMode.OpenOrCreate).Close();
    }

    private static SqliteConnection CreateDatabaseConnection(string path)
    {
        var connection = new SqliteConnection($"Data Source={path}");
        connection.Open();
        return connection;
    }

    private static void RunAllMigrations(IReadOnlyContainer container, SqliteConnection connection)
    {
        var migrations = container.AllInstance<IMigration>();
        migrations.RunAllMigrations(connection);
    }
}