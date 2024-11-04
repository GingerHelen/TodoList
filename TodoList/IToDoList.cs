namespace TodoList;
using Types;
public interface IToDoList
{
    Task<bool> AddTask(TaskToDo taskToDo);
    Task<List<TaskToDo>> SearchTasksByTags(List<string> tags);
    Task<bool> RemoveByTitle(string title);
    Task<List<TaskToDo>> LastTasks(int n);
    Task Clear();
}



