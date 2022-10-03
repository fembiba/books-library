using System.Windows.Controls;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Pages;

public partial class CreateBookPage : Page
{
    public CreateBookPage(CreateBookFormController controller)
    {
        InitializeComponent();

        DataContext = controller;
    }
}