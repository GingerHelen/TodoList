using TodoList;
using Types;

namespace Application;

public class MainApplication
{
    
    public static void Main(string[] args)
    {
        var appConfig = new Config(args);
        
        IToDoList todoList = CreateTodoList(appConfig);
        App app = CreateApp(appConfig);
        
        // Start application
        app.StartApp(todoList);
    }

    static App CreateApp(Config config)
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
        return new ToDoListSimpleImpl();
    }
}