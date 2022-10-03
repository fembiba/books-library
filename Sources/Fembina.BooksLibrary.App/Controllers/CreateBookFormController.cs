using System.Windows;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class CreateBookFormController : BookFormController
{
    private readonly IBookService _bookService;
    
    public CreateBookFormController(INavigator navigator, IBookService bookService) : base(navigator)
    {
        _bookService = bookService;
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
            await _bookService.CreateBook(Book.Title, Book.Description, Book.PublishYear, Book.AuthorFirstName,
                Book.AuthorLastName, Book.IsbnCode, Path!);

            Navigator.ForceNavigatePage<BooksLibraryPage>();
        }
        catch (ValidationException e)
        {
            MessageBox.Show(e.Message);
        }
    }
}