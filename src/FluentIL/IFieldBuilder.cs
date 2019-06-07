namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the field builder interface.
    /// </summary>
    public interface IFieldBuilder
    {
        /// <summary>
        /// Gets the fields name.
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Gets the fields type.
        /// </summary>
        Type FieldType { get; }

        /// <summary>
        /// Gets or sets the field attributes.
        /// </summary>
        FieldAttributes FieldAttributes { get; set; }

        /// <summary>
        /// Sets the fields attributes.
        /// </summary>
        /// <param name="attributes">The field attributes.</param>
        /// <returns>A <see cref="FieldBuilder"/>.</returns>
        IFieldBuilder Attributes(FieldAttributes attributes);

        /// <summary>
        /// Defines the field builder.
        /// </summary>
        /// <returns>A <see cref="FieldBuilder"/>.</returns>
        FieldBuilder Define();
    }
}