using System.Windows;
using System.Windows.Input;
using Fembina.BooksLibrary.App.Arguments;
using Fembina.BooksLibrary.App.Models;
using Fembina.BooksLibrary.App.Pages;
using Fembina.BooksLibrary.App.Services;
using Fembina.BooksLibrary.App.Utils;
using FluentValidation;
using Prism.Commands;

namespace Fembina.BooksLibrary.App.Controllers;

public abstract class BookFormController : SingleController<BookModel>
{
    private string? _path;

    public BookFormController(INavigator navigator) : base(navigator)
    {
        Cancel = new DelegateCommand(OnBack);
        Save = new DelegateCommand(async () => await OnSave().ConfigureAwait(false));
        Upload = new DelegateCommand<SourceUpdatedArgs>(OnUpload);
        Model = new BookModel();
    }
    
    protected string? Path => _path;

    public BookModel Book => Model!;

    public ICommand Upload { get; }

    public ICommand Save { get; }

    public ICommand Cancel { get; }

    private void OnUpload(SourceUpdatedArgs obj)
    {
        _path = obj.Path;
    }

    private void OnBack()
    {
        Navigator.NavigateBack();
    }

    protected abstract Task OnSave();
}