using Prism.Mvvm;

namespace Fembina.BooksLibrary.App.Controllers;

using Utils;

public abstract class Controller : BindableBase
{
    protected Controller(INavigator navigator)
    {
        Navigator = navigator;
    }
    
    protected INavigator Navigator { get; }
}