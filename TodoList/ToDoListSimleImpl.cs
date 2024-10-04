namespace TodoList;

public class ToDoListSimleImpl : ToDoList
{
    private SortedDictionary<DateTime, Task> tasks; 
    public void addTask(Task task)
    {
        tasks.Add(, task);
        throw new NotImplementedException();
    }

    public List<Task> searchTasksByTags(List<string> tags)
    {
        throw new NotImplementedException();
    }

    public bool removeByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public List<Task> lastTasks(int N)
    {
        throw new NotImplementedException();
    }

    public void clear()
    {
        throw new NotImplementedException();
    }

}