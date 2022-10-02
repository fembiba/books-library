using System.Windows.Media.Imaging;

namespace Fembina.BooksLibrary.App.Extensions;

public static class UriExtensions
{
    public static BitmapImage CreateImage(this Uri uri)
    {
        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = uri;
        image.EndInit();
        return image;
    }
}