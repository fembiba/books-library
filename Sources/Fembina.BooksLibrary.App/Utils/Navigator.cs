using System.Windows.Navigation;
using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;

namespace Fembina.BooksLibrary.App.Utils;

public sealed class Navigator : INavigator
{
    private readonly IReadOnlyContainer _container;

    private NavigationService? _service;

    public Navigator(IReadOnlyContainer container, NavigationService? service = null)
    {
        _service = service;
        _container = container;
    }

    public bool CanNavigateBack => _service!.CanGoBack;

    public bool CanNavigateForward => _service!.CanGoForward;

    public bool TryNavigateBack()
    {
        var can = CanNavigateBack;
        if (can) NavigateBack();
        return can;
    }

    public bool TryNavigateForward()
    {
        var can = CanNavigateForward;
        if (can) NavigateForward();
        return can;
    }

    public void NavigateBack()
    {
        _service!.GoBack();
    }

    public void NavigateForward()
    {
        _service!.GoForward();
    }

    public bool TryNavigatePage<T>() where T : class
    {
        var page = _container.TryFirstInstance<T>();
        return page is not null && _service!.Navigate(page);
    }

    public bool TryNavigatePage<T>(Action<T> factory) where T : class
    {
        var page = _container.TryFirstInstance<T>();
        if (page is null) return false;
        factory(page);
        return _service!.Navigate(page);
    }

    public void NavigatePage()
    {
        _service!.Refresh();
    }

    public void NavigatePage<T>() where T : class
    {
        var page = _container.FirstInstance<T>();
        var result = _service!.Navigate(page);
        if (!result) throw new InvalidOperationException();
    }

    public void NavigatePage<T>(Action<T> factory) where T : class
    {
        var page = _container.FirstInstance<T>();
        factory(page);
        var result = _service!.Navigate(page);
        if (!result) throw new InvalidOperationException();
    }

    public void ForceNavigatePage<T>() where T : class
    {
        _service!.RemoveBackEntry();
        NavigatePage<T>();
    }

    public void ForceNavigatePage<T>(Action<T> factory) where T : class
    {
        _service!.RemoveBackEntry();
        NavigatePage<T>(factory);
    }

    public void BindNavigationService(NavigationService service)
    {
        _service = service;
    }
}