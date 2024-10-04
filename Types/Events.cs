using System.Dynamic;

class Event {
    public int id { get; private set; }
}

class AddTaskEvent : Event
{
    public Task task { get; private set; }
}

class SearchTagsEvent : Event
{
    public List<String> tags { get; private set; }
}

class LastTasksEvent : Event
{
    public int amount { get; private set; }
}

class ExitEvent : Event
{
}