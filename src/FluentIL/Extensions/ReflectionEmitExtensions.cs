namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Builders;

    /// <summary>
    /// Reflection emit extension methods.
    /// </summary>
    public static class ReflectionEmitExtensions
    {
        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod(
            this ModuleBuilder moduleBuilder,
            string methodName,
            Type returnType)
        {
            return moduleBuilder
                .DefineGlobalMethod(methodName)
                .Returns(returnType);
        }

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="returnType">The return type.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod<TReturn>(
            this ModuleBuilder moduleBuilder,
            string methodName)
        {
            return moduleBuilder
                .DefineGlobalMethod(methodName)
                .Returns(typeof(TReturn));
        }

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod(
            this ModuleBuilder moduleBuilder,
            string methodName)
        {
            Func<string, MethodAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], MethodBuilder> defineFunc =
                (name,
                attributes,
                callingConvention,
                rType,
                returnTypeRequiredCustomModifiers,
                returnTypeOptionalCustomModifiers,
                parameterTypes,
                parameterTypeRequiredCustomModifiers,
                parameterTypeOptionalCustomModifiers) =>
                {
                    return moduleBuilder.DefineGlobalMethod(
                        name,
                        attributes |= MethodAttributes.Static,
                        callingConvention,
                        rType,
                        returnTypeRequiredCustomModifiers,
                        returnTypeOptionalCustomModifiers,
                        parameterTypes,
                        parameterTypeRequiredCustomModifiers,
                        parameterTypeOptionalCustomModifiers);
                };

            return new FluentMethodBuilder(methodName, defineFunc)
                .Static();
        }
    }
}