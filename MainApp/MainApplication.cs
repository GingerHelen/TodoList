using TodoList;
using Types;
using App;
using App.ConsoleApp;
using App.Rest;
using App.Gui;

using Microsoft.Extensions.DependencyInjection;

namespace MainApp;

public class MainApplication
{

    public static void Main(string[] args)
    {
        var appConfig = ParseArgs(args);

        var services = new ServiceCollection();

        services.AddScoped<IToDoList>(_ => CreateTodoList(appConfig));
        services.AddTransient<IApp>(provider => CreateApp(provider.GetService<IToDoList>()!, appConfig));

        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetService<IApp>();

        app!.StartApp();
    }

    static Config ParseArgs(string[] args)
    {
        return new Config(args);
    }

    static IApp CreateApp(IToDoList todolist, Config config)
    {
        IApp app = config.AppType switch
        {
            AppType.Rest => new RestWebApp(),
            AppType.Gui => new GuiApp(),
            _ => new ConsoleApp()
        };
        app.SetConfig(config);
        app.SetTodoList(todolist);
        return app;
    }

    static IToDoList CreateTodoList(Config config)
    {
        IToDoList todolist = config.DbType switch {
            DbType.Sql => new ToDoListPostgres(config.DbName),
            _ => new ToDoListSimpleImpl(config.Path),
        };
        return todolist;
    }
}
