using FluentValidation;

namespace Fembina.BooksLibrary.App.Validators;

public sealed class FileFormatValidator : AbstractValidator<string>
{
    public const string SystemFilesFilter = "Image Files (JPG,JPEG,PNG,GIF)|*.JPG;*.JPEG;*.PNG;*.GIF";

    public FileFormatValidator()
    {
        var minLength = 4;

        var formatRgx = @"[^\s]+(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$";

        RuleFor(x => x)
            .NotNull()
            .Matches(formatRgx);
    }
}