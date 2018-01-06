namespace FluentIL
{
    using System;

    /// <summary>
    /// Various utility methods.
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Throws an exception if the passed argument is null.
        /// </summary>
        /// <param name="argument">The arguement.</param>
        /// <param name="argumentName">The argument name.</param>
        public static void ThrowIfArgumentNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Throws an exception if the passed argument is null or empty.
        /// </summary>
        /// <param name="argument">The arguement.</param>
        /// <param name="argumentName">The argument name.</param>
        public static void ThrowIfArgumentNullOrEmpty(string argument, string argumentName)
        {
            Utility.ThrowIfArgumentNull(argument, argumentName);

            if (argument == string.Empty)
            {
                throw new ArgumentException("Argument is empty", argumentName);
            }
        }

        /// <summary>
        /// Throws an exception if the passed argument is null, empty or contains whitespace.
        /// </summary>
        /// <param name="argument">The arguement.</param>
        /// <param name="argumentName">The argument name.</param>
        public static void ThrowIfArgumentNullEmptyOrWhitespace(string argument, string argumentName)
        {
            Utility.ThrowIfArgumentNullOrEmpty(argument, argumentName);

            if (argument.IndexOf(' ') != -1)
            {
                throw new ArgumentException("Argument contains whitespace", argumentName);
            }
        }
    }
}