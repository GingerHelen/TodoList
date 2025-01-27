using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TodoList;
using Types;

namespace App.Gui;

public class GuiApp : AbstractApp
{

    public static void Main(string[] args) {
    }

    public virtual void StartApp()
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime([]);
    }


    public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<AvaloniaApp.App>()
                      .UsePlatformDetect()
                      .LogToTrace();
}
