namespace App;
using Types;
using TodoList;

public interface IApp
{
    void StartApp() {}

    void SetConfig(Config config) {}

    void SetTodoList(IToDoList todolist);
}
