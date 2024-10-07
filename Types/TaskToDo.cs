namespace Types;

public class TaskToDo(string title, string description, DateTime deadline, List<string> tags)
{
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public DateTime Deadline { get; private set; } = deadline;
    public List<string> Tags { get; private set; } = tags;
}


