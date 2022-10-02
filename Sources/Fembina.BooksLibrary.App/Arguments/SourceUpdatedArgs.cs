using System.Windows;

namespace Fembina.BooksLibrary.App.Arguments;

public class SourceUpdatedArgs : RoutedEventArgs
{
    public SourceUpdatedArgs(string path)
    {
        Path = path;
    }

    public SourceUpdatedArgs(RoutedEvent routedEvent, string path) : base(routedEvent)
    {
        Path = path;
    }

    public SourceUpdatedArgs(RoutedEvent routedEvent, object source, string path) : base(routedEvent, source)
    {
        Path = path;
    }

    public string Path { get; }
}