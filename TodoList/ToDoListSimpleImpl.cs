
namespace TodoList
{   
    using Types;
    public class ToDoListSimpleImpl : IToDoList
    {
        private SortedDictionary<string, Task> Tasks;

        public ToDoListSimpleImpl()
        {
            Tasks = new SortedDictionary<string, Task>();
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task.Title, task);
        }

        public List<Task> SearchTasksByTags(List<string> tags)
        {
            List<Task> result = new List<Task>();
            foreach (var pair in Tasks)
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    if (pair.Value.Tags.Contains(tags[i]))
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

        public List<Task> LastTasks(int n)
        { 
         List<Task> result = new List<Task>();
          
        }

        public void Clear()
        {
          Tasks.Clear();  
        }

    }
}


