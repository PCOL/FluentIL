namespace FluentIL.Emitters
{
    using System;

    /// <summary>
    /// A label adapter.
    /// </summary>
    internal class LabelAdapter
        : ILabel,
        IAdaptedLabel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LabelAdapter"/> class.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <param name="label">The label.</param>
        internal LabelAdapter(string name, object label = null)
        {
            this.Name = name;
            this.Label = label;
        }

        /// <inheritdoc/>
        public object Label { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }
    }
}