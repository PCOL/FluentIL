namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

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

        IFieldBuilder Attributes(FieldAttributes attributes);

        FieldBuilder Define();
    }
}