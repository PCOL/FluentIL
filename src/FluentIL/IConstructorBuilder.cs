namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the Constructor builder interface.
    /// </summary>
    public interface IConstructorBuilder
    {
        /// <summary>
        /// Gets or sets the methods attributes.
        /// </summary>
        MethodAttributes MethodAttributes { get; set; }

        /// <summary>
        /// Gets the constructors body emitter.
        /// </summary>
        IEmitter Body();

        /// <summary>
        /// Sets the constructors calling convention.
        /// </summary>
        /// <param name="callingConvention"></param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        IConstructorBuilder CallingConvention(CallingConventions callingConvention);

        /// <summary>
        /// Defines a constructor parameter.
        /// </summary>
        /// <param name="parameterType">The parameters type.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="attrs">The parameters attributes.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        IConstructorBuilder Param(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Defines a constructor parameter.
        /// </summary>
        /// <param name="action">Parameter builder action</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        IConstructorBuilder Param(Action<IParameterBuilder> action);

        /// <summary>
        /// Defines the constructor parameters.
        /// </summary>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        IConstructorBuilder Params(params Type[] parameterTypes);

        /// <summary>
        /// Defines the constructor.
        /// </summary>
        /// <returns>A <see cref="ConstructorBuilder"/> instance.</returns>
        ConstructorBuilder Define();
    }
}