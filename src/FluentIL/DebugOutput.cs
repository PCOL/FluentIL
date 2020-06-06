namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Debug output helper.
    /// </summary>
    public static class DebugOutput
    {
        /// <summary>
        /// Gets or sets the <see cref="IDebugOutput"/> instance.
        /// </summary>
        /// <returns>The <see cref="IDebugOutput"/> instsnce.</returns>
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

        /// <summary>
        /// Outputs method attributes as a string.
        /// </summary>
        /// <param name="methodAttributes">The method attributes.</param>
        /// <returns>A string representation.</returns>
        internal static string OutputMethodAttributes(this MethodAttributes methodAttributes)
        {
            var str = new StringBuilder();
            if ((methodAttributes & System.Reflection.MethodAttributes.Private) != 0)
            {
                str.Append("private ");
            }
            else if ((methodAttributes & System.Reflection.MethodAttributes.Public) != 0)
            {
                str.Append("public ");
            }
            else if ((methodAttributes & System.Reflection.MethodAttributes.Family) != 0)
            {
                str.Append("protected ");
            }
            else if ((methodAttributes & System.Reflection.MethodAttributes.Assembly) != 0)
            {
                str.Append("internal ");
            }
            else if ((methodAttributes & System.Reflection.MethodAttributes.FamANDAssem) != 0)
            {
                str.Append("protected internal");
            }

            if ((methodAttributes & System.Reflection.MethodAttributes.Static) != 0)
            {
                str.Append("static ");
            }

            if ((methodAttributes & System.Reflection.MethodAttributes.Virtual) != 0)
            {
                str.Append("virtual ");
            }

            if ((methodAttributes & System.Reflection.MethodAttributes.Final) != 0)
            {
                str.Append("sealed ");
            }

            if ((methodAttributes & System.Reflection.MethodAttributes.Abstract) != 0)
            {
                str.Append("abstract ");
            }

            return str.ToString();
        }
    }
}