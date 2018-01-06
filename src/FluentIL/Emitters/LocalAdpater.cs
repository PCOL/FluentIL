namespace FluentIL.Emitters
{
    using System;

    /// <summary>
    /// A local variable adapter.
    /// </summary>
    public class LocalAdapter
        : ILocal,
        IAdaptedLocal
    {
        internal LocalAdapter(string name, Type localType, int localIndex, bool isPinned, object local = null)
        {
            this.Name = name;
            this.LocalType = localType;
            this.LocalIndex = localIndex;
            this.IsPinned = isPinned;
            this.Local = local;
        }

        public object Local { get; set; }

        public string Name { get; }

        public bool IsPinned { get; }

        public int LocalIndex { get; }

        public Type LocalType { get; }

        public T Value<T>()
        {
            return default(T);
        }
    }
}