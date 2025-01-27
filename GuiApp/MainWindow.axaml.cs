using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;

namespace AvaloniaApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddTaskClick(object sender, RoutedEventArgs args)
    {
        // Логика обработки нажатия
        var dataPage = this.FindControl<ContentControl>("DataPage");
        var dataTemplate = Application.Current!.Resources["AddTaskTemplate"] as IDataTemplate;

        dataPage!.ContentTemplate = dataTemplate;

        Console.WriteLine("Add task is chosen");
    }

    private void AddTaskButtonClick(object sender, RoutedEventArgs e)
    {
        // Этот код выполнится, когда кнопка будет нажата
        var title = this.FindControl<TextBox>("AddTaskTitleTextBox");
        if (title != null) {
            Console.WriteLine(title.Text);
        } else {
            Console.WriteLine("WTF");
        }
        
    }

    private void ExitClick(object sender, RoutedEventArgs args)
    {
        // Логика обработки нажатия
        var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!;
        lifetime.Shutdown();
    }

    private void SearchTaskClick(object sender, RoutedEventArgs args)
    {
        // Логика обработки нажатия
        var dataPage = this.FindControl<ContentControl>("DataPage");
        var dataTemplate = Application.Current!.Resources["AddTaskTemplate"] as IDataTemplate;

        dataPage!.ContentTemplate = dataTemplate;

        Console.WriteLine("Add task is chosen");
    }
}