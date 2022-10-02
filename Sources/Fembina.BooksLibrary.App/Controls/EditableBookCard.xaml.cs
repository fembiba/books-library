using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fembina.BooksLibrary.App.Arguments;
using Fembina.BooksLibrary.App.Delegates;
using Fembina.BooksLibrary.App.Extensions;
using Fembina.BooksLibrary.App.Validators;
using Microsoft.Win32;

namespace Fembina.BooksLibrary.App.Controls;

public partial class EditableBookCard : UserControl
{
    public static readonly DependencyProperty IdentifierProperty;

    public static readonly DependencyProperty TitleProperty;

    public static readonly DependencyProperty DescriptionProperty;

    public static readonly DependencyProperty AuthorFirstNameProperty;

    public static readonly DependencyProperty AuthorLastNameProperty;

    public static readonly DependencyProperty YearProperty;

    public static readonly DependencyProperty IsbnProperty;

    public static readonly DependencyProperty SourceProperty;

    public static readonly RoutedEvent SourceUpdatedEvent;

    public static readonly RoutedEvent TitleChangedEvent;

    public static readonly RoutedEvent DescriptionChangedEvent;

    public static readonly RoutedEvent AuthorFirstNameChangedEvent;

    public static readonly RoutedEvent AuthorLastNameChangedEvent;

    public static readonly RoutedEvent IsbnChangedEvent;

    public static readonly RoutedEvent YearChangedEvent;

    public static readonly RoutedEvent SaveClickEvent;

    public static readonly RoutedEvent BackClickEvent;

    static EditableBookCard()
    {
        IdentifierProperty = DependencyProperty.Register("Identifier",
            typeof(int),
            typeof(EditableBookCard));

        TitleProperty = DependencyProperty.Register("Title",
            typeof(string),
            typeof(EditableBookCard));

        DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string),
            typeof(EditableBookCard));

        AuthorFirstNameProperty = DependencyProperty.Register("AuthorFirstName",
            typeof(string),
            typeof(EditableBookCard));

        AuthorLastNameProperty = DependencyProperty.Register("AuthorLastName",
            typeof(string),
            typeof(EditableBookCard));

        YearProperty = DependencyProperty.Register("Year",
            typeof(int),
            typeof(EditableBookCard));

        IsbnProperty = DependencyProperty.Register("Isbn",
            typeof(string),
            typeof(EditableBookCard));

        SourceProperty = DependencyProperty.Register("Source",
            typeof(ImageSource),
            typeof(EditableBookCard));

        SourceUpdatedEvent = EventManager.RegisterRoutedEvent("SourceUpdated",
            RoutingStrategy.Bubble,
            typeof(SourceUpdatedHandler),
            typeof(EditableBookCard));

        TitleChangedEvent = EventManager.RegisterRoutedEvent("TitleChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        DescriptionChangedEvent = EventManager.RegisterRoutedEvent("DescriptionChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        AuthorFirstNameChangedEvent = EventManager.RegisterRoutedEvent("AuthorFirstNameChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        AuthorLastNameChangedEvent = EventManager.RegisterRoutedEvent("AuthorLastNameChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        IsbnChangedEvent = EventManager.RegisterRoutedEvent("IsbnChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        YearChangedEvent = EventManager.RegisterRoutedEvent("YearChanged",
            RoutingStrategy.Bubble,
            typeof(TextChangedEventHandler),
            typeof(EditableBookCard));

        SaveClickEvent = EventManager.RegisterRoutedEvent("SaveClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EditableBookCard));

        BackClickEvent = EventManager.RegisterRoutedEvent("BackClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EditableBookCard));
    }

    public EditableBookCard()
    {
        InitializeComponent();
    }

    public int Identifier
    {
        get => (int)GetValue(IdentifierProperty);
        set => SetValue(IdentifierProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string AuthorFirstName
    {
        get => (string)GetValue(AuthorFirstNameProperty);
        set => SetValue(AuthorFirstNameProperty, value);
    }

    public string AuthorLastName
    {
        get => (string)GetValue(AuthorLastNameProperty);
        set => SetValue(AuthorLastNameProperty, value);
    }

    public int Year
    {
        get => (int)GetValue(YearProperty);
        set => SetValue(YearProperty, value);
    }

    public string Isbn
    {
        get => (string)GetValue(IsbnProperty);
        set => SetValue(IsbnProperty, value);
    }

    public ImageSource Source
    {
        get => (ImageSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public event SourceUpdatedHandler SourceUpdated
    {
        add => AddHandler(SourceUpdatedEvent, value);
        remove => RemoveHandler(SourceUpdatedEvent, value);
    }

    public event TextChangedEventHandler TitleChanged
    {
        add => AddHandler(TitleChangedEvent, value);
        remove => RemoveHandler(TitleChangedEvent, value);
    }

    public event TextChangedEventHandler DescriptionChanged
    {
        add => AddHandler(DescriptionChangedEvent, value);
        remove => RemoveHandler(DescriptionChangedEvent, value);
    }

    public event TextChangedEventHandler AuthorFirstNameChanged
    {
        add => AddHandler(AuthorFirstNameChangedEvent, value);
        remove => RemoveHandler(AuthorFirstNameChangedEvent, value);
    }

    public event TextChangedEventHandler AuthorLastNameChanged
    {
        add => AddHandler(AuthorLastNameChangedEvent, value);
        remove => RemoveHandler(AuthorLastNameChangedEvent, value);
    }

    public event TextChangedEventHandler IsbnChanged
    {
        add => AddHandler(IsbnChangedEvent, value);
        remove => RemoveHandler(IsbnChangedEvent, value);
    }

    public event TextChangedEventHandler YearChanged
    {
        add => AddHandler(YearChangedEvent, value);
        remove => RemoveHandler(YearChangedEvent, value);
    }

    public event RoutedEventHandler SaveClick
    {
        add => AddHandler(SaveClickEvent, value);
        remove => RemoveHandler(SaveClickEvent, value);
    }

    public event RoutedEventHandler BackClick
    {
        add => AddHandler(BackClickEvent, value);
        remove => RemoveHandler(BackClickEvent, value);
    }

    private void OnSourceTryChanged(object sender, MouseButtonEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = FileFormatValidator.SystemFilesFilter
        };

        var result = dialog.ShowDialog();

        if (result != true) return;

        var path = dialog.FileName;

        Source = new Uri(path).CreateImage();

        RaiseEvent(new SourceUpdatedArgs(SourceUpdatedEvent, path));
    }

    private void OnSaveButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(SaveClickEvent));
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(BackClickEvent));
    }

    private void OnTitleTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(TitleChangedEvent, e.UndoAction, e.Changes));
    }

    private void OnDescriptionTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(DescriptionChangedEvent, e.UndoAction, e.Changes));
    }

    private void OnAuthorFirstNameTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(AuthorFirstNameChangedEvent, e.UndoAction, e.Changes));
    }

    private void OnAuthorLastNameTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(AuthorLastNameChangedEvent, e.UndoAction, e.Changes));
    }

    private void OnYearTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(YearChangedEvent, e.UndoAction, e.Changes));
    }

    private void OnIsbnTextChanged(object sender, TextChangedEventArgs e)
    {
        RaiseEvent(new TextChangedEventArgs(IsbnChangedEvent, e.UndoAction, e.Changes));
    }
}