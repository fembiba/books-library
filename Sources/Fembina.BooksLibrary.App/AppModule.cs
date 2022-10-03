using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Modules;

namespace Fembina.BooksLibrary.App;

public sealed class AppModule : Module
{
    public override void Load(IContainer container)
    {
        container.AddModule<NavigationModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<PagesModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<LoggerModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<SqlModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<ValidatorsModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<RepositoriesModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<ServicesModule>()
            .With(() => new())
            .ToModule();
        container.AddModule<ControllersModule>()
            .With(() => new())
            .ToModule();
    }
}