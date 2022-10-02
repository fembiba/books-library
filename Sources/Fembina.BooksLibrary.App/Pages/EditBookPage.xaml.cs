using System.Windows.Controls;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Pages;

public partial class EditBookPage : Page
{
    public EditBookPage()
    {
        InitializeComponent();
    }

    public void SetController(BookEditFormController controller)
    {
        DataContext = controller;
    }
}