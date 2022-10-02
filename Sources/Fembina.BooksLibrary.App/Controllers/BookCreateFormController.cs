using System.Windows;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class BookCreateFormController : BookFormController
{
    public BookCreateFormController(INavigator navigator, IBookService bookService) : base(navigator, bookService)
    {
    }
    
    protected override async Task OnSave()
    {
        if (Path is null)
        {
            MessageBox.Show("Upload book cover image before save.");
            return;
        }

        try
        {
            await BookService.CreateBook(Form.Title, Form.Description, Form.PublishYear, Form.AuthorFirstName,
                Form.AuthorLastName, Form.IsbnCode, Path!);

            Navigator.ForceNavigatePage<BooksLibraryPage>();
        }
        catch (ValidationException e)
        {
            MessageBox.Show(e.Message);
        }
    }
}