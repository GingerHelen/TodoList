using Fclp;

namespace Types;

public enum AppType { Console, Rest, Gui };
public enum DbType { Json, Sql };

public class Config
{
    public AppType AppType { get; private set; }
    public string Path { get; private set; } = "todolist.json";
    public string DbName { get; private set; } = "todolist";
    public DbType DbType { get; private set; } = DbType.Json;

    public Config(string[] args)
    {
        var parser = new FluentCommandLineParser();
        parser.Setup<string>(CaseType.CaseSensitive, "dbname")
            .Callback(dbname => DbName = dbname)
            .SetDefault("todolist")
            .WithDescription("Database name");
        parser.Setup<string>(CaseType.CaseSensitive, "path")
            .Callback(path => Path = path)
            .SetDefault("todolist.json")
            .WithDescription("Path to json database");
        parser.Setup<bool>(CaseType.CaseSensitive, "console")
            .Callback(b => AppType = AppType.Console)
            .WithDescription("Sets application type to console");
        parser.Setup<bool>(CaseType.CaseSensitive, "gui")
            .Callback(b => AppType = AppType.Gui)
            .WithDescription("Sets application type to gui");
        parser.Setup<bool>(CaseType.CaseSensitive, "rest")
            .Callback(b => AppType = AppType.Rest)
            .WithDescription("Sets application type to rest");
        parser.Setup<bool>(CaseType.CaseSensitive, "json")
            .Callback(b => DbType = DbType.Json)
            .WithDescription("Application will use json database");
        parser.Setup<bool>(CaseType.CaseSensitive, "sql")
            .Callback(b => DbType = DbType.Sql)
            .WithDescription("Application will use sql database");
        parser.Parse(args);
    }
}   



