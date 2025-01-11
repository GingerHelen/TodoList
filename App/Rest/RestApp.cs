using TodoList;
using Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
namespace App.Rest;

public class RestWebApp : IApp
{
    public void StartApp(IToDoList todoList)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapGet("/last_tasks", (int? n) =>
        {
            var tasks = todoList.LastTasks(n ?? 5).Result;
            return Results.Ok(new {
                amount = tasks.Count,
                tasks
            });
        }).WithName("Last tasks").WithOpenApi();
        
        app.MapGet("/exit", () =>
        {
            Console.WriteLine("Exited successfully");
            app.StopAsync();
            return Results.Ok();
        }).WithName("Exit").WithOpenApi();
        
        app.MapGet("/add_task", (string? title, string? description, DateTime? deadline, string[]? tags) =>
        {
            if (title == null || description == null || deadline == null || tags == null)
            {
                return Results.BadRequest();
            }

            todoList.AddTask(new TaskToDo(
                title,
                description,
                deadline.Value,
                new List<string>(tags)
                ));

            return Results.Ok();
        }).WithName("Add task").WithOpenApi();
        
        app.MapGet("/search_task", (string[]? tags) =>
        {
            if (tags == null)
            {
                return Results.BadRequest();
            }

            var res = todoList.SearchTasksByTags(new List<string>(tags)).Result;
            
            return Results.Ok(res);
        }).WithName("Search task").WithOpenApi();
        
        app.Run();
    }

}