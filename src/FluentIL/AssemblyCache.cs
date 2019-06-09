namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.DotNet.InternalAbstractions;
    using Microsoft.Extensions.DependencyModel;

    /// <summary>
    ///  Represents a cache of assemblies in the current application.
    /// </summary>
    internal static class AssemblyCache
    {
        /// <summary>
        /// Gets a type by name from the cache.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="dynamicOnly">Optional value indicating whether only dynamic assemblies should be checked or not.</param>
        /// <returns>A <see cref="Type"/> representing the type if found; otherwise null.</returns>
        public static Type GetType(string typeName, bool dynamicOnly = false)
        {
            foreach (var ass in AssemblyCache.GetAssemblies(dynamicOnly))
            {
                Type type = ass.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a list of loaded assemblies.
        /// </summary>
        /// <param name="dynamicOnly">Optional value indicating whether only dynamic assemblies should be checked or not.</param>
        /// <returns>A list of assemblies.</returns>
        public static IEnumerable<Assembly> GetAssemblies(bool dynamicOnly = false)
        {
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var compiled =
                from lib in DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId)
                let ass = Assembly.Load(lib)
                select ass;

            return FilterAssemblies(compiled, dynamicOnly);
        }

        private static IEnumerable<Assembly> FilterAssemblies(IEnumerable<Assembly> list, bool dynamicOnly)
        {
            if (list == null)
            {
                yield break;
            }

            foreach (var assembly in list)
            {
                if (dynamicOnly == false ||
                    assembly.IsDynamic == true)
                {
                   yield return assembly;
                }
            }
        }
    }
}