using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fembina.BooksLibrary.App.Controls;

public partial class BookCard : UserControl
{
    public static readonly DependencyProperty IdentifierProperty;

    public static readonly DependencyProperty TitleProperty;

    public static readonly DependencyProperty DescriptionProperty;

    public static readonly DependencyProperty AuthorProperty;

    public static readonly DependencyProperty YearProperty;

    public static readonly DependencyProperty IsbnProperty;

    public static readonly DependencyProperty SourceProperty;

    public static readonly RoutedEvent EditClickEvent;

    public static readonly RoutedEvent DeleteClickEvent;

    static BookCard()
    {
        IdentifierProperty = DependencyProperty.Register("Identifier",
            typeof(int),
            typeof(BookCard),
            new FrameworkPropertyMetadata(-1,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        TitleProperty = DependencyProperty.Register("Title",
            typeof(string),
            typeof(BookCard),
            new FrameworkPropertyMetadata("Title",
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string),
            typeof(BookCard),
            new FrameworkPropertyMetadata("There must be description",
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        AuthorProperty = DependencyProperty.Register("Author",
            typeof(string),
            typeof(BookCard),
            new FrameworkPropertyMetadata("First Last",
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        YearProperty = DependencyProperty.Register("Year",
            typeof(int),
            typeof(BookCard),
            new FrameworkPropertyMetadata(2000,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        IsbnProperty = DependencyProperty.Register("Isbn",
            typeof(string),
            typeof(BookCard),
            new FrameworkPropertyMetadata("000-0-00000-000-0",
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        SourceProperty = DependencyProperty.Register("Source",
            typeof(ImageSource),
            typeof(BookCard),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        EditClickEvent = EventManager.RegisterRoutedEvent("EditClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(BookCard));

        DeleteClickEvent = EventManager.RegisterRoutedEvent("DeleteClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(BookCard));
    }

    public BookCard()
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

    public string Author
    {
        get => (string)GetValue(AuthorProperty);
        set => SetValue(AuthorProperty, value);
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

    public event RoutedEventHandler EditClick
    {
        add => AddHandler(EditClickEvent, value);
        remove => RemoveHandler(EditClickEvent, value);
    }

    public event RoutedEventHandler DeleteClick
    {
        add => AddHandler(DeleteClickEvent, value);
        remove => RemoveHandler(DeleteClickEvent, value);
    }

    private void OnEditButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(EditClickEvent));
    }

    private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(DeleteClickEvent));
    }
}