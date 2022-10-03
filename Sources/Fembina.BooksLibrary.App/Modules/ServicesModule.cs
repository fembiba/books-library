using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Services;

namespace Fembina.BooksLibrary.App.Modules;

using Repositories;
using Validators;

public sealed class ServicesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookService>()
            .As<IBookService>()
            .With(static c => new(c.FirstInstance<IBookRepository>(), 
                c.FirstInstance<IFileRepository>(), 
                c.FirstInstance<FileFormatValidator>(), 
                c.FirstInstance<BookEntityValidator>()))
            .ToSingleton();
    }
}