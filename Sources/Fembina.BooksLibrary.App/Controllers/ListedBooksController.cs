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

public sealed class ListedBooksController : Controller
{
    private readonly ObservableCollection<BookOperationsController> _books;

    private readonly IBookService _bookService;

    private readonly INavigator _navigator;

    public ListedBooksController(INavigator navigator, IBookService bookService)
    {
        _books = new ObservableCollection<BookOperationsController>();
        _navigator = navigator;
        _bookService = bookService;
        Books = _books;
        Pattern = string.Empty;
        Search = new DelegateCommand(async () => await OnSearching().ConfigureAwait(false));
        Create = new DelegateCommand(OnCreate);
        OnSearching().Wait();
    }

    public IReadOnlyCollection<BookOperationsController> Books { get; }

    public string Pattern { get; set; }

    public ICommand Search { get; }

    public ICommand Create { get; }

    // TODO: Add lazy loading.
    private async Task OnSearching()
    {
        _books.Clear();

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

            var controller = new BookOperationsController(model, _navigator, _bookService);

            _books.Add(controller);
        }
    }

    private void OnCreate()
    {
        _navigator.NavigatePage<CreateBookPage>();
    }
}