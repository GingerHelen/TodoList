using TodoList;

namespace App;

public abstract class AbstractApp : IApp {
    protected IToDoList _todolist;

    public void SetTodoList(IToDoList todolist) {
        _todolist = todolist;
    }
}
