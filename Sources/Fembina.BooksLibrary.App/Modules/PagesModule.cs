using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Pages;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class PagesModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BooksLibraryPage>().ToTransient();
        container.SetTarget<CreateBookPage>().ToTransient();
        container.SetTarget<EditBookPage>().ToTransient();
    }
}