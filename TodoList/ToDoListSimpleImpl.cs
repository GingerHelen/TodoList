
namespace TodoList;

using Types;
public class ToDoListSimpleImpl : IToDoList
{
    private SortedDictionary<string, TaskToDo> Tasks;

    public ToDoListSimpleImpl()
    {
        Tasks = new SortedDictionary<string, TaskToDo>();
    }

    public bool AddTask(TaskToDo taskToDo)
    {
        if (Tasks.ContainsKey(taskToDo.Title))
        {
            return false;
        }
        Tasks.Add(taskToDo.Title, taskToDo);
        return true;
    }

    public List<TaskToDo> SearchTasksByTags(List<string> tags)
    {
        List<TaskToDo> result = new List<TaskToDo>();
        foreach (var pair in Tasks)
        {
            foreach (var tag in tags)
            {
                if (pair.Value.Tags.Contains(tag))
                {
                    result.Add(pair.Value);
                    break;
                } 
            }
        }
        return result;
    }

    public bool RemoveByTitle(string title)
    {
        return Tasks.Remove(title);
    }

    public List<TaskToDo> LastTasks(int n)
    { 
        List<TaskToDo> result = new List<TaskToDo>();
        foreach (var pair in Tasks) { 
            result.Add(pair.Value);
        }
        return result.OrderBy(x => x.Deadline).Take(n).ToList();
    }

    public void Clear()
    {
      Tasks.Clear();  
    }

}


