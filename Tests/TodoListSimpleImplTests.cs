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
    public void Test1()
    {
        Assert.Pass();
    }
}