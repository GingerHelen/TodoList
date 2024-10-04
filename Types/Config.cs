public enum AppType { Console, Rest, Gui };

class Config
{
    public AppType appType { get; private set; }

    public Config(String[] args)
    {
        appType = AppType.Console;
        for (int i = 0; i < args.Length; i++) {
            if (args[i] == "--console")
            {
                appType = AppType.Console;
            }
            if (args[i] == "--rest")
            {
                appType = AppType.Rest;
            }
            if (args[i] == "--gui")
            {
                appType = AppType.Gui;
            }
        }
    }
}

