namespace FluentIL
{
    using System;

    /// <summary>
    /// Defines the debug output interface.
    /// </summary>
    public interface IDebugOutput
    {
        /// <summary>
        /// Writes to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Writes a line to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        void WriteLine(string format, params object[] args);

        /// <summary>
        /// Writes to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        void WriteColor(ConsoleColor color, string format, params object[] args);

        /// <summary>
        /// Writes a line to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        void WriteLineColor(ConsoleColor color, string format, params object[] args);
    }
}