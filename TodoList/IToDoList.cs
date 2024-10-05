namespace TodoList
{   
    using Types;
    public interface IToDoList
    {
        void AddTask(Task task);
        List<Task> SearchTasksByTags(List<string> tags);
        bool RemoveByTitle(string title);
        List<Task> LastTasks(int n);
        void Clear();
    }
}


