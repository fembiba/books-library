using System.Windows.Input;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using Prism.Commands;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class BookOperationsController : SingleController<BookModel>
{
    private readonly IBookService _bookService;

    public BookOperationsController(INavigator navigator, IBookService bookService, BookModel model) : base(navigator)
    {
        Model = model;
        _bookService = bookService;
        Delete = new DelegateCommand(async () => await OnDelete().ConfigureAwait(false));
        Edit = new DelegateCommand(OnEdit);
    }

    public BookModel Book => Model!;

    public ICommand Edit { get; }

    public ICommand Delete { get; }

    private void OnEdit()
    {
        Navigator.NavigatePage<EditBookPage>(page =>
        {
            var controller = new EditBookFormController(Navigator, _bookService, Model!);
            
            page.SetController(controller);
        });
    }

    private async Task OnDelete()
    {
        await _bookService.RemoveBook((int)Model!.Identifier!);
        Navigator.ForceNavigatePage<BooksLibraryPage>();
    }
}