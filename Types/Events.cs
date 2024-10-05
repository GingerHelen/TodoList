namespace Types
{
    class Event {
        public int Id { get; private set; }
    }

    class AddTaskEvent : Event
    {
        public Task Task { get; private set; }
    }

    class SearchTagsEvent : Event
    {
        public List<string> Tags { get; private set; }
    }

    class LastTasksEvent : Event
    {
        public int Amount { get; private set; }
    }

    class ExitEvent : Event
    {
    }
}

