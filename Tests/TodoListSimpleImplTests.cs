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
        todoList = new ToDoListSimpleImpl();
    }

    [Test]
    public void TestAddTask1()
    {
        todoList.AddTask(new TaskToDo("gg", "", DateTime.Now, new List<string>()));
        if (todoList.LastTasks(2).Count != 1)
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
       
        todoList.AddTask(task1);
        todoList.AddTask(task2);
        todoList.AddTask(task3);
        if (todoList.LastTasks(1).FirstOrDefault(new TaskToDo("title", "", DateTime.Now, new List<string>())).Title == "first")
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
        todoList.AddTask(task1);
        todoList.AddTask(task2);
        Assert.True(todoList.RemoveByTitle("task1"));
        var lastTasks = todoList.LastTasks(2);
        Assert.True(lastTasks.Count == 1 & lastTasks[0].Title == "task2");
    }

    [Test]
    public void TestSearchByTags()
    {
        var list = new List<string>();
        list.Add("tagg");
        var task1 = new TaskToDo("task1", "", DateTime.Now, list);
        var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
        todoList.AddTask(task1);
        todoList.AddTask(task2);
        var search = todoList.SearchTasksByTags(list);
        Assert.True(search.Count == 1 & search[0].Title == "task1");
    }

    [Test]
    public void TestClear()
    {
        var task1 = new TaskToDo("task1", "", DateTime.Now, new List<string>());
        var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
        todoList.AddTask(task1);
        todoList.AddTask(task2);
        todoList.Clear();
        Assert.True(todoList.LastTasks(2).Count == 0);
    }
}