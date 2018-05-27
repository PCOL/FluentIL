namespace FluentIL
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Represents a type factory.
    /// </summary>
    public class TypeFactory
    {
        /// <summary>
        /// The default <see cref="TypeFactory"/> instance.
        /// </summary>
        private static Lazy<TypeFactory> instance = new Lazy<TypeFactory>(() => new TypeFactory("Default", "Default"), true);

        /// <summary>
        /// The assemlby cache.
        /// </summary>
        private AssemblyBuilderCache assemblyCache;

        /// <summary>
        /// The assembly builder.
        /// </summary>
        private IAssemblyBuilder assemblyBuilder;

        /// <summary>
        /// THe module builder
        /// </summary>
        private IModuleBuilder moduleBuilder;

        /// <summary>
        /// Initialises a new instance of the <see cref="TypeFactory"/> class.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="moduleName">The module name.</param>
        public TypeFactory(string assemblyName, string moduleName)
        {
            Utility.ThrowIfArgumentNullEmptyOrWhitespace(assemblyName, nameof(assemblyName));
            Utility.ThrowIfArgumentNullEmptyOrWhitespace(moduleName, nameof(moduleName));

            this.assemblyCache = new AssemblyBuilderCache();
            this.assemblyBuilder =  this.assemblyCache.GetOrCreateAssemblyBuilder(assemblyName);
            this.moduleBuilder = this.assemblyBuilder.NewDynamicModule(moduleName);
        }

        /// <summary>
        /// Gets the default type factory.
        /// </summary>
        public static TypeFactory Default
        {
            get
            {
                return instance.Value;
            }
        }

        /// <summary>
        /// Defines a anonymous type.
        /// </summary>
        /// <returns>A type builder.</returns>
        public ITypeBuilder NewType()
        {
            return this.NewType(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Defines a named type.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>A type builder.</returns>
        public ITypeBuilder NewType(string typeName)
        {
            return this.moduleBuilder.NewType(typeName);
        }

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <returns></returns>
        public IMethodBuilder NewGlobalMethod(string methodName)
        {
            return this.moduleBuilder.NewGlobalMethod(methodName);
        }

        /// <summary>
        /// Creates the global functions in a module.
        /// </summary>
        public void CreateGlobalFunctions()
        {
            this.moduleBuilder.CreateGlobalFunctions();
        }

        /// <summary>
        /// Gets a method from the type factory.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <returns>A <see cref="MethodInfo"/> instance if found; otherwise null.</returns>
        public MethodInfo GetMethod(string methodName)
        {
            return this.moduleBuilder.GetMethod(methodName);
        }

                /// <summary>
        /// Gets a type by name from the current <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="dynamicOnly">Optional value indicating whether only dynamic assemblies should be checked or not.</param>
        /// <returns>A <see cref="Type"/> representing the type if found; otherwise null.</returns>
        public Type GetType(string typeName, bool dynamicOnly = false)
        {
            var list = this.assemblyCache.GetAssemblies()
                .Union(AssemblyCache.GetAssemblies(dynamicOnly));

            foreach (var ass in list)
            {
                Type type = ass.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}