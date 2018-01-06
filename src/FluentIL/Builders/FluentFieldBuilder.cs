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
        /// The field attributes.
        /// </summary>
        private FieldAttributes fieldAttributes = FieldAttributes.Private;

        /// <summary>
        /// The field builder.
        /// </summary>
        private FieldBuilder fieldBuilder;

        /// <summary>
        /// Initialises a new instance of <see cref="FieldBuilderImpl"/> class.
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

        /// <summary>
        /// Gets the fields name.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Gets the fields type.
        /// </summary>
        public Type FieldType { get; }

        /// <summary>
        /// Gets or sets the fields attributes.
        /// </summary>
        public FieldAttributes FieldAttributes { get; set; }

        /// <summary>
        /// Sets the attributes
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns>A <see cref="FieldBuilder"/> instance.</returns>
        public IFieldBuilder Attributes(FieldAttributes attributes)
        {
            this.FieldAttributes = attributes;
            return this;
        }

        /// <summary>
        /// Defines the field.
        /// </summary>
        /// <returns>A <see cref="FieldBuilder"/> instance.</returns>
        public FieldBuilder Define()
        {
            if (this.fieldBuilder == null)
            {
                this.fieldBuilder = this.defineFunc(
                    this.FieldName,
                    this.FieldType,
                    null,
                    null,
                    this.fieldAttributes);

                DebugOutput.WriteLine("=======================================");
                DebugOutput.WriteLine("New Field '{0}' [{1}]", this.FieldName, this.FieldType);
                DebugOutput.WriteLine("Field Attributes: {0}", this.fieldAttributes);
                DebugOutput.WriteLine("");
            }

            return this.fieldBuilder;
        }
    }
}