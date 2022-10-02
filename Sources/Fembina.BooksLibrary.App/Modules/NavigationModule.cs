using System.Windows.Navigation;
using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Utils;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class NavigationModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<Navigator>()
            .As<INavigator>()
            .With(_ => new Navigator(container))
            .ToSingleton();
    }
}