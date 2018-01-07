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
        /// 
        /// </summary>
        /// <returns></returns>
        IConstructorBuilder NewConstructor();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        IFieldBuilder NewField(string fieldName, Type fieldType);

        /// <summary>
        /// Builds a method.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="action"></param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewMethod(string methodName, Action<IMethodBuilder> action);

        /// <summary>
        /// Builds a method.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="attributes"></param>
        /// <param name="callingConvention"></param>
        /// <param name="returnType"></param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        IMethodBuilder NewMethod(
            string methodName,
            MethodAttributes attributes,
            CallingConventions callingConvention,
       	    Type returnType);

        /// <summary>
        /// Builds a method.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        IMethodBuilder NewMethod(string methodName);

        /// <summary>
        /// Adds a property to the type.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        IPropertyBuilder NewProperty(
            string propertyName,
            Type propertyType);

        /// <summary>
        /// Adds an event to the type.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventType"></param>
        /// <returns>The <see cref="IEventBuilder"/> instance.</returns>
        IEventBuilder NewEvent(string eventName, Type eventType);

        /// <summary>
        /// Adds a new generic type parameter to the type.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder NewGenericParameter(string parameterName);

        /// <summary>
        /// Adds a new generic type parameter to the type.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterBuilder"></param>
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
        /// <param name="typeName"><The type name/param>
        /// <returns>The <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewNestedType(string typeName);

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