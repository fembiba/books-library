namespace Fembina.BooksLibrary.App.Entities;

public abstract record Entity : IEntity
{
    public const string IdSql = "id";

    public int Id { get; set; }
}