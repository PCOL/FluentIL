namespace FluentIL
{
    using System;

    /// <summary>
    /// Defines the local variable interface.
    /// </summary>
    public interface ILocal
    {
        /// <summary>
        /// Gets the name of the local.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether ot not the local is pinned.
        /// </summary>
        bool IsPinned { get; }

        /// <summary>
        /// Gets the locals index.
        /// </summary>
        int LocalIndex { get; }

        /// <summary>
        /// Gets the locals type.
        /// </summary>
        Type LocalType { get; }
    }
}