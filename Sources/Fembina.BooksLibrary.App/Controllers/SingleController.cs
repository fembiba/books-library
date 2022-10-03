namespace Fembina.BooksLibrary.App.Controllers;

using Utils;

public abstract class SingleController<T> : Controller
{
    protected SingleController(INavigator navigator) : base(navigator)
    {
    }
    
    protected T? Model { get; set; }
}