namespace FluentIL
{
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    /// <summary>
    /// <see cref="IConstructorBuilder"/> and <see cref="ConstructorBuilder"/> extension methods.
    /// </summary>
    public static class ConstructorBuilderExtensionMethods
    {
        /// <summary>
        /// Adds the 'Public' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Public(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.Public;
            return builder;
        }

        /// <summary>
        /// Adds the 'Private' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Private(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.Private;
            return builder;
        }

        /// <summary>
        /// Adds the 'HideBySig' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder HideBySig(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.HideBySig;
            return builder;
        }

        /// <summary>
        /// Adds the 'Assembly' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Assembly(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.Assembly;
            return builder;
        }

        /// <summary>
        /// Adds the 'FamANDAssembly' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder FamANDAssem(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.FamANDAssem;
            return builder;
        }

        /// <summary>
        /// Adds the 'Family' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Family(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.Family;
            return builder;
        }

        /// <summary>
        /// Adds the 'FamORAssem' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder FamORAssem(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.FamORAssem;
            return builder;
        }

        /// <summary>
        /// Adds the 'SpecialName' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder SpecialName(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.SpecialName;
            return builder;
        }

        /// <summary>
        /// Adds the 'RTSpecialName' attribute to the constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder RTSpecialName(this IConstructorBuilder builder)
        {
            builder.MethodAttributes |= MethodAttributes.RTSpecialName;
            return builder;
        }

        /// <summary>
        /// Defines the constructor as static.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Static(this IConstructorBuilder builder)
        {
            builder.CallingConvention(CallingConventions.Standard);
            return builder;
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="builder">A <see cref="IConstructorBuilder"/> instance.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public static IConstructorBuilder Param<T>(this IConstructorBuilder builder, string parameterName)
        {
            builder.Param(typeof(T), parameterName);
            return builder;
        }

        /// <summary>
        /// Returns a <see cref="IEmitter"/> to define the constructors body.
        /// </summary>
        /// <param name="constructorBuilder">A <see cref="ConstructorBuilder"/> instance.</param>
        /// <returns>An <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Body(this ConstructorBuilder constructorBuilder)
        {
            DebugOutput.WriteLine("Body:");

            var ilEmitter = new ILGeneratorEmitter(constructorBuilder.GetILGenerator());
            if (DebugOutput.Output == null)
            {
                return ilEmitter;
            }

            return new DebugEmitter(ilEmitter, DebugOutput.Output);
        }
    }
}