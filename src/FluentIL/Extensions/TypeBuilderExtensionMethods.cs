namespace FluentIL
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="ITypeBuilder"/> extension methods.
    /// </summary>
    public static class TypeBuilderExtensionMethods
    {
        /// <summary>
        /// Specifies that the type is public.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>The type builder instance.</returns>
        public static ITypeBuilder Public(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.Public;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that the type is not public.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>The type builder instance.</returns>
        public static ITypeBuilder NotPublic(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.NotPublic;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that the type is a class.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>A field builder.</returns>
        public static ITypeBuilder Class(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.Class;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that the type is sealed.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>A field builder.</returns>
        public static ITypeBuilder Sealed(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.Sealed;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that the type is an interface.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>A field builder.</returns>
        public static ITypeBuilder Interface(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.Interface;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that the type is abstract.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>A field builder.</returns>
        public static ITypeBuilder Abstract(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.Abstract;
            return typeBuilder;
        }

        /// <summary>
        /// Specifies that calling static methods on the type does not force the system
        /// to initialize the type.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <returns>A field builder.</returns>
        public static ITypeBuilder BeforeFieldInit(this ITypeBuilder typeBuilder)
        {
            typeBuilder.TypeAttributes |= TypeAttributes.BeforeFieldInit;
            return typeBuilder;
        }

        /// <summary>
        /// Creates a field.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>A field builder.</returns>
        public static IFieldBuilder NewField<T>(this ITypeBuilder typeBuilder, string fieldName)
        {
            return typeBuilder.NewField(fieldName, typeof(T));
        }

        /// <summary>
        /// Creates a property.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>A properrty builder.</returns>
        public static IPropertyBuilder NewProperty<T>(this ITypeBuilder typeBuilder, string propertyName)
        {
            return typeBuilder.NewProperty(propertyName, typeof(T));
        }

        /// <summary>
        /// Creates a method.
        /// </summary>
        /// <param name="typeBuilder">A type builder.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A method builder instance.</returns>
        public static IMethodBuilder NewMethod<TReturn>(this ITypeBuilder typeBuilder, string name)
        {
            return typeBuilder.NewMethod(
                name,
                MethodAttributes.Public,
                CallingConventions.HasThis,
                typeof(TReturn));
        }
    }
}