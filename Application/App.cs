namespace Application;
using Types;
using TodoList;

public interface App
{
    void StartApp(IToDoList todoList);

    void SetConfig(Config config)
    {
    }

}