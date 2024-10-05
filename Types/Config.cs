namespace Types
{
    public enum AppType { Console, Rest, Gui };

    class Config
    {
        public AppType AppType { get; private set; }

        public Config(String[] args)
        {
            AppType = AppType.Console;
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "--console")
                {
                    AppType = AppType.Console;
                }
                if (args[i] == "--rest")
                {
                    AppType = AppType.Rest;
                }
                if (args[i] == "--gui")
                {
                    AppType = AppType.Gui;
                }
            }
        }
    }   
}


