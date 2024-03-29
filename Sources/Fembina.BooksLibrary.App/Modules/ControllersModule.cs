﻿using DependencyManagement.Composition.Containers;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Modules;
using Fembina.BooksLibrary.App.Controllers;

namespace Fembina.BooksLibrary.App.Modules;

public sealed class ControllersModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<BookOperationsController>()
            .AsSelf()
            .As<Controller>()
            .ToTransient();
        
        container.SetTarget<EditBookFormController>()
            .AsSelf()
            .As<Controller>()
            .ToTransient();
        
        container.SetTarget<ListedBooksController>()
            .AsSelf()
            .As<Controller>()
            .ToTransient();

        container.SetTarget<CreateBookFormController>()
            .AsSelf()
            .As<Controller>()
            .ToTransient();
    }
}