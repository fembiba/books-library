using System.Windows;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class EditBookFormController : BookFormController
{
    private readonly IBookService _bookService;
    
    public EditBookFormController(INavigator navigator, IBookService bookService,
        BookModel book) : base(navigator)
    {
        _bookService = bookService;
        Model = book;
    }
    
    protected override async Task OnSave()
    {
        try
        {
            await _bookService.ChangeBookInformation((int)Book.Identifier!, Book.Title, Book.Description, 
                Book.PublishYear, Book.AuthorFirstName, Book.AuthorLastName, Book.IsbnCode, Path);

            Navigator.ForceNavigatePage<BooksLibraryPage>();
        }
        catch (ValidationException e)
        {
            MessageBox.Show(e.Message);
        }
    }
}