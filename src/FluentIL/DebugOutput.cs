namespace FluentIL
{
    using System;

    /// <summary>
    /// Debug output helper.
    /// </summary>
    public static class DebugOutput
    {
        /// <summary>
        /// Gets or sets the <see cref="IDebugOutput"/> instance.
        /// </summary>
        /// <returns></returns>
        public static IDebugOutput Output { get; set; }

        /// <summary>
        /// Writes to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        internal static void Write(string format, params object[] args)
        {
            Output?.Write(format, args);
        }

        /// <summary>
        /// Writes a line to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        internal static void WriteLine(string format, params object[] args)
        {
            Output?.WriteLine(format, args);
        }

        /// <summary>
        /// Writes to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        internal static void WriteColor(ConsoleColor color, string format, params object[] args)
        {
            Output?.WriteColor(color, format, args);
        }

        /// <summary>
        /// Writes a line to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        internal static void WriteLineColor(ConsoleColor color, string format, params object[] args)
        {
            Output?.WriteLineColor(color, format, args);
        }
    }
}