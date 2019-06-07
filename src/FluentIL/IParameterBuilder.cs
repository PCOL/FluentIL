namespace FluentIL
{
    using System;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the parameter builder interface.
    /// </summary>
    public interface IParameterBuilder
    {
        /// <summary>
        /// Specifies the parameters type.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Type<T>();

        /// <summary>
        /// Specifies the parameters type.
        /// </summary>
        /// <param name="parameterType">The parameters type.</param>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Type(Type parameterType);

        /// <summary>
        /// Specifies the parameters name.
        /// </summary>
        /// <param name="parameterName">The parameters name.</param>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Name(string parameterName);

        /// <summary>
        /// Specifies that the parameter is an input parameter.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder In();

        /// <summary>
        /// Specifies that the parameter is an output parameter.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Out();

        /// <summary>
        /// Specifies that the parameter is a locale identifier (lcid).
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Lcid();

        /// <summary>
        /// Specifies that the parameter is a return value.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Retval();

        /// <summary>
        /// Specifies that the parameter is optional.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder Optional();

        /// <summary>
        /// Specifies that the parameter has a default value.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder HasDefault();

        /// <summary>
        /// Specifies that the parameter has field marshaling information.
        /// </summary>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder HasFieldMarshal();

        /// <summary>
        /// Sets a custom attribute.
        /// </summary>
        /// <param name="attributeBuilder">The custom attribute.</param>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder SetCustomAttribute(CustomAttributeBuilder attributeBuilder);
    }
}