using System.Windows;
using System.Windows.Controls;
using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using Fembina.BooksLibrary.App.Controllers;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;

namespace Fembina.BooksLibrary.App.Pages;

public partial class BooksLibraryPage : Page
{
    public BooksLibraryPage(ListedBooksController controller)
    {
        InitializeComponent();

        DataContext = controller;
    }
}