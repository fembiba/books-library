namespace Fembina.BooksLibrary.App.Controllers;

using System.Collections.ObjectModel;
using Utils;

public abstract class ManyController<T> : Controller
{
    protected ManyController(INavigator navigator) : base(navigator)
    {
        Models = new();
    }
    
    protected ObservableCollection<T> Models { get; set; }
}