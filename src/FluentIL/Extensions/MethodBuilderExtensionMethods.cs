namespace FluentIL
{
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    /// <summary>
    /// <see cref="IMethodBuilder"/> and <see cref="MethodBuilder"/> extension methods.
    /// </summary>
    public static class MethodBuilderExtensionMethods
    {
        /// <summary>
        /// Indicates that the method is accessible to any object for which this object is in scope.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Public(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.Public;
            return builder;
        }

        /// <summary>
        /// Indicates that the method is accessible only to the current class.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Private(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.Private;
            return builder;
        }

        /// <summary>
        /// Indicates that the method is virtual.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Virtual(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.Virtual;
            return builder;
        }

        /// <summary>
        /// Indicates that the method hides by name and signature; otherwise, by name only.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder HideBySig(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.HideBySig;
            return builder;
        }

        /// <summary>
        /// Indicates that the method
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder SpecialName(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.SpecialName;
            return builder;
        }

        /// <summary>
        /// Indicates that the method
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder RTSpecialName(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.RTSpecialName;
            return builder;
        }

        /// <summary>
        /// Indicates that the method always gets a new slot in the vtable.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder NewSlot(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.NewSlot;
            return builder;
        }

        /// <summary>
        /// Indicates that the method is defined on the type; otherwise, it is defined per instance.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Static(this IMethodBuilder builder)
        {
            builder.Attributes |= MethodAttributes.Static;
            return builder;
        }

        /// <summary>
        /// Defines a parameter.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Param<TParam>(this IMethodBuilder methodBuilder)
        {
            return methodBuilder.Param<TParam>(null);
        }

        /// <summary>
        /// Defines a parameter.
        /// </summary>
        /// <param name="builder">A <see cref="IMethodBuilder"/> instance.</param>
        /// <param name="parameterName">The name of parameter.</param>
        /// <param name="attrs">The parameters attribtes</param>
        /// <returns>The <see cref="IMethoBuilder"/> instance.</returns>
        public static IMethodBuilder Param<TParam>(this IMethodBuilder methodBuilder, string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            return methodBuilder.Param(typeof(TParam), parameterName, attrs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="methodBuilder">A <see cref="MethodBuilder"/> instance.</param>
        /// <returns>An <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Body(this MethodBuilder methodBuilder)
        {
            DebugOutput.WriteLine("Body:");

            var ilEmitter = new ILGeneratorEmitter(methodBuilder.GetILGenerator());
            if (DebugOutput.Output == null)
            {
                return ilEmitter;
            }

            return new DebugEmitter(ilEmitter, DebugOutput.Output);
        }
    }
}