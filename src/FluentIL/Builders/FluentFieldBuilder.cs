namespace FluentIL.Builders
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IFieldBuilder"/> interface.
    /// </summary>
    internal class FluentFieldBuilder
        : IFieldBuilder
    {
        /// <summary>
        /// The function used to define the field.
        /// </summary>
        private readonly Func<string, Type, Type[], Type[], FieldAttributes, FieldBuilder> defineFunc;

        /// <summary>
        /// The field builder.
        /// </summary>
        private FieldBuilder fieldBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentFieldBuilder"/> class.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <param name="fieldType">The field type.</param>
        /// <param name="defineFunc">The field define function.</param>
        public FluentFieldBuilder(
            string fieldName,
            Type fieldType,
            Func<string, Type, Type[], Type[], FieldAttributes, FieldBuilder> defineFunc)
        {
            this.FieldName = fieldName;
            this.FieldType = fieldType;
            this.defineFunc = defineFunc;
            this.FieldAttributes = FieldAttributes.Private;
        }

        /// <inheritdoc />
        public string FieldName { get; }

        /// <inheritdoc />
        public Type FieldType { get; }

        /// <inheritdoc />
        public FieldAttributes FieldAttributes { get; set; }

        /// <inheritdoc />
        public IFieldBuilder Attributes(FieldAttributes attributes)
        {
            this.FieldAttributes = attributes;
            return this;
        }

        /// <inheritdoc />
        public FieldBuilder Define()
        {
            if (this.fieldBuilder == null)
            {
                this.fieldBuilder = this.defineFunc(
                    this.FieldName,
                    this.FieldType,
                    null,
                    null,
                    this.FieldAttributes);

                DebugOutput.WriteLine("=======================================");
                DebugOutput.WriteLine("New Field '{0}' [{1}]", this.FieldName, this.FieldType);
                DebugOutput.WriteLine("Field Attributes: {0}", this.FieldAttributes);
                DebugOutput.WriteLine(string.Empty);
            }

            return this.fieldBuilder;
        }
    }
}