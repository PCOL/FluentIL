namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the generic parameter builder interface.
    /// </summary>
    public interface IGenericParameterBuilder
    {
        /// <summary>
        /// Gets or sets the generic parameters attributes.
        /// </summary>
        GenericParameterAttributes Attributes { get; set; }

        /// <summary>
        /// Gets the generic parameters name.
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Sets the base type that a type must inherit in order to be substituted for the type parameter.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder BaseType<T>();

        /// <summary>
        /// Sets the base type that a type must inherit in order to be substituted for the type parameter.
        /// </summary>
        /// <param name="baseType">The base type.</param>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder BaseType(Type baseType);

        /// <summary>
        /// Sets the interfaces a type must implement in order to be substituted for the type parameter.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder InterfaceType<T>();

        /// <summary>
        /// Sets the interfaces a type must implement in order to be substituted for the type parameter.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder InterfaceType(Type interfaceType);

        /// <summary>
        /// The generic type parameter is covariant. A covariant type parameter can appear as the result
        /// type of a method, the type of a read-only field, a declared base type, or an implemented interface.
        /// </summary>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder Covariant();

        /// <summary>
        /// The generic type parameter is contravariant. A contravariant type parameter can appear as a parameter
        /// type in method signatures.
        /// </summary>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder Contravariant();

        /// <summary>
        /// A type can be substituted for the generic type parameter only if it has a parameterless constructor.
        /// </summary>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder DefaultConstructor();

        /// <summary>
        /// A type can be substituted for the generic type parameter only if it is a value type and is not nullable.
        /// </summary>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder NotNullableValueType();

        /// <summary>
        /// A type can be substituted for the generic type parameter only if it is a reference type.
        /// </summary>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder ReferenceType();

        /// <summary>
        /// Defines the generic parameter.
        /// </summary>
        /// <returns>The <see cref="GenericTypeParameterBuilder"/> instance.</returns>
        Type AsType();
    }
}