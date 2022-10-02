namespace Fembina.BooksLibrary.App.Entities;

public sealed record BookEntity : Entity
{
    public const string TableNameSql = "books";

    public const string TitleSql = "title";

    public const string DescriptionSql = "description";

    public const string PublishYearSql = "publish_year";

    public const string AuthorFirstNameSql = "author_first_name";

    public const string AuthorLastNameSql = "author_last_name";

    public const string IsbnSql = "isbn";

    public const string FileIdSql = "file_id";

    public const string FileNameSql = "file_name";

    public BookEntity(string title, string description, int publishYear,
        string authorFirstName, string authorLastName, string isbn,
        Guid fileId, string fileName, int id = -1)
    {
        Title = title;
        Description = description;
        PublishYear = publishYear;
        AuthorLastName = authorLastName;
        AuthorFirstName = authorFirstName;
        Isbn = isbn;
        FileId = fileId;
        FileName = fileName;
        Id = id;
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public int PublishYear { get; set; }

    public string AuthorFirstName { get; set; }

    public string AuthorLastName { get; set; }

    public string Isbn { get; set; }

    public Guid FileId { get; set; }

    public string FileName { get; set; }
}