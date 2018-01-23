namespace FluentIL.Emitters
{
    using System;

    /// <summary>
    /// A local variable adapter.
    /// </summary>
    internal class LocalAdapter
        : ILocal,
        IAdaptedLocal
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LocalAdapter"/> class.
        /// </summary>
        /// <param name="name">The name of the local.</param>
        /// <param name="localType">The locals type.</param>
        /// <param name="localIndex">The locals index.</param>
        /// <param name="isPinned">A value indicating wheteher or not the local is pinned.</param>
        /// <param name="local">The local instance.</param>
        internal LocalAdapter(string name, Type localType, int localIndex, bool isPinned, object local = null)
        {
            this.Name = name;
            this.LocalType = localType;
            this.LocalIndex = localIndex;
            this.IsPinned = isPinned;
            this.Local = local;
        }

        /// <inheritdoc/>
        public object Local { get; set; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public bool IsPinned { get; }

        /// <inheritdoc/>
        public int LocalIndex { get; }

        /// <inheritdoc/>
        public Type LocalType { get; }
    }
}