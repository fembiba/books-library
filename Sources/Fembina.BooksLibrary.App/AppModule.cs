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
        container.AddModule<NavigationModule>().ToModule();
        container.AddModule<PagesModule>().ToModule();
        container.AddModule<LoggerModule>().ToModule();
        container.AddModule<SqlModule>().ToModule();
        container.AddModule<ValidatorsModule>().ToModule();
        container.AddModule<RepositoriesModule>().ToModule();
        container.AddModule<ServicesModule>().ToModule();
        container.AddModule<ControllersModule>().ToModule();
    }
}