
using Newtonsoft.Json;

namespace TodoList;

using Types;
public class ToDoListSimpleImpl : IToDoList
{
    private SortedDictionary<string, TaskToDo> Tasks;
    private string _path;

    public ToDoListSimpleImpl(string path)
    {
        _path = path;
        Tasks = new SortedDictionary<string, TaskToDo>();
        if (!Path.Exists(_path))
        {
            using (File.Create(_path)){}
        }
        FetchTasks();
    }

    private void FetchTasks()
    {
        using (StreamReader r = new StreamReader(_path))
        {
            string json = r.ReadToEnd();
            List<TaskToDo>? tasks = JsonConvert.DeserializeObject<List<TaskToDo>>(json);
            Tasks.Clear();
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    Tasks.Add(task.Title, task);
                }
            }
        }
    }

    private void UpdateTasks()
    {
        List<TaskToDo> tasks = new List<TaskToDo>();
        foreach (var pair in Tasks)
        {
            tasks.Add(pair.Value);
        }
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        using (StreamWriter outputFile = new StreamWriter(_path, false))
        {
            outputFile.Write(json);
        }
    }

    public Task<bool> AddTask(TaskToDo taskToDo)
    {
        if (Tasks.ContainsKey(taskToDo.Title))
        {
            return Task.FromResult(false);
        }
        Tasks.Add(taskToDo.Title, taskToDo);
        UpdateTasks();
        return Task.FromResult(true);
    }

    public Task<List<TaskToDo>> SearchTasksByTags(List<string> tags)
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
        return Task.FromResult(result);
    }

    public Task<bool> RemoveByTitle(string title)
    {
        bool res = Tasks.Remove(title);
        UpdateTasks();
        return Task.FromResult(res);
    }

    public Task<List<TaskToDo>> LastTasks(int n)
    { 
        List<TaskToDo> result = new List<TaskToDo>();
        foreach (var pair in Tasks) { 
            result.Add(pair.Value);
        }
        return Task.FromResult(result.OrderBy(x => x.Deadline).Take(n).ToList());
    }

    public Task Clear()
    {
      Tasks.Clear(); 
      UpdateTasks();
      return Task.CompletedTask;
    }

}


