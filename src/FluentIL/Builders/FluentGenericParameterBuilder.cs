namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IGenericParameterBuilder"/> interface.
    /// </summary>
    internal class FluentGenericParameterBuilder
        : IGenericParameterBuilder
    {
        private readonly Func<string, GenericTypeParameterBuilder> define;

        private Type baseType;

        private List<Type> interfaceTypes;

        private GenericTypeParameterBuilder builder;

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentGenericParameterBuilder"/> class.
        /// </summary>
        /// <param name="parameterName">The paramter name.</param>
        /// <param name="defineFunc">The builder action.</param>
        public FluentGenericParameterBuilder(
            string parameterName,
            Func<string, GenericTypeParameterBuilder> defineFunc)
        {
            this.ParameterName = parameterName;
            this.define = defineFunc;
        }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Gets the parameter builder.
        /// </summary>
        internal GenericTypeParameterBuilder ParameterBuilder { get; private set; }

        /// <inheritdoc />
        public GenericParameterAttributes Attributes { get; set; }

        /// <inheritdoc />
        public IGenericParameterBuilder BaseType<T>()
        {
            return this.BaseType(typeof(T));
        }

        /// <inheritdoc />
        public IGenericParameterBuilder BaseType(Type baseType)
        {
            this.baseType = baseType;
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder InterfaceType<T>()
        {
            return this.InterfaceType(typeof(T));
        }

        /// <inheritdoc />
        public IGenericParameterBuilder InterfaceType(Type interfaceType)
        {
            if (interfaceType.IsInterface == false)
            {
                throw new InvalidOperationException("Type must be an interface");
            }

            this.interfaceTypes = this.interfaceTypes ?? new List<Type>();
            this.interfaceTypes.Add(interfaceType);
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder Contravariant()
        {
            this.Attributes |= GenericParameterAttributes.Contravariant;
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder Covariant()
        {
            this.Attributes |= GenericParameterAttributes.Covariant;
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder DefaultConstructor()
        {
            this.Attributes |= GenericParameterAttributes.DefaultConstructorConstraint;
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder NotNullableValueType()
        {
            this.Attributes |= GenericParameterAttributes.NotNullableValueTypeConstraint;
            return this;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder ReferenceType()
        {
            this.Attributes |= GenericParameterAttributes.ReferenceTypeConstraint;
            return this;
        }

        public void Build(GenericTypeParameterBuilder builder)
        {
            this.builder = builder;

            if (this.baseType != null)
            {
                this.builder.SetBaseTypeConstraint(this.baseType);
            }

            if (this.interfaceTypes != null)
            {
                this.builder.SetInterfaceConstraints(this.interfaceTypes.ToArray());
            }

            this.builder.SetGenericParameterAttributes(this.Attributes);
        }

        public Type AsType()
        {
            return this.builder.AsType();
        }

        /// <inheritdoc />
        public GenericTypeParameterBuilder Define()
        {
            if (this.ParameterBuilder == null)
            {
                this.ParameterBuilder = this.define(this.ParameterName);
            }

            return this.ParameterBuilder;
        }
    }
}