using System.Windows.Navigation;

namespace Fembina.BooksLibrary.App.Utils;

public interface INavigator
{
    bool CanNavigateBack { get; }

    bool CanNavigateForward { get; }

    bool TryNavigateBack();

    bool TryNavigateForward();

    void NavigateBack();

    void NavigateForward();

    bool TryNavigatePage<T>() where T : class;

    bool TryNavigatePage<T>(Action<T> factory) where T : class;

    void NavigatePage();

    void NavigatePage<T>() where T : class;

    void NavigatePage<T>(Action<T> factory) where T : class;

    void ForceNavigatePage<T>() where T : class;

    void ForceNavigatePage<T>(Action<T> factory) where T : class;

    void BindNavigationService(NavigationService service);
}