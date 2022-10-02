using System.Windows;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Utils;

namespace Fembina.BooksLibrary.App;

public partial class AppWindow : Window
{
    public AppWindow(INavigator navigator)
    {
        InitializeComponent();

        navigator.BindNavigationService(XFrame.NavigationService);

        navigator.ForceNavigatePage<BooksLibraryPage>();
    }
}