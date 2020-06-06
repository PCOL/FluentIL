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
        private readonly IGenericParameterBuilder genericParameter;

        private readonly IGenericParameterBuilder[] genericTypeArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAdapter"/> class.
        /// </summary>
        /// <param name="name">The name of the local.</param>
        internal LocalAdapter(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAdapter"/> class.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAdapter"/> class.
        /// </summary>
        /// <param name="name">The name of the local.</param>
        /// <param name="genericParameter">A generic parameter.</param>
        /// <param name="local">The local instance.</param>
        internal LocalAdapter(string name, IGenericParameterBuilder genericParameter, object local = null)
        {
            this.Name = name;
            this.genericParameter = genericParameter;
            this.Local = local;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAdapter"/> class.
        /// </summary>
        /// <param name="name">The name of the local.</param>
        /// <param name="localTypeDefinition">The local type definition.</param>
        /// <param name="genericTypeArgs">An array of generic type arguments.</param>
        /// <param name="local">The local instance.</param>
        internal LocalAdapter(string name, Type localTypeDefinition, IGenericParameterBuilder[] genericTypeArgs, object local = null)
        {
            this.Name = name;
            this.LocalType = localTypeDefinition;
            this.genericTypeArgs = genericTypeArgs;
            this.Local = local;
        }

        /// <inheritdoc/>
        public object Local { get; set; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public bool IsPinned { get; }

        /// <inheritdoc/>
        public int LocalIndex { get; set; }

        /// <inheritdoc/>
        public Type LocalType { get; set; }
    }
}