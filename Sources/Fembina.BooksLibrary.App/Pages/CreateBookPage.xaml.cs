using System.Windows.Controls;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Pages;

public partial class CreateBookPage : Page
{
    public CreateBookPage(BookCreateFormController controller)
    {
        InitializeComponent();

        DataContext = controller;
    }
}