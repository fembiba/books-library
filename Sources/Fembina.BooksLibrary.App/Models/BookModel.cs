using System.Windows.Media.Imaging;

namespace Fembina.BooksLibrary.App.Models;

public sealed record BookModel
{
    public BookModel(int identifier = -1)
    {
        Identifier = identifier;
    }

    public int? Identifier { get; }
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int PublishYear { get; set; } = 2000;

    public string AuthorFirstName { get; set; } = string.Empty;

    public string AuthorLastName { get; set; } = string.Empty;

    public string IsbnCode { get; set; } = "00-00-000-000-000";
    
    public BitmapImage? Cover { get; set; }
}