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
        /// A list of parameter types.
        /// </summary>
        private List<Type> parameterTypes;

        /// <summary>
        /// The get method builder.
        /// </summary>
        private IMethodBuilder getMethod;

        /// <summary>
        /// The set method builder.
        /// </summary>
        private IMethodBuilder setMethod;

        private PropertyBuilder propertyBuilder;

        /// <summary>
        /// Initialises a new instance of the <see cref="PropertyBuilderImpl"/> class.
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

        /// <summary>
        /// Gets or sets the property attributes.
        /// </summary>
        public PropertyAttributes PropertyAttributes { get; set; }

        /// <summary>
        /// Gets the get property method builder
        /// </summary>
        public IPropertyBuilder Getter(Action<IMethodBuilder> action)
        {
            action.Invoke(this.Getter());
            return this;
        }

        /// <summary>
        /// Gets the get property method builder
        /// </summary>
        public IMethodBuilder Getter()
        {
            if (this.getMethod == null)
            {
                this.getMethod = this.typeBuilder
                    .NewMethod($"get_{this.name}")
                    .CallingConvention(this.callingConvention)
                    .Returns(this.propertyType);
            }

            return this.getMethod;
        }

        /// <summary>
        /// Gets or adds the set property method builder.
        /// </summary>
        /// <returns>A <see cref="IMethodBuilder"/> which represents the set method.</returns>
        public IPropertyBuilder Setter(Action<IMethodBuilder> action)
        {
            action.Invoke(this.Setter());
            return this;
        }

        /// <summary>
        /// Gets or adds the set property method builder.
        /// </summary>
        /// <returns>A <see cref="IMethodBuilder"/> which represents the set method.</returns>
        public IMethodBuilder Setter()
        {
            if (this.setMethod == null)
            {
                this.setMethod = this.typeBuilder
                    .NewMethod($"set_{this.name}")
                    .CallingConvention(this.callingConvention)
                    .Param(this.propertyType, "value", ParameterAttributes.None)
                    .Returns(typeof(void));
            }

            return this.setMethod;
        }

        /// <summary>
        /// Sets the attributes of the property.
        /// </summary>
        /// <param name="attributes">The attibutes to set.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder Attributes(PropertyAttributes attributes)
        {
            this.PropertyAttributes = attributes;
            return this;
        }

        /// <summary>
        /// Sets the calling convention.
        /// </summary>
        /// <param name="callingConvention">The calling convention.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder CallingConvention(CallingConventions callingConvention)
        {
            this.callingConvention = callingConvention;
            return this;
        }

        /// <summary>
        /// Builds the property.
        /// </summary>
        /// <returns>A <see cref="PropertyInfo"/> instance.</returns>
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
                    this.parameterTypes != null ? this.parameterTypes.ToArray() : System.Type.EmptyTypes,
                    null,
                    null);

                if (this.getMethod != null)
                {
                    this.propertyBuilder.SetGetMethod(this.getMethod.Define());
                }

                if (this.setMethod != null)
                {
                    this.propertyBuilder.SetSetMethod(this.setMethod.Define());
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