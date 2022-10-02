using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Microsoft.Extensions.Logging;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class LoggerModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<ILoggerFactory>()
            .With(static _ => LoggerFactory.Create(config => config.AddDebug()))
            .ToSingleton();

        container.SetTarget<ILogger>()
            .With(static container =>
                container.FirstInstance<ILoggerFactory>().CreateLogger<App>())
            .ToSingleton();
    }
}