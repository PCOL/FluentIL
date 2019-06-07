namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the dynamic method builder interface.
    /// </summary>
    public interface IDynamicMethodBuilder
    {
        /// <summary>
        /// Gets the methods body.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Body();

        /// <summary>
        /// Provides access to the method body.
        /// </summary>
        /// <param name="action">An action to emit the body IL.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Body(Action<IEmitter> action);

        /// <summary>
        /// Sets the methods return type.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Returns<TReturn>();

        /// <summary>
        /// Sets the methods return type.
        /// </summary>
        /// <param name="returnType">The return type.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Returns(Type returnType);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <typeparam name="TParam">The parameters type.</typeparam>
        /// <param name="parameterName">The parameters name.</param>
        /// <param name="attrs">The parameters attributes.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instsnce.</returns>
        IDynamicMethodBuilder Param<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <param name="action">A parameter builder action.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Param(Action<IParameterBuilder> action);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <param name="parameter">A parameter builder.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Param(IParameterBuilder parameter);

        /// <summary>
        /// Defines the methods parameters.
        /// </summary>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Params(params Type[] parameterTypes);

        /// <summary>
        /// Defines the methods parameters.
        /// </summary>
        /// <param name="parameters">A list of parameter builders.</param>
        /// <returns>The <see cref="IDynamicMethodBuilder"/> instance.</returns>
        IDynamicMethodBuilder Params(params IParameterBuilder[] parameters);

        /// <summary>
        /// Creates a parameter.
        /// </summary>
        /// <typeparam name="TParam">The parameters type.</typeparam>
        /// <param name="parameterName">The parameters name.</param>
        /// <param name="attrs">The parameters attributes.</param>
        /// <returns>The <see cref="IParameterBuilder"/> instsnce.</returns>
        IParameterBuilder CreateParam<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Creates a parameter.
        /// </summary>
        /// <param name="parameterType">The parameters type.</param>
        /// <param name="parameterName">The parameters name.</param>
        /// <param name="attrs">The parameters attribute.</param>
        /// <returns>The <see cref="IParameterBuilder"/> instance.</returns>
        IParameterBuilder CreateParam(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Defines the dynamic method.
        /// </summary>
        /// <returns>A <see cref="DynamicMethod"/> instance.</returns>
        DynamicMethod Define();
   }
}