using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Pages;

namespace Fembina.BooksLibrary.App.Modules;

using Controllers;

public sealed class PagesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BooksLibraryPage>()
            .With(static c => new(c.FirstInstance<ListedBooksController>()))
            .ToTransient();
        container.SetTarget<CreateBookPage>()
            .With(static c => new(c.FirstInstance<CreateBookFormController>()))
            .ToTransient();
        container.SetTarget<EditBookPage>()
            .With(static c => new())
            .ToTransient();
    }
}