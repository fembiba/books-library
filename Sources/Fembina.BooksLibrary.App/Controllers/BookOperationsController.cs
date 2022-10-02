using System.Windows.Input;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using Prism.Commands;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class BookOperationsController : Controller
{
    private readonly IBookService _bookService;

    private readonly INavigator _navigator;

    public BookOperationsController(BookModel model, INavigator navigator, IBookService bookService)
    {
        Model = model;
        _navigator = navigator;
        _bookService = bookService;
        Delete = new DelegateCommand(async () => await OnDelete().ConfigureAwait(false));
        Edit = new DelegateCommand(OnEdit);
    }

    public BookModel Model { get; }

    public ICommand Edit { get; }

    public ICommand Delete { get; }

    private void OnEdit()
    {
        _navigator.NavigatePage<EditBookPage>(page =>
        {
            var controller = new BookEditFormController(Model, _navigator, _bookService);
            
            page.SetController(controller);
        });
    }

    private async Task OnDelete()
    {
        await _bookService.RemoveBook((int)Model.Identifier!);
        _navigator.ForceNavigatePage<BooksLibraryPage>();
    }
}