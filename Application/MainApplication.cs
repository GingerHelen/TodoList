using TodoList;
using Types;

namespace Application;

public class MainApplication
{
    
    public static void Main(string[] args)
    {
        var appConfig = new Config(args);
        
        IToDoList todoList = CreateTodoList(appConfig);
        IApp app = CreateApp(appConfig);
        app.SetConfig(appConfig);
        app.StartApp(todoList);
    }

    static IApp CreateApp(Config config)
    {
        if (config.AppType == AppType.Console)
       {
            return new ConsoleApp();
        }
        else
        {
            throw new NotImplementedException();
        }
    }
    
    static IToDoList CreateTodoList(Config config)
    {
        return new ToDoListSimpleImpl(config.Path);
    }
}