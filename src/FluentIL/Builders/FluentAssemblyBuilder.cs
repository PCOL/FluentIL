namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    /// <summary>
    /// An implementation of the <see cref="IAssemblyBuilder"/> interafce.
    /// </summary>
    internal class FluentAssemblyBuilder
        : IAssemblyBuilder
    {
        /// <summary>
        /// A list of module builders.
        /// </summary>
        private Dictionary<string, FluentModuleBuilder> modules = new Dictionary<string, FluentModuleBuilder>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentAssemblyBuilder"/> class.
        /// </summary>
        /// <param name="assemblyBuilder">A <see cref="AssemblyBuilder"/> instance.</param>
        public FluentAssemblyBuilder(AssemblyBuilder assemblyBuilder)
        {
            this.AssemblyBuilder = assemblyBuilder;
        }

        /// <summary>
        /// Gets an <see cref="AssemblyBuilder"/> instance.
        /// </summary>
        public AssemblyBuilder AssemblyBuilder { get; }

        /// <summary>
        /// Defines a module.
        /// </summary>
        /// <param name="moduleName">The name of the module.</param>
        /// <returns>A <see cref="IModuleBuilder" /> instance.</returns>
        public IModuleBuilder NewDynamicModule(string moduleName)
        {
            FluentModuleBuilder impl;
            if (this.modules.TryGetValue(moduleName, out impl) == false)
            {
                var module = this.AssemblyBuilder.DefineDynamicModule(moduleName);
                impl = new FluentModuleBuilder(module);
                this.modules.Add(moduleName, impl);
            }

            return impl;
        }
    }
}