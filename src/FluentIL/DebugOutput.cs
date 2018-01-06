namespace FluentIL
{
    public static class DebugOutput
    {
        public static IDebugOutput Output { get; set; }


        public static void Write(string format, params object[] args)
        {
            Output?.Write(format, args);
        }

        public static void WriteLine(string format, params object[] args)
        {
            Output?.WriteLine(format, args);
        }
    }
}