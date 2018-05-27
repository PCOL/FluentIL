namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyBuilder"/> interface.
    /// </summary>
    internal class FluentPropertyBuilder
        : IPropertyBuilder
    {
        /// <summary>
        /// A function to define a property.
        /// </summary>
        private readonly Func<string, PropertyAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], PropertyBuilder> define;

        /// <summary>
        ///  A type builder.
        /// </summary>
        private readonly ITypeBuilder typeBuilder;

        /// <summary>
        /// The property name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The property type.
        /// </summary>
        private Type propertyType;

        /// <summary>
        /// The calling convention.
        /// </summary>
        private CallingConventions callingConvention = CallingConventions.HasThis;

        /// <summary>
        /// The defined property builder. 
        /// </summary>
        private PropertyBuilder propertyBuilder;

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentPropertyBuilder"/> class.
        /// </summary>
        /// <param name="typeBuilder">A <see cref="ITypeBuilder"/> instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The property type.</param>
        /// <param name="define">A function to define the property.</param>
        public FluentPropertyBuilder(
            ITypeBuilder typeBuilder,
            string propertyName,
            Type propertyType,
            Func<string, PropertyAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], PropertyBuilder> define)
        {
            this.define = define;
            this.typeBuilder = typeBuilder;
            this.name = propertyName;
            this.propertyType = propertyType;
            this.PropertyAttributes = PropertyAttributes.None;
        }

        /// <inheritdoc />
        public PropertyAttributes PropertyAttributes { get; set; }

        /// <inheritdoc />
        public IMethodBuilder SetMethod { get; set; }

        /// <inheritdoc />
        public IMethodBuilder GetMethod { get; set; }

        /// <inheritdoc />
        public IPropertyBuilder Getter(Action<IMethodBuilder> action)
        {
            action.Invoke(this.Getter());
            return this;
        }

        /// <inheritdoc />
        public IMethodBuilder Getter()
        {
            if (this.GetMethod == null)
            {
                this.GetMethod = this.typeBuilder
                    .NewMethod($"get_{this.name}")
                    .CallingConvention(this.callingConvention)
                    .SpecialName()
                    .Returns(this.propertyType);
            }

            return this.GetMethod;
        }

        /// <inheritdoc />
        public IPropertyBuilder Setter(Action<IMethodBuilder> action)
        {
            action.Invoke(this.Setter());
            return this;
        }

        /// <inheritdoc />
        public IMethodBuilder Setter()
        {
            if (this.SetMethod == null)
            {
                this.SetMethod = this.typeBuilder
                    .NewMethod($"set_{this.name}")
                    .CallingConvention(this.callingConvention)
                    .Param(this.propertyType, "value", ParameterAttributes.None)
                    .Returns(typeof(void));
            }

            return this.SetMethod;
        }

        /// <inheritdoc />
        public IPropertyBuilder Attributes(PropertyAttributes attributes)
        {
            this.PropertyAttributes = attributes;
            return this;
        }

        /// <inheritdoc />
        public IPropertyBuilder CallingConvention(CallingConventions callingConvention)
        {
            this.callingConvention = callingConvention;
            return this;
        }

        /// <inheritdoc />
        public PropertyBuilder Define()
        {
            if (this.propertyBuilder == null)
            {
                this.propertyBuilder = this.define(
                    this.name,
                    this.PropertyAttributes,
                    this.callingConvention,
                    this.propertyType,
                    null,
                    null,
                    null,
                    null,
                    null);

                if (this.GetMethod != null)
                {
                    this.propertyBuilder.SetGetMethod(this.GetMethod.Define());
                }

                if (this.SetMethod != null)
                {
                    this.propertyBuilder.SetSetMethod(this.SetMethod.Define());
                }

                DebugOutput.WriteLine("");
                DebugOutput.WriteLine("=======================================");
                DebugOutput.WriteLine("Property {0} Defined", this.name);
                DebugOutput.WriteLine("Calling Convention: {0}", this.callingConvention);
                DebugOutput.WriteLine("Attributes: {0}", this.propertyBuilder.Attributes);
                DebugOutput.WriteLine("");
            }

            return this.propertyBuilder;
        }
    }
}