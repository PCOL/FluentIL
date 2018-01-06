namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public interface IMethodBuilder
    {
        /// <summary>
        /// Gets or sets the methods attributes.
        /// </summary>
        MethodAttributes Attributes { get; set; }

        /// <summary>
        /// Gets the methods body.
        /// </summary>
        IEmitter Body();

        /// <summary>
        /// Provides access to the method body.
        /// </summary>
        /// <param name="action">An action to emit the body IL.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Body(Action<IEmitter> action);

        /// <summary>
        /// Sets the methods atrributes.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder MethodAttributes(MethodAttributes attributes);

        /// <summary>
        /// Sets the calling convention.
        /// </summary>
        /// <param name="convention">The calling convention.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder CallingConvention(CallingConventions convention);

        /// <summary>
        /// Sets the methods return type.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Returns<TReturn>();

        /// <summary>
        /// Sets the methods return type.
        /// </summary>
        /// <param name="returnType">The return type.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Returns(Type returnType);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <typeparam name="T">The parameters type.</typeparam>
        /// <param name="parameterName">The parameters name.</param>
        /// <param name="attrs">The parameters attributes.`</param>
        /// <returns></returns>
        IMethodBuilder Param<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <param name="parameterType">The parameters type.</param>
        /// <param name="parameterName">The parameters name.`</param>
        /// <param name="attrs">The parameters attribute.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Param(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None);

        /// <summary>
        /// Adds a parameter to the method.
        /// </summary>
        /// <param name="action">A parameter builder action.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Param(Action<IParameterBuilder> action);

        /// <summary>
        /// Defines the methods parameters.
        /// </summary>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Params(params Type[] parameterTypes);

        /// <summary>
        /// Checks if the method has a named parameter.
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>True if it has; otherwisw false.</returns>
        bool HasParameter(string parameterName);

        /// <summary>
        /// Defines a generic parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The <see cref="IGenericParameterBuilder"/> instance.</returns>
        IGenericParameterBuilder NewGenericParameter(string parameterName);

        /// <summary>
        /// Defines a generic parameter.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterBuilder"></param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder NewGenericParameter(string parameterName, Action<IGenericParameterBuilder> parameterBuilder);

        /// <summary>
        /// Defines generic parameters.
        /// </summary>
        /// <param name="parameterNames">The names of the parameters.</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder NewGenericParameters(params string[] parameterNames);

        /// <summary>
        /// Defines generic parameters.
        /// </summary>
        /// <param name="parameterNames">The names of the parameters.</param>
        /// <param name="action">The action to update the parameters</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder NewGenericParameters(string[] parameterNames, Action<IGenericParameterBuilder[]> action);

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns></returns>
        GenericTypeParameterBuilder GetGenericParameter(string parameterName);

        /// <summary>
        /// Defines the method.
        /// </summary>
        /// <returns>A <see cref="MethodInfo"/> instance.</returns>
        MethodBuilder Define();
    }
}