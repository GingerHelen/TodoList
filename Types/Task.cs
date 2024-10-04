
class Task
{
    public String title { get; private set; }
    public String description { get; private set; }
    public DateTime deadline { get; private set; }
    public List<String> tags { get; private set; }

    public Task(String title, String description, DateTime deadline, List<String> tags)
    {
        this.title = title;
        this.description = description;
        this.deadline = deadline;
        this.tags = tags;
    }
}
