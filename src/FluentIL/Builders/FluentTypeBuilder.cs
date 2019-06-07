namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="ITypeBuilder"/> interface.
    /// </summary>
    internal class FluentTypeBuilder
        : ITypeBuilder
    {
        private readonly ModuleBuilder moduleBuilder;

        private readonly string typeName;

        private readonly Func<string, TypeAttributes, Type, Type[], TypeBuilder> define;

        private Type baseType;

        private TypeBuilder typeBuilder;

        private List<Type> interfaces = new List<Type>();

        private List<Action> actions = new List<Action>();

        private List<FluentGenericParameterBuilder> genericParameters;

        private List<CustomAttributeBuilder> customAttributes;

        private TypeInfo typeInfo;

        private bool genericParemetersBuilt;

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentTypeBuilder"/> class.
        /// </summary>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="typeName">The name of the type.</param>
        internal FluentTypeBuilder(ModuleBuilder moduleBuilder, string typeName)
        {
            this.moduleBuilder = moduleBuilder;
            this.typeName = typeName;
            this.TypeAttributes = TypeAttributes.Class;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentTypeBuilder"/> class.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="define">A function to define the <see cref="TypeBuilder"/>.</param>
        internal FluentTypeBuilder(
            string typeName,
            Func<string, TypeAttributes, Type, Type[], TypeBuilder> define)
        {
            this.typeName = typeName;
            this.define = define;
        }

        /// <inheritdoc />
        public TypeAttributes TypeAttributes { get; set; }

        /// <inheritdoc />
        public IEnumerable<Type> Interfaces
        {
            get
            {
                return this.interfaces;
            }
        }

        /// <inheritdoc />
        public ITypeBuilder Attributes(TypeAttributes attributes)
        {
            this.ThrowIfAlreadyBuilt();
            this.TypeAttributes = attributes;
            return this;
        }

        /// <inheritdoc/>
        public ITypeBuilder InheritsFrom<T>()
        {
            return this.InheritsFrom(typeof(T));
        }

        /// <inheritdoc/>
        public ITypeBuilder InheritsFrom(Type baseType)
        {
            this.ThrowIfAlreadyBuilt();

            if (baseType.IsInterface == true)
            {
                throw new InvalidOperationException("Type cannot be an interface.");
            }

            if (baseType.IsSealed == true)
            {
                throw new InvalidOperationException("Type cannot inherit from a sealed type.");
            }

            this.baseType = baseType;
            return this;
        }

        /// <inheritdoc/>
        public ITypeBuilder Implements<T>()
        {
            return this.Implements(typeof(T));
        }

        /// <inheritdoc/>
        public ITypeBuilder Implements(Type interfaceType)
        {
            this.ThrowIfAlreadyBuilt();

            if (interfaceType.IsInterface == false)
            {
                throw new InvalidOperationException("Type must be an interface.");
            }

            this.interfaces.Add(interfaceType);
            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder NewConstructor()
        {
            var ctorBuilder = new FluentConstructorBuilder(
                (attrs,
                calling,
                parms,
                required,
                optional) =>
                {
                    return this
                        .Define()
                        .DefineConstructor(
                            attrs,
                            calling,
                            parms,
                            required,
                            optional);
                }
            );

            this.actions.Add(() => ctorBuilder.Define());
            return ctorBuilder;
        }

        /// <inheritdoc />
        public ITypeBuilder NewConstructor(Action<IConstructorBuilder> constructorBuilder)
        {
            constructorBuilder(this.NewConstructor());
            return this;
        }

        /// <inheritdoc />
        public ITypeBuilder NewDefaultConstructor(MethodAttributes constructorAttributes)
        {
            var ctorBuilder = new FluentConstructorBuilder(
                (attrs) =>
                {
                    return this
                        .Define()
                        .DefineDefaultConstructor(
                            attrs);
                }
            );

            ctorBuilder.MethodAttributes = constructorAttributes;
            this.actions.Add(() => ctorBuilder.Define());
            return this;
        }

        /// <inheritdoc />
        public IFieldBuilder NewField(string fieldName, Type fieldType)
        {
            var fieldBuilder = new FluentFieldBuilder(
                fieldName,
                fieldType,
                (name,
                type,
                requiredCustomModifiers,
                optionalCustomModifiers,
                fieldAttributes) =>
            {
                return this
                    .Define()
                    .DefineField(
                        name,
                        type,
                        requiredCustomModifiers,
                        optionalCustomModifiers,
                        fieldAttributes);
            });

            this.actions.Add(() => fieldBuilder.Define());
            return fieldBuilder;
        }

        /// <inheritdoc />
        public ITypeBuilder NewField(string fieldName, Type fieldType, Action<IFieldBuilder> fieldBuilder)
        {
            fieldBuilder(this.NewField(fieldName, fieldType));
            return this;
        }

        /// <inheritdoc />
        public ITypeBuilder NewMethod(string methodName, Action<IMethodBuilder> action)
        {
            var builder = new FluentMethodBuilder(methodName, this.DefineMethod)
                .CallingConvention(CallingConventions.HasThis);

            action(builder);
            this.actions.Add(() => builder.Define());
            return this;
        }

        /// <inheritdoc />
        public IMethodBuilder NewMethod(
            string methodName,
            MethodAttributes methodAttributes,
            CallingConventions callingConvention,
            Type returnType)
        {
            return this.NewMethod(methodName)
                .MethodAttributes(methodAttributes)
                .CallingConvention(CallingConventions.HasThis)
                .Returns(returnType);
        }

        /// <inheritdoc />
        public IMethodBuilder NewMethod(string methodName)
        {
            var builder =  new FluentMethodBuilder(methodName, this.DefineMethod);
            this.actions.Add(() => builder.Define());
            return builder;
        }

        /// <summary>
        /// Defines a property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The field builder instance.</returns>
        public IPropertyBuilder NewProperty(
            string propertyName,
            Type propertyType)
        {
            var builder = new FluentPropertyBuilder(
                this,
                propertyName,
                propertyType,
                (name,
                attr,
                calling,
                returnType,
                returnTypeRequired,
                returnTypeOptional,
                parameterTypes,
                parameterTypesRequired,
                parameterTypesOptional) =>
                {
                    return this
                        .Define()
                        .DefineProperty(
                            name,
                            attr,
                            calling,
                            returnType,
                            returnTypeRequired,
                            returnTypeOptional,
                            parameterTypes,
                            parameterTypesRequired,
                            parameterTypesOptional);
                });

            this.actions.Add(() => builder.Define());
            return builder;
        }

        /// <inheritdoc/>
        public ITypeBuilder NewProperty(string propertyName, Type propertyType, Action<IPropertyBuilder> propertyBuilder)
        {
            propertyBuilder(this.NewProperty(propertyName, propertyType));
            return this;
        }

        /// <inheritdoc/>
        public IEventBuilder NewEvent(string eventName, Type eventType)
        {
            var builder = new FluentEventBuilder(
                eventName,
                eventType,
                (name, attrs, type) =>
                {
                    return this
                        .Define()
                        .DefineEvent(
                            name,
                            attrs,
                            type);
                });

            this.actions.Add(() => builder.Define());
            return builder;
        }

        /// <inheritdoc />
        public ITypeBuilder NewEvent(string eventName, Type eventType, Action<IEventBuilder> eventBuilder)
        {
            eventBuilder(this.NewEvent(eventName, eventType));
            return this;
        }

        /// <inheritdoc/>
        public ITypeBuilder NewNestedType(string typeName)
        {
            var builder = new FluentTypeBuilder(
                typeName,
                (name, attrs, parent, interfaces) =>
                {
                    return this
                        .Define()
                        .DefineNestedType(
                            name,
                            attrs,
                            parent,
                            interfaces);
                });

            this.actions.Add(() => builder.Define());
            return builder;
        }

        /// <inheritdoc />
        public IGenericParameterBuilder NewGenericParameter(string parameterName)
        {
            this.genericParameters = this.genericParameters ?? new List<FluentGenericParameterBuilder>();
            var builder = new FluentGenericParameterBuilder(parameterName, null);
            this.genericParameters.Add(builder);
            return builder;
        }

        /// <inheritdoc />
        public ITypeBuilder NewGenericParameter(string parameterName, Action<IGenericParameterBuilder> action)
        {
            var builder = this.NewGenericParameter(parameterName);
            action?.Invoke(builder);
            return this;
        }

        public Type GetGenericParameterType(string parameterName)
        {
            if (this.genericParameters.Any() == true)
            {
                this.BuildGenericParameters();

                return this.genericParameters
                    .FirstOrDefault(p => p.ParameterName == parameterName)
                    ?.AsType();
            }

            return null;
        }

        /// <inheritdoc />
        public ITypeBuilder SetCustomAttribute(CustomAttributeBuilder customAttribute)
        {
            this.customAttributes = this.customAttributes ?? new List<CustomAttributeBuilder>();
            this.customAttributes.Add(customAttribute);
            return this;
        }

        /// <summary>
        /// Builds the type.
        /// </summary>
        /// <returns>The type.</returns>
        public TypeBuilder Define()
        {
            if (this.typeBuilder == null)
            {
                DebugOutput.WriteLine("=======================================");
                DebugOutput.WriteLine("Type '{0}' defined", this.typeName);
                DebugOutput.WriteLine("Type Attributes: {0}", this.TypeAttributes);
                DebugOutput.WriteLine("Base Type: {0}", this.baseType);
                DebugOutput.WriteLine("Implements: {0}",string.Join(", ", this.interfaces.Select(i => i.Name)));

                this.typeBuilder = this.moduleBuilder.DefineType(
                    this.typeName,
                    this.TypeAttributes,
                    this.baseType,
                    this.interfaces.ToArray());

                this.BuildGenericParameters();

                this.customAttributes.SetCustomAttributes(a => this.typeBuilder.SetCustomAttribute(a));
            }

            return this.typeBuilder;
        }

        /// <summary>
        /// Creates the type.
        /// </summary>
        /// <returns>The created type.</returns>
        public Type CreateType()
        {
            this.Define();

            if (this.typeBuilder.IsCreated() == false)
            {
                foreach (var action in this.actions)
                {
                    action();
                }

                this.typeInfo =  this.typeBuilder.CreateTypeInfo();
            }

            return this.typeInfo.AsType();
        }

        /// <summary>
        /// Throws an error if the type has been built.
        /// </summary>
        private void ThrowIfAlreadyBuilt()
        {
            if (this.typeBuilder != null)
            {
                throw new InvalidOperationException("Type has been built");
            }
        }

        /// <summary>
        /// Builds any generic parameters.
        /// </summary>
        private void BuildGenericParameters()
        {
            this.Define();

            if (this.genericParameters != null &&
                this.genericParemetersBuilt == false)
            {
                this.genericParemetersBuilt = true;

                var genericParms = this.typeBuilder.DefineGenericParameters(
                    this.genericParameters.Select(g => g.ParameterName).ToArray());

                for (int i = 0; i < genericParameters.Count; i++)
                {
                    this.genericParameters[i].Build(genericParms[i]);
                }
            }
        }

        /// <summary>
        /// Defines a method.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="attributes">The methods attributes.</param>
        /// <param name="convention">The methods calling convention.</param>
        /// <param name="returnType">The methods return type.</param>
        /// <param name="returnTypeRequiredCustomModifiers">The return types required custom modifiers.</param>
        /// <param name="returnTypeOptionalCustomModifiers">The return types optional custom modifiers.</param>
        /// <param name="parameterTypes">The methods parameter types.</param>
        /// <param name="parameterTypeRequiredCustomModifiers">The parameter types required custom modifiers.</param>
        /// <param name="parameterTypeOptionalCustomModifiers">The parameter types optional custom modifiers.</param>
        /// <returns>A <see cref="MethodBuilder"/> instance.</returns>
        private MethodBuilder DefineMethod(
            string name,
            MethodAttributes attributes,
            CallingConventions convention,
            Type returnType,
            Type[] returnTypeRequiredCustomModifiers,
            Type[] returnTypeOptionalCustomModifiers,
            Type[] parameterTypes,
            Type[][] parameterTypeRequiredCustomModifiers,
            Type[][] parameterTypeOptionalCustomModifiers)
        {
            return this.Define().DefineMethod(
                name,
                attributes,
                convention,
                returnType,
                returnTypeRequiredCustomModifiers,
                returnTypeOptionalCustomModifiers,
                parameterTypes,
                parameterTypeRequiredCustomModifiers,
                parameterTypeOptionalCustomModifiers);
        }
    }
}