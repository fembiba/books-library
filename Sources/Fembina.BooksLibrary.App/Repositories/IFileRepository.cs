namespace Fembina.BooksLibrary.App.Repositories;

public interface IFileRepository
{
    Guid Save(string path);

    bool Contains(Guid identifier);

    string Get(Guid identifier);

    bool Remove(Guid identifier);
}