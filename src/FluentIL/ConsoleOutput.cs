namespace FluentIL
{
    using System;

    /// <summary>
    /// Console implementation of the <see cref="IDebugOutput"/> interface.
    /// </summary>
    public class ConsoleOutput
        : IDebugOutput
    {
        /// <inheritdoc/>
        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        /// <inheritdoc/>
        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        /// <inheritdoc/>
        public void WriteColor(ConsoleColor color, string format, params object[] args)
        {
            var oldColor = this.SetConsoleColor(color);
            Console.Write(format, args);
            this.SetConsoleColor(oldColor);
        }

        /// <inheritdoc/>
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