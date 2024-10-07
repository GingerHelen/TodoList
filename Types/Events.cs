namespace Types;

public abstract class Event {
    public int Id { get; protected set; }
}

public class AddTaskEvent : Event
{
    public TaskToDo Task { get; private set; }

    public AddTaskEvent(TaskToDo taskToDo)
    {
        Task = taskToDo;
        Id = 1;
    }
}

public class SearchTagsEvent : Event
{
    public List<string> Tags { get; private set; }

    public SearchTagsEvent(List<string> tags)
    {
        Tags = tags;
        Id = 2;
    }
}

public class LastTasksEvent : Event
{
    public int Amount { get; private set; }

    public LastTasksEvent(int amount)
    {
        Amount = amount;
        Id = 3;
    }
}

public class ExitEvent : Event
{
    public ExitEvent()
    {
        Id = 4;
    }
}


