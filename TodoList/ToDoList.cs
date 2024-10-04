using System.Net.Sockets;

namespace TodoList;

public interface ToDoList
{
    void addTask(Task task);
    List<Task> searchTasksByTags(List<String> tags);
    bool removeByTitle(String title);
    List<Task> lastTasks(int N);
    void clear();
}

