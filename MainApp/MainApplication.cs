using TodoList;
using Types;
using App;
using App.ConsoleApp;
using App.Rest;
using App.Gui;

namespace MainApp;

public class MainApplication
{

    public static void Main(string[] args)
    {
        var appConfig = ParseArgs(args);

        IToDoList todoList = CreateTodoList(appConfig);
        IApp app = CreateApp(appConfig);
        app.SetConfig(appConfig);
        app.StartApp(todoList);
    }

    static Config ParseArgs(string[] args)
    {
        return new Config(args);
    }

    static IApp CreateApp(Config config)
    {
        if (config.AppType == AppType.Console)
        {
            return new ConsoleApp();
        }
        else if (config.AppType == AppType.Rest)
        {
            return new RestWebApp();
        }
        else if (config.AppType == AppType.Gui)
        {
            Console.WriteLine("Started");
            return new GuiApp();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    static IToDoList CreateTodoList(Config config)
    {
        if (config.DbType == DbType.Json)
        {
            return new ToDoListSimpleImpl(config.Path);
        }
        else
        {
            return new ToDoListPostgres(config.DbName);
        }

    }
}