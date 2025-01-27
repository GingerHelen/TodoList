namespace App;
using Types;
using TodoList;

public interface IApp
{
    void StartApp(IToDoList todoList);

    void SetConfig(Config config)
    {
    }

}