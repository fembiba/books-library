using System.Windows;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class BookEditFormController : BookFormController
{
    public BookEditFormController(BookModel form, INavigator navigator, IBookService bookService) : base(navigator, bookService)
    {
        Form = form;
    }
    
    protected override async Task OnSave()
    {
        try
        {
            await BookService.ChangeBookInformation((int)Form.Identifier!, Form.Title, Form.Description, 
                Form.PublishYear, Form.AuthorFirstName, Form.AuthorLastName, Form.IsbnCode, Path);

            Navigator.ForceNavigatePage<BooksLibraryPage>();
        }
        catch (ValidationException e)
        {
            MessageBox.Show(e.Message);
        }
    }
}