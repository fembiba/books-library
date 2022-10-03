using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Modules;

using Repositories;
using Services;
using Utils;

public sealed class ControllersModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<ListedBooksController>()
            .AsSelf()
            .As<Controller>()
            .With(static c => new(c.FirstInstance<INavigator>(),
                c.FirstInstance<IBookService>()))
            .ToTransient();

        container.SetTarget<CreateBookFormController>()
            .AsSelf()
            .As<Controller>()
            .With(static c => new(c.FirstInstance<INavigator>(),
                c.FirstInstance<IBookService>()))
            .ToTransient();
    }
}