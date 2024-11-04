using Types;

namespace Tests;

using NUnit.Framework;
using TodoList;

[TestFixture]
public class TodoListSimpleImplTest
{
    private ToDoListSimpleImpl todoList; 
    
    [SetUp]
    public void Setup()
    {
        using (StreamWriter outputFile = new StreamWriter("todolisttest.json", false))
        {
            outputFile.Write(string.Empty);
        }
        todoList = new ToDoListSimpleImpl("todolisttest.json");
    }

    [Test]
    public void TestAddTask1()
    {
        Assert.True(todoList.AddTask(new TaskToDo("gg", "", DateTime.Now, new List<string>())).Result);
        
        if (todoList.LastTasks(2).Result.Count != 1)
        {
            Assert.Fail();
        }
        Assert.Pass();
    }

    [Test]
    public void TestLastTasks1()
    {
        DateTime specificDateTime1 = new DateTime(2024, 10, 11);
        DateTime specificDateTime2 = new DateTime(2024, 11, 12);
        var task1 = new TaskToDo("first", "", specificDateTime1, new List<string>());
        var task2 = new TaskToDo("second", "", specificDateTime2, new List<string>());
        var task3 = new TaskToDo("third", "", specificDateTime2, new List<string>());
       
        var t1 = todoList.AddTask(task1);
        var t2 = todoList.AddTask(task2);
        var t3 = todoList.AddTask(task3);
        Assert.True(t1.Result);
        Assert.True(t2.Result);
        Assert.True(t3.Result);
        if (todoList.LastTasks(1).Result.FirstOrDefault(new TaskToDo("title", "", DateTime.Now, new List<string>())).Title == "first")
        {
            Assert.Pass();
        }
        else
        { Assert.Fail();
        }
    }

    [Test]
    public void TestRemoveByTitle()
    {
        var task1 = new TaskToDo("task1", "", DateTime.Now, new List<string>());
        var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
        var t1 = todoList.AddTask(task1);
        var t2 = todoList.AddTask(task2);
        Assert.True(t1.Result);
        Assert.True(t2.Result);
        Assert.True(todoList.RemoveByTitle("task1").Result);
        var lastTasks = todoList.LastTasks(2).Result;
        Assert.True(lastTasks.Count == 1 & lastTasks[0].Title == "task2");
    }

    [Test]
    public void TestSearchByTags()
    {
        var list = new List<string>();
        list.Add("tagg");
        var task1 = new TaskToDo("task1", "", DateTime.Now, list);
        var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
        var t1 = todoList.AddTask(task1);
        var t2 = todoList.AddTask(task2);
        Assert.True(t1.Result);
        Assert.True(t2.Result);
        var search = todoList.SearchTasksByTags(list).Result;
        Console.WriteLine(search.Count);
        Assert.True(search.Count == 1 && search[0].Title == "task1", search.Count.ToString());
    }

    [Test]
    public void TestClear()
    {
        var task1 = new TaskToDo("task1", "", DateTime.Now, new List<string>());
        var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
        var t1 = todoList.AddTask(task1);
        var t2 = todoList.AddTask(task2);
        var t3 = todoList.Clear();
        Assert.True(t1.Result);
        Assert.True(t2.Result);
        t3.Wait();
        Assert.True(todoList.LastTasks(2).Result.Count == 0);
    }
}