namespace Fembina.BooksLibrary.App;

public static class AppConfiguration
{
    private const string DatabaseFileEnv = "APP_DATABASE_FILE_PATH";

    private const string DatabaseFileDefault = "./Cache/Database.db";

    private const string FilesStorageEnv = "APP_FILES_STORAGE_PATH";

    private const string FilesStorageDefault = "./Cache/Storage";

    public static string DatabaseFile { get; }
        = Environment.GetEnvironmentVariable(DatabaseFileEnv) ?? DatabaseFileDefault;

    public static string FilesStorage { get; }
        = Environment.GetEnvironmentVariable(FilesStorageEnv) ?? FilesStorageDefault;
}