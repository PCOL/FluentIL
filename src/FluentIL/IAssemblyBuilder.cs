namespace FluentIL
{
    /// <summary>
    /// Defines the assembly builder interface.
    /// </summary>
    public interface IAssemblyBuilder
    {
        /// <summary>
        /// Creates a module.
        /// </summary>
        /// <param name="moduleName">The name of the module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        IModuleBuilder NewDynamicModule(string moduleName);
    }
}