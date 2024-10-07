namespace TodoList;
using Types;
public interface IToDoList
{
    void AddTask(TaskToDo taskToDo);
    List<TaskToDo> SearchTasksByTags(List<string> tags);
    bool RemoveByTitle(string title);
    List<TaskToDo> LastTasks(int n);
    void Clear();
}



