namespace FluentIL
{
    using System;

    public interface ILocal
    {
        string Name { get; }

        bool IsPinned { get; }

        int LocalIndex { get; }

        Type LocalType { get; }

        T Value<T>();
    }
}