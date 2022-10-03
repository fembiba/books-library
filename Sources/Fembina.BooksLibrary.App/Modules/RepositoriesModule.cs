using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Repositories;

namespace Fembina.BooksLibrary.App.Modules;

using Microsoft.Data.Sqlite;

public sealed class RepositoriesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookRepository>()
            .As<IBookRepository>()
            .With(static c => new(c.FirstInstance<SqliteConnection>()))
            .ToSingleton();

        container.SetTarget<FileRepository>()
            .As<IFileRepository>()
            .With(static _ => new())
            .ToSingleton();
    }
}