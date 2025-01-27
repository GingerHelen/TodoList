using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TodoList;
using Types;

namespace App.Gui;

public class GuiApp : IApp
{

    public static void Main(string[] args) {

    }
    
    public void SetConfig(Config config)
    {}

    public void StartApp(IToDoList todoList)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime([]);
    }


    public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<AvaloniaApp.App>()
                      .UsePlatformDetect()
                      .LogToTrace();
}