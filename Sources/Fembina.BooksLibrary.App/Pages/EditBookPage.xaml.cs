using System.Windows.Controls;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Pages;

public partial class EditBookPage : Page
{
    public EditBookPage()
    {
        InitializeComponent();
    }

    public void SetController(EditBookFormController controller)
    {
        DataContext = controller;
    }
}