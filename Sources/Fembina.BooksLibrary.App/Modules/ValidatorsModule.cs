using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Entities;
using Fembina.BooksLibrary.App.Validators;
using FluentValidation;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class ValidatorsModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookEntityValidator>()
            .AsSelf()
            .As<IValidator<BookEntity>>()
            .ToSingleton();

        container.SetTarget<FileFormatValidator>()
            .AsSelf()
            .As<IValidator<string>>()
            .ToSingleton();
    }
}