using Fembina.BooksLibrary.App.Entities;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Validators;

public sealed class BookEntityValidator : AbstractValidator<BookEntity>
{
    public BookEntityValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();

        var minLength = 3;

        var textRgx = @"^[a-zA-Z\s,.:]*$";

        var maxTitleLength = 32;

        RuleFor(x => x.Title)
            .NotNull()
            .MinimumLength(minLength)
            .MaximumLength(maxTitleLength)
            .Matches(textRgx)
            .WithMessage("Name of the book should consist only of letters and symbols ':', ',', '.'. " +
                         $"Also the name must be no less than {minLength} and no more than {maxTitleLength} symbols.");

        var maxDescriptionLength = 64;

        RuleFor(x => x.Description)
            .NotNull()
            .MinimumLength(minLength)
            .MaximumLength(maxDescriptionLength)
            .Matches(textRgx)
            .WithMessage("Description of the book should consist only of letters and symbols ':', ',', '.'. " +
                         $"Also the description must be no less than {minLength} and no more than {maxDescriptionLength} symbols.");

        var minYear = 1800;

        var maxYear = DateTime.Now.Year;

        RuleFor(x => x.PublishYear)
            .NotNull()
            .GreaterThan(minYear)
            .LessThanOrEqualTo(maxYear)
            .WithMessage($"Release year must be greater than {minYear} and less than or equal to {maxYear}.");

        var ibsnRgx = @"^(\d-?){13}$";

        RuleFor(x => x.Isbn)
            .NotNull()
            .Matches(ibsnRgx)
            .WithMessage("ISBN must comply with the IBSN standard.");

        var nameRgx = @"^[a-zA-Z]*$";

        var maxNameLength = 16;

        RuleFor(x => x.AuthorFirstName)
            .NotNull()
            .MinimumLength(minLength)
            .MaximumLength(maxNameLength)
            .Matches(nameRgx)
            .WithMessage("First author name of the book should consist only letters. " +
                         $"Also the name must be no less than {minLength} and no more than {maxNameLength} symbols.");

        RuleFor(x => x.AuthorLastName)
            .NotNull()
            .MinimumLength(minLength)
            .MaximumLength(maxNameLength)
            .Matches(nameRgx)
            .WithMessage("Last author name of the book should consist only letters. " +
                         $"Also the name must be no less than {minLength} and no more than {maxNameLength} symbols.");

        RuleFor(x => x.FileId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotNull()
            .NotEmpty();
    }
}