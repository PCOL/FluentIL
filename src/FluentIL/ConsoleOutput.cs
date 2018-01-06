using System;

namespace FluentIL
{
    public class ConsoleOutput
        : IDebugOutput
    {
        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteColor(ConsoleColor color, string format, params object[] args)
        {
            var oldColor = this.SetConsoleColor(color);
            Console.Write(format, args);
            this.SetConsoleColor(oldColor);
        }

        public void WriteLineColor(ConsoleColor color, string format, params object[] args)
        {
            var oldColor = this.SetConsoleColor(color);
            Console.WriteLine(format, args);
            this.SetConsoleColor(oldColor);
        }

        private ConsoleColor SetConsoleColor(ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            return oldColor;
        }        
    }
}