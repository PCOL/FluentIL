namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Builders;

    /// <summary>
    /// Represents an <see cref="AssemblyBuilder"/> cache.
    /// </summary>
    internal class AssemblyBuilderCache
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private Dictionary<string, FluentAssemblyBuilder> cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyBuilderCache"/> class.
        /// </summary>
        public AssemblyBuilderCache()
        {
            this.cache = new Dictionary<string, FluentAssemblyBuilder>();
        }

        /// <summary>
        /// Gets or creates an <see cref="AssemblyBuilder"/> and <see cref="ModuleBuilder"/> pair.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly builder.</param>
        /// <returns>An assembly builder instance.</returns>
        public IAssemblyBuilder GetOrCreateAssemblyBuilder(string assemblyName)
        {
            FluentAssemblyBuilder impl;
            if (this.cache.TryGetValue(assemblyName, out impl) == false)
            {
                AssemblyName name = new AssemblyName(assemblyName);
                var builder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
                impl = new FluentAssemblyBuilder(builder);
                this.cache.Add(assemblyName, impl);
            }

            return impl;
        }

        /// <summary>
        /// Removes an assembly builder and all of its module builders.
        /// </summary>
        /// <param name="name">The name of the assembly builder.</param>
        /// <returns>True if removed; otherwise false.</returns>
        public bool RemoveAssemblyBuilder(string name)
        {
            return this.cache.Remove(name);
        }

        /// <summary>
        /// Returns a list of assmiblies.
        /// </summary>
        /// <returns>A list of assemblies</returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            return this.cache.Select(a => a.Value.AssemblyBuilder);
        }
    }
}
