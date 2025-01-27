using TodoList;
using Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace App.Rest;

public class RestWebApp : AbstractApp
{
    private int port;
    public virtual void SetConfig(Config config)
    {
        port = config.HttpPort;
    }

    public virtual void StartApp()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapGet("/last_tasks", (int? n) =>
        {
            var tasks = _todolist.LastTasks(n ?? 5).Result;
            return Results.Json(new {
                amount = tasks.Count,
                tasks
            });
        }).WithName("Last tasks").WithOpenApi();
        
        app.MapPost("/exit", () =>
        {
            Console.WriteLine("Exited successfully");
            Environment.Exit(0);
        }).WithName("Exit").WithOpenApi();
        
        app.MapPost("/add_task", ([FromBody] TaskToDo? task) =>
        {
            if (task == null)
            {
                return Results.BadRequest();
            }

            _todolist.AddTask(task);

            return Results.Ok();
        }).WithName("Add task").WithOpenApi();
        
        app.MapGet("/search_task", ([FromBody] List<string>? tags) =>
        {
            if (tags == null)
            {
                return Results.BadRequest();
            }

            var res = this._todolist.SearchTasksByTags(tags).Result;
            
            return Results.Ok(res);
        }).WithName("Search task").WithOpenApi();
        
        app.Run();
    }

}
