using Types;

namespace Tests;

using NUnit.Framework;
using TodoList;

[TestFixture]
public class TodoListPostgresTest
{
    private ToDoListPostgres todoList; 
    
    [SetUp]
    public void Setup()
    {
        todoList = new ToDoListPostgres("todolist_test");
        todoList.Clear().Wait();
    }

    [Test]
    public void Add_ToDoTask_ResultOK()
    {
        Assert.True(todoList.AddTask(new TaskToDo("gg", "", DateTime.Now, new List<string>())).Result);
        
        if (todoList.LastTasks(2).Result.Count != 1)
        {
            Assert.Fail();
        }
        Assert.Pass();
    }

    [Test]
    public void Get_LastTasks_Returns_One_Task()
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
    public void Remove_Task_By_Title_ResultOK()
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
    public void Search_By_Tags_Returns_One_Task()
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

    // [Test]
    // public void Clear_ResultOK()
    // {
    //     var task1 = new TaskToDo("task1", "", DateTime.Now, new List<string>());
    //     var task2 = new TaskToDo("task2", "", DateTime.Now, new List<string>());
    //     var t1 = todoList.AddTask(task1);
    //     var t2 = todoList.AddTask(task2);
    //     var t3 = todoList.Clear();
    //     Assert.True(t1.Result);
    //     Assert.True(t2.Result);
    //     t3.Wait();
    //     Assert.True(todoList.LastTasks(2).Result.Count == 0);
    // }
}