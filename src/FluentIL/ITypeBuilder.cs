namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines an interface for builder types.
    /// </summary>
    public interface ITypeBuilder
    {
        /// <summary>
        /// Gets or sets the <see cref="ITypeBuilder"/> attributes.
        /// </summary>
        /// <returns></returns>
        TypeAttributes TypeAttributes { get; set; }

        /// <summary>
        /// Gets the implemented interfaces.
        /// </summary>
        IEnumerable<Type> Interfaces { get; }

        /// <summary>
        /// Sets the <see cref="ITypeBuilder"/> attributes.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        ITypeBuilder Attributes(TypeAttributes attributes);

        /// <summary>
        /// Sets the <see cref="ITypeBuilder"/> base type.
        /// </summary>
        /// <typeparam name="T">The base type</typeparam>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder InheritsFrom<T>();

        /// <summary>
        /// Sets the <see cref="ITypeBuilder"/> base type.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder InheritsFrom(Type baseType);

        /// <summary>
        /// Implements an interface on the <see cref="ITypeBuilder"/> instance.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder Implements<T>();

        /// <summary>
        /// Implements an interface on the <see cref="ITypeBuilder"/> instance.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder Implements(Type interfaceType);

        /// <summary>
        /// Defines a new constructor.
        /// </summary>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        IConstructorBuilder NewConstructor();

        /// <summary>
        /// Defines a new constructor.
        /// </summary>
        /// <param name="constructorBuilder">A <see cref="IConstructorBuilder"/>.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        ITypeBuilder NewConstructor(Action<IConstructorBuilder> constructorBuilder);

        /// <summary>
        /// Defines a default constructor.
        /// </summary>
        /// <param name="constructorAttributes">The constructors method attributes.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        ITypeBuilder NewDefaultConstructor(MethodAttributes constructorAttributes);

        /// <summary>
        /// Defines a new field.
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="fieldType">The fields type.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        IFieldBuilder NewField(string fieldName, Type fieldType);

        /// <summary>
        /// Defines a new field.
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="fieldType">The fields type.</param>
        /// <param name="fieldBuilder">A <see cref="IFieldBuilder"/>.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewField(string fieldName, Type fieldType, Action<IFieldBuilder> fieldBuilder);

        /// <summary>
        /// Defines a method.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="action">A <see cref="IMethodBuilder"/> action.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewMethod(string methodName, Action<IMethodBuilder> action);

        /// <summary>
        /// Defines a method.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="attributes">The methods attributes.</param>
        /// <param name="callingConvention">The method calling convention.</param>
        /// <param name="returnType">The methods return type.</param>
        /// <returns>A <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder NewMethod(
            string methodName,
            MethodAttributes attributes,
            CallingConventions callingConvention,
       	    Type returnType);

        /// <summary>
        /// Defines a method.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        IMethodBuilder NewMethod(string methodName);

        /// <summary>
        /// Defines a property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The properties type.</param>
        /// <returns></returns>
        IPropertyBuilder NewProperty(string propertyName, Type propertyType);

        /// <summary>
        /// Defines a property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyType">The properties type.</param>
        /// <param name="propertyBuilder">A <see cref="IPropertyBuilder"/> action.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewProperty(string propertyName, Type propertyType, Action<IPropertyBuilder> propertyBuilder);

        /// <summary>
        /// Defines an event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="eventType">The events type.</param>
        /// <returns>The <see cref="IEventBuilder"/> instance.</returns>
        IEventBuilder NewEvent(string eventName, Type eventType);

        /// <summary>
        /// Defines an event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="eventType">The events type.</param>
        /// <param name="eventBuilder">An <see cref="IEventBuilder"/> action</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewEvent(string eventName, Type eventType, Action<IEventBuilder> eventBuilder);

        /// <summary>
        /// Defines a generic parameter.
        /// </summary>
        /// <param name="parameterName">The name of the generic parameter.</param>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder NewGenericParameter(string parameterName);

        /// <summary>
        /// Adds a new generic type parameter to the type.
        /// </summary>
        /// <param name="parameterName">The name of the generic parameter.</param>
        /// <param name="parameterBuilder">A <see cref="IGenericParameterBuilder"/> action.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewGenericParameter(string parameterName, Action<IGenericParameterBuilder> parameterBuilder);

        /// <summary>
        /// Gets a generic parameter type.
        /// </summary>
        /// <param name="parameterName">The name of the generic parameter.</param>
        /// <returns>A <see cref="Type"/>.</returns>
        Type GetGenericParameterType(string parameterName);

        /// <summary>
        /// Defines a nest type.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewNestedType(string typeName);

        /// <summary>
        /// Sets a custom attribute.
        /// </summary>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder SetCustomAttribute(CustomAttributeBuilder customAttribute);

        /// <summary>
        /// Defines the <see cref="TypeBuilder"/>.
        /// </summary>
        /// <returns>A <see cref="TypeBuilder"/> instance.</returns>
        TypeBuilder Define();

        /// <summary>
        /// Creates the actual type.
        /// </summary>
        /// <returns>A <see cref="Type"/> instance.</returns>
        Type CreateType();
    }
}