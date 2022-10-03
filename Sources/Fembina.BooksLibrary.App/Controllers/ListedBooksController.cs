using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Fembina.BooksLibrary.App.Extensions;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using Prism.Commands;

namespace Fembina.BooksLibrary.App.Controllers;

public sealed class ListedBooksController : ManyController<BookOperationsController>
{
    private readonly IBookService _bookService;

    public ListedBooksController(INavigator navigator, IBookService bookService) : base(navigator)
    {
        _bookService = bookService;
        Pattern = string.Empty;
        Search = new DelegateCommand(async () => await OnSearching().ConfigureAwait(false));
        Create = new DelegateCommand(OnCreate);
        OnSearching().Wait();
    }

    public IReadOnlyCollection<BookOperationsController> Books => Models;

    public string Pattern { get; set; }

    public ICommand Search { get; }

    public ICommand Create { get; }

    // TODO: Add lazy loading.
    private async Task OnSearching()
    {
        Models.Clear();

        var books = _bookService.SearchBooks(Pattern);

        await foreach (var book in books)
        {
            var path = _bookService.GetBookCover(book);

            var image = new Uri(path).CreateImage();

            // TODO: Add automapper.
            var model = new BookModel(book.Id)
            {
                Title = book.Title,
                Description = book.Description,
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                IsbnCode = book.Isbn,
                PublishYear = book.PublishYear,
                Cover = image
            };

            var controller = new BookOperationsController(Navigator, _bookService, model);

            Models.Add(controller);
        }
    }

    private void OnCreate()
    {
        Navigator.NavigatePage<CreateBookPage>();
    }
}