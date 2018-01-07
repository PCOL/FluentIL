namespace FluentIL.Builders
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IEventBuilder"/> interface.
    /// </summary>
    internal class FluentEventBuilder
        : IEventBuilder
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        private readonly string eventName;

        /// <summary>
        /// The events type.
        /// </summary>
        private readonly Type eventType;

        /// <summary>
        /// A function to define the event.
        /// </summary>
        private readonly Func<string, EventAttributes, Type, EventBuilder> define;

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentEventBuilder"/> class.
        /// </summary>
        /// <param name="eventName">The events name.</param>
        /// <param name="eventType">The events type.</param>
        /// <param name="define">A function to define the event.</param>
        public FluentEventBuilder(
            string eventName,
            Type eventType,
            Func<string, EventAttributes, Type, EventBuilder> define)
        {
            this.eventName = eventName;
            this.eventType = eventType;
            this.EventAttributes = EventAttributes.None;
            this.define = define;
        }

        /// <inheritdoc />
        public EventAttributes EventAttributes { get; private set; }

        /// <inheritdoc />
        public IEventBuilder SpecialName()
        {
            this.EventAttributes |= EventAttributes.SpecialName;
            return this;
        }

        /// <inheritdoc />
        public IEventBuilder RTSpecialName()
        {
            this.EventAttributes |= EventAttributes.RTSpecialName;
            return this;
        }

        /// <inheritdoc />
        public EventBuilder Define()
        {
            return this.define(
                this.eventName,
                this.EventAttributes,
                this.eventType);
        }
    }
}