namespace FluentIL
{
    using System.Reflection;

    /// <summary>
    /// <see cref="IFieldBuilder"/> extension methods.
    /// </summary>
    public static class FieldBuilderExtensionMethods
    {
        /// <summary>
        /// Specifies that the field is accessible only by the parent type.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Private(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Private;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is accessible only by subtypes in this assembly.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder FamANDAssem(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.FamANDAssem;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is accessible throughout the assembly.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Assembly(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Assembly;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is accessible only by type and subtypes.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Family(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Family;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is accessible by subtypes anywhere, as well as throughout
        /// this assembly.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder FamORAssem(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.FamORAssem;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is accessible by any member for whom this scope is visible.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Public(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Public;
            return builder;
        }

        /// <summary>
        /// Specifies that the field represents the defined type, or else it is per-instance.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Static(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Static;
            return builder;
        }

        /// <summary>
        /// Specifies that the field is initialized only, and can be set only in the body
        /// of a constructor.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder InitOnly(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.InitOnly;
            return builder;
        }

        /// <summary>
        /// Specifies that the field's value is a compile-time (static or early bound) constant.
        /// Any attempt to set it throws a System.FieldAccessException.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder Literal(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.Literal;
            return builder;
        }

        /// <summary>
        /// Specifies that the field does not have to be serialized when the type is remoted.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder NotSerialized(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.NotSerialized;
            return builder;
        }

        /// <summary>
        /// Specifies that the field has a relative virtual address (RVA). The RVA is the
        /// location of the method body in the current image, as an address relative to the
        /// start of the image file in which it is located.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder HasFieldRVA(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.HasFieldRVA;
            return builder;
        }

        /// <summary>
        /// Specifies a special method, with the name describing how the method is special.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder SpecialName(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.SpecialName;
            return builder;
        }

        /// <summary>
        /// Specifies that the common language runtime (metadata internal APIs) should check
        /// the name encoding.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder RTSpecialName(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.RTSpecialName;
            return builder;
        }

        /// <summary>
        /// Specifies that the field has marshaling information.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder HasFieldMarshal(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.HasFieldMarshal;
            return builder;
        }

        /// <summary>
        /// Specifies that the field has a default value.
        /// </summary>
        /// <param name="builder">A <see cref="IFieldBuilder"/> instance.</param>
        /// <returns>The <see cref="IFieldBuilder"/> instance.</returns>
        public static IFieldBuilder HasDefault(this IFieldBuilder builder)
        {
            builder.FieldAttributes |= FieldAttributes.HasDefault;
            return builder;
        }
    }
}