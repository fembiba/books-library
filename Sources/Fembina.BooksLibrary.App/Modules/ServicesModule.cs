using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Services;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class ServicesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookService>()
            .As<IBookService>()
            .ToSingleton();
    }
}