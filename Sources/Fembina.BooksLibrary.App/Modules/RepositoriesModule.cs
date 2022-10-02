using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Repositories;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class RepositoriesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookRepository>()
            .As<IBookRepository>()
            .ToSingleton();

        container.SetTarget<FileRepository>()
            .As<IFileRepository>()
            .ToSingleton();
    }
}