using Console = System.Console;

namespace App.ConsoleApp;

using Types;
using TodoList;

class WrongOptionException : Exception
{
}

class NullInputException : Exception
{
}

class WrongArgumentException : Exception
{
}

public class ConsoleApp : IApp
{
    private IToDoList _toDoList;
    private bool _running;

    delegate Event EventGetter();

    private static Dictionary<string, EventGetter> _eventGetters = new Dictionary<string, EventGetter>()
    {
        { "1", GetOne },
        { "2", GetTwo },
        { "3", GetThree },
        { "4", GetFour }
    };

    static Event GetOne()
    {
        Console.WriteLine("New Task");
        Console.Write("Title: ");
        var title = GetInput();
        Console.Write("Description: ");
        var description = GetInput();
        Console.Write("DeadLine: ");
        var deadLineString = GetInput();
        DateTime deadLine;
        try
        {
            deadLine = DateTime.Parse(deadLineString);
        }
        catch (FormatException)
        {
            throw new WrongArgumentException();
        }

        Console.WriteLine("Tags:");
        int i = 0;
        var tags = new List<string>();
        while (true)
        {
            i++;
            Console.Write(i);
            Console.Write(": ");
            var tag = GetInput();
            if (string.IsNullOrEmpty(tag))
            {
                break;
            }

            tags.Add(tag);
        }

        var task = new TaskToDo(title, description, deadLine, tags);

        return new AddTaskEvent(task);
    }

    static Event GetTwo()
    {
        Console.Write("Search tasks by Tag: ");
        var tag = GetInput();
        var listOfTags = new List<string>();
        listOfTags.Add(tag);
        return new SearchTagsEvent(listOfTags);
    }

    static Event GetThree()
    {
        Console.Write("Amount: ");
        var amountString = GetInput();
        int amount;
        try
        {
            amount = Convert.ToInt32(amountString);
        }
        catch (FormatException)
        {
            throw new WrongArgumentException();
        }

        return new LastTasksEvent(amount);
    }

    static Event GetFour()
    {
        return new ExitEvent();
    }

    static Event GetEvent()
    {
        Console.WriteLine("Enter the number of action and press [Enter]. Then follow instructions.");
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Add task");
        Console.WriteLine("2. Search task");
        Console.WriteLine("3. Last tasks");
        Console.WriteLine("4. Exit");
        Console.Write("> ");
        var input = GetInput();
        return _eventGetters.GetValueOrDefault(input, () =>
        {
            Console.WriteLine("Wrong option");
            throw new WrongOptionException();
        })();
    }

    static string GetInput()
    {
        var input = Console.ReadLine();
        if (input is null)
        {
            throw new NullInputException();
        }

        return input.Trim();
    }

    public void StartApp(IToDoList todoList)
    {
        _toDoList = todoList;
        _running = true;
        while (_running)
        {
            try
            {
                var @event = GetEvent();
                ProcessEvent(@event);
            }
            catch (WrongArgumentException)
            {
                Console.WriteLine("Wrong input. Try again.");
            }
            catch (WrongOptionException)
            {
                Console.WriteLine("Wrong option. Try again.");
            }
        }
    }

    public void ProcessEvent(Event @event)
    {
        if (@event is AddTaskEvent)
        {
            var addTaskEvent = (AddTaskEvent)@event;
            if (_toDoList.AddTask(addTaskEvent.Task).Result)
            {
                Console.WriteLine("Task was successfully added!");
            }
            else
            {
                Console.WriteLine("Cannot add task: this title exist already");
            }
        }
        else if (@event is SearchTagsEvent)
        {
            var searchTagsEvent = (SearchTagsEvent)@event;
            var resultOfSearch = _toDoList.SearchTasksByTags(searchTagsEvent.Tags).Result;
            if (resultOfSearch.Count > 0)
            {
                Console.WriteLine("Found tasks:");
                foreach (var task in resultOfSearch)
                {
                    PrintTask(task);
                }
            }
            else
            {
                Console.WriteLine("No such tasks found");
            }
        }
        else if (@event is LastTasksEvent)
        {
            var lastTasksEvent = (LastTasksEvent)@event;
            var tasks = _toDoList.LastTasks(lastTasksEvent.Amount).Result;
            if (tasks.Count != 0)
            {
                foreach (var task in tasks)
                {
                    PrintTask(task);
                }
            }
            else
            {
                Console.WriteLine("There are no tasks in the list, you can add some tasks:)");
            }
        }
        else if (@event is ExitEvent)
        {
            var exitEvent = (ExitEvent)@event;
            Console.WriteLine("Exited successfully!");
            _running = false;
        }
    }

    static void PrintTask(TaskToDo task)
    {
        Console.WriteLine("Title: " + task.Title);
        Console.WriteLine("Description: " + task.Description);
        Console.WriteLine("Deadline: " + task.Deadline.Date);
        Console.WriteLine("Tags: ");
        for (int i = 0; i < task.Tags.Count; i++)
        {
            Console.WriteLine(task.Tags[i] + " ");
        }
    }
}