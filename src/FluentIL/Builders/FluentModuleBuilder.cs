namespace FluentIL.Builders
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of a <see cref="IModuleBuilder"/>
    /// </summary>
    internal class FluentModuleBuilder
        : IModuleBuilder
    {
        /// <summary>
        /// A <see cref="ModuleBuilder"/> instance.
        /// </summary>
        private readonly ModuleBuilder moduleBuilder;

        /// <summary>
        /// Initialises a new instance of the <see cref="ModuleBuilderImpl"/> class.
        /// </summary>
        /// <param name="moduleBuilder">A module builder.</param>
        public FluentModuleBuilder(ModuleBuilder moduleBuilder)
        {
            this.moduleBuilder = moduleBuilder;
        }

        /// <inheritdoc />>
        public ITypeBuilder NewType(string typeName)
        {
            return new FluentTypeBuilder(this.moduleBuilder, typeName);
        }

        /// <inheritdoc />>
        public IMethodBuilder NewGlobalMethod(string methodName)
        {
            return new FluentMethodBuilder(
                methodName,
                this.moduleBuilder.DefineGlobalMethod,
                () =>
                {
                    this.CreateGlobalFunctions();
                    return this.GetMethod(methodName);
                })
                .CallingConvention(CallingConventions.Standard);
        }

        /// <inheritdoc />
        public IModuleBuilder CreateGlobalFunctions()
        {
            this.moduleBuilder.CreateGlobalFunctions();
            return this;
        }

        /// <inheritdoc />
        public MethodInfo GetMethod(string methodName)
        {
            return this.moduleBuilder.GetMethod(methodName);
        }
    }
}