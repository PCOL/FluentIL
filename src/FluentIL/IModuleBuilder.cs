namespace FluentIL
{
    using System.Reflection;

    /// <summary>
    /// Defines a module builder.
    /// </summary>
    public interface IModuleBuilder
    {
        /// <summary>
        /// Defines a type.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>A <see cref="ITypeBuilder"/> instance.</returns>
        ITypeBuilder NewType(string typeName);

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder NewGlobalMethod(string methodName);

        /// <summary>
        /// Creates any global methods defined in the module.
        /// </summary>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        IModuleBuilder CreateGlobalFunctions();

        /// <summary>
        /// Gets a global method defined on the module.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> instance if found; otherwise null.</returns>
        MethodInfo GetMethod(string methodName);
    }
}