using System.IO;
using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Injection.Providers;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Extensions;
using Fembina.BooksLibrary.App.Migrations;
using Fembina.BooksLibrary.App.Providers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class SqlModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BooksTableMigration>()
            .AsSelf()
            .As<IMigration>()
            .ToSingleton();

        container.SetTarget<SqliteConnection>()
            .With(static () => new SqlConnectionProvider(AppConfiguration.DatabaseFile))
            .ToSingleton();
    }
}