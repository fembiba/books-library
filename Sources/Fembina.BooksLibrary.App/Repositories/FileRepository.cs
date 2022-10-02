using System.IO;

namespace Fembina.BooksLibrary.App.Repositories;

public sealed class FileRepository : IFileRepository
{
    public Guid Save(string path)
    {
        var fileId = Guid.NewGuid();
        var storagePath = BuildPath(fileId);
        Directory.CreateDirectory(Path.GetDirectoryName(storagePath)!);
        File.Copy(path, storagePath);
        return fileId;
    }

    public bool Contains(Guid identifier)
    {
        return File.Exists(BuildPath(identifier));
    }

    public string Get(Guid identifier)
    {
        return Contains(identifier)
            ? BuildPath(identifier)
            : throw new FileNotFoundException();
    }

    public bool Remove(Guid identifier)
    {
        if (!Contains(identifier)) return false;
        File.Delete(BuildPath(identifier));
        return true;
    }

    public static string BuildPath(Guid identifier)
    {
        return Path.Combine(Path.GetFullPath(AppConfiguration.FilesStorage), identifier.ToString());
    }
}