namespace FluentIL
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the event builder interface.
    /// </summary>
    public interface IEventBuilder
    {
        IEventBuilder SpecialName();

        IEventBuilder RTSpecialName();

        EventBuilder Define();
    }
}