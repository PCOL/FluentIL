namespace FluentIL
{
    using System;

    public interface IDebugOutput
    {
        void Write(string format, params object[] args);

        void WriteLine(string format, params object[] args);

        void WriteColor(ConsoleColor color, string format, params object[] args);

        void WriteLineColor(ConsoleColor color, string format, params object[] args);        
    }
}