using TodoList;
using Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace App.Rest;

public class RestWebApp : IApp
{
    private int port;
    public void SetConfig(Config config)
    {
        port = config.HttpPort;
    }

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
        
        // app.MapPost("/add_task", () =>
        // {
        //     if (title == null || description == null || deadline == null || )
        //     {
        //         return Results.BadRequest();
        //     }
        // }).WithName("Exit").WithOpenApi();
        
        app.Run();
    }

}