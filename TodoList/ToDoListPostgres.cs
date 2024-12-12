using System.Data;

namespace TodoList;

using Npgsql;
using Dapper;  
using Types;

public class ToDoListPostgres : IToDoList
{
    private string psqlURI = "Host=localhost;Port=5432;Username=postgres;Password=postgres";

    private class SqlTaskToDo {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string[] Tags { get; set; }
    }
    
    IDbConnection CreateConnectionToDb()
    {
        return new NpgsqlConnection(psqlURI);
    }

    public ToDoListPostgres(string dbname)
    {
        psqlURI += $";Database={dbname}";
        using (IDbConnection connection = CreateConnectionToDb())
        {
            connection.Open();
            CreateToDoList(connection);
        }
    }
    
    private static string TagsToString(List<string> tags)
    {
        var stags = "{";
        foreach (var tag in tags)
        {
            if (stags.Length > 1)
            {
                stags += ", ";
            }

            stags += $"\"{tag}\"";
        }
        stags += "}";
        return stags;
    }

    private void CreateToDoList(IDbConnection connection)
    {
        string createToDoListQuery = @"
            CREATE TABLE IF NOT EXISTS ToDoList (
                Title TEXT PRIMARY KEY,
                Description TEXT NOT NULL,
                Deadline DATE NOT NULL,
                Tags TEXT[]
            );
        ";
        connection.Execute(createToDoListQuery);
    }

    public async Task<bool> AddTask(TaskToDo taskToDo)
    {
        using (var connection = CreateConnectionToDb())
        {
            connection.Open();
            var addTaskQuery =
                string.Format("INSERT INTO ToDoList VALUES ('{0}', '{1}', '{2}', '{3}')"
                    , taskToDo.Title
                    , taskToDo.Description
                    , taskToDo.Deadline
                    , TagsToString(taskToDo.Tags));
            var res = await connection.ExecuteAsync(addTaskQuery);
            return res > 0;
        }
    }

    public async Task<List<TaskToDo>> SearchTasksByTags(List<string> tags)
    {
        using (var connection = CreateConnectionToDb())
        {
            connection.Open();
            var searchTasksByTagsQuery =
                string.Format(
                    @"SELECT Title, Description, Deadline, Tags 
                      FROM ToDoList 
                      WHERE Tags && '{0}'"
                    , TagsToString(tags));
            return (await connection.QueryAsync<SqlTaskToDo>(searchTasksByTagsQuery))
                .Select(stask => new TaskToDo(
                    stask.Title,
                    stask.Description,
                    stask.Deadline,
                    new List<string>(stask.Tags))
                ).ToList();
        }
    }

    public async Task<bool> RemoveByTitle(string title)
    {
        using (var connection = CreateConnectionToDb())
        {
            connection.Open();
            var removeByTitleQuery =
                string.Format("DELETE FROM ToDoList WHERE Title = '{0}'", title);
            var res = await connection.ExecuteAsync(removeByTitleQuery);
            return res > 0;
        }
    }

    public async Task<List<TaskToDo>> LastTasks(int n)
    {
        using (var connection = CreateConnectionToDb())
        {
            connection.Open();
            var searchTasksQuery =
                string.Format(
                    @"SELECT Title, Description, Deadline, Tags 
                      FROM ToDoList 
                      ORDER BY Deadline LIMIT {0}"
                    , n);
            return (await connection.QueryAsync<SqlTaskToDo>(searchTasksQuery))
                .Select(stask => new TaskToDo(
                    stask.Title,
                    stask.Description,
                    stask.Deadline,
                    new List<string>(stask.Tags))
                ).ToList(); 
        }
    }

    public async Task Clear()
    {
        using (var connection = CreateConnectionToDb())
        {
            connection.Open();
            var clearQuery = "DELETE FROM ToDoList";
            await connection.ExecuteAsync(clearQuery);
        }
    }
}