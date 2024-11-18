namespace TodoList;

using Npgsql;
using Types;

public class ToDoListPostgres : IToDoList
{
    private string psqlURI = "Host=localhost;Port=5432;Username=postgres;Password=postgres";
    

    public ToDoListPostgres(string dbname)
    {
        psqlURI += $";Database={dbname}";
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open();
            CreateToDoList(psqlConnection);
        }
    }
    
    private static async Task<List<TaskToDo>> ReadTasks(NpgsqlDataReader reader)
    {
        var list = new List<TaskToDo>();
        while (await reader.ReadAsync())
        {
            list.Add(new TaskToDo(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetDateTime(2),
                new List<string>(reader.GetFieldValue<string[]>(3))
            ));
        }

        return list;
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

    private void CreateToDoList(NpgsqlConnection psqlConnection)
    {
        string createToDoListQuery = @"
            CREATE TABLE IF NOT EXISTS ToDoList (
                Title TEXT PRIMARY KEY,
                Description TEXT NOT NULL,
                Deadline DATE NOT NULL,
                Tags TEXT[]
            );
        ";
        using (var createCommand = new NpgsqlCommand(createToDoListQuery, psqlConnection))
        {
            createCommand.ExecuteNonQuery();
        }
    }

    public async Task<bool> AddTask(TaskToDo taskToDo)
    {
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open(); 
            var addTaskQuery =
                string.Format("INSERT INTO ToDoList VALUES ('{0}', '{1}', '{2}', '{3}')"
                    , taskToDo.Title
                    , taskToDo.Description
                    , taskToDo.Deadline
                    , TagsToString(taskToDo.Tags));
            var res = await new NpgsqlCommand(addTaskQuery, psqlConnection).ExecuteNonQueryAsync();
            return res > 0;
        }
    }

    public async Task<List<TaskToDo>> SearchTasksByTags(List<string> tags)
    {
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open();
            var searchTasksByTagsQuery =
                string.Format("SELECT * FROM ToDoList WHERE Tags && '{0}'"
                    , TagsToString(tags));
            var reader = await new NpgsqlCommand(searchTasksByTagsQuery, psqlConnection).ExecuteReaderAsync();
            return ReadTasks(reader).Result;
        }
    }

    public async Task<bool> RemoveByTitle(string title)
    {
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open();
            var removeByTitleQuery =
                string.Format("DELETE FROM ToDoList WHERE Title = '{0}'", title);
            var res = await new NpgsqlCommand(removeByTitleQuery, psqlConnection).ExecuteNonQueryAsync();
            return res > 0;
        }
    }

    public async Task<List<TaskToDo>> LastTasks(int n)
    {
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open();
            var searchTasksByTagsQuery =
                string.Format("SELECT * FROM ToDoList ORDER BY Deadline LIMIT {0}", n);
            var reader = await new NpgsqlCommand(searchTasksByTagsQuery, psqlConnection).ExecuteReaderAsync();
            return ReadTasks(reader).Result;
        }
    }

    public async Task Clear()
    {
        using (var psqlConnection = new NpgsqlConnection(psqlURI))
        {
            psqlConnection.Open();
            var clearQuery = "DELETE FROM ToDoList";
            await new NpgsqlCommand(clearQuery, psqlConnection).ExecuteNonQueryAsync();
        }
    }
}