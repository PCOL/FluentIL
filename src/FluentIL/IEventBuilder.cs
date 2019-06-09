namespace FluentIL
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the event builder interface.
    /// </summary>
    public interface IEventBuilder
    {
        /// <summary>
        /// Gets the events attributes.
        /// </summary>
        EventAttributes EventAttributes { get; }

        /// <summary>
        /// Adds the 'SpecialName' attribute.
        /// </summary>
        /// <returns>The <see cref="IEventBuilder"/> instance.</returns>
        IEventBuilder SpecialName();

        /// <summary>
        /// Adds the 'RTSpecialName' attribute.
        /// </summary>
        /// <returns>The <see cref="IEventBuilder"/> instance.</returns>
        IEventBuilder RTSpecialName();

        /// <summary>
        /// Defines the <see cref="EventBuilder"/>.
        /// </summary>
        /// <returns>An <see cref="EventBuilder"/> instance.</returns>
        EventBuilder Define();
    }
}