namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Builders;

    /// <summary>
    /// <see cref="IPropertyBuilder"/> extension methods.
    /// </summary>
    public static class PropertyExtensionMethods
    {
        /// <summary>
        /// Gets a value indicating whether or not the <see cref="MethodInfo"/> is a property method.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>True if the <see cref="MethodInfo"/> is a property method; otherwise false.</returns>
        public static bool IsProperty(this MethodInfo methodInfo)
        {
            return methodInfo.IsPropertyGet() || methodInfo.IsPropertySet();
        }

        /// <summary>
        /// Gets a value indicating whether or not the <see cref="MethodInfo"/> is a property get method.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>True if the <see cref="MethodInfo"/> is a property get method; otherwise false.</returns>
        public static bool IsPropertyGet(this MethodInfo methodInfo)
        {
            return methodInfo != null && methodInfo.Name.StartsWith("get_");
        }

        /// <summary>
        /// Gets a value indicating whether or not the <see cref="MethodInfo"/> is a property set method.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>True if the <see cref="MethodInfo"/> is a property set method; otherwise false.</returns>
        public static bool IsPropertySet(this MethodInfo methodInfo)
        {
            return methodInfo != null && methodInfo.Name.StartsWith("set_");
        }

        /// <summary>
        /// Returns the name of the get method if the given <see cref="MemberInfo"/> is a property.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>The name of the get method if the <see cref="MemberInfo"/> is a property; otherwise null.</returns>
        public static string PropertyGetName(this MemberInfo memberInfo)
        {
            if (memberInfo != null &&
                memberInfo is PropertyInfo)
            {
                return string.Format("get_{0}", memberInfo.Name);
            }

            return null;
        }

        /// <summary>
        /// Returns the name of the set method if the given <see cref="MemberInfo"/> is a property.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>The name of the set method if the <see cref="MemberInfo"/> is a property; otherwise null.</returns>
        public static string PropertySetName(this MemberInfo memberInfo)
        {
            if (memberInfo != null &&
                memberInfo is PropertyInfo)
            {
                return string.Format("set_{0}", memberInfo.Name);
            }

            return null;
        }

        /// <summary>
        /// Defines a properties get method.
        /// </summary>
        /// <param name="propertyBuilder">A property builder.</param>
        /// <param name="action">A lambda to define the properties get method.</param>
        /// <returns>The <see cref="IPropertyBuilder"/> instance.</returns>
        public static PropertyBuilder Getter(this PropertyBuilder propertyBuilder, Func<MethodBuilder> action)
        {
            var method = action();
            propertyBuilder.SetGetMethod(method);
            return propertyBuilder;
        }

        /// <summary>
        /// Defines a properties set method.
        /// </summary>
        /// <param name="propertyBuilder">A property builder.</param>
        /// <param name="action">A lambda to define the properties get method.</param>
        /// <returns>The <see cref="IPropertyBuilder"/> instance.</returns>
        public static PropertyBuilder Setter(this PropertyBuilder propertyBuilder, Func<MethodBuilder> action)
        {
            var method = action();
            propertyBuilder.SetSetMethod(method);
            return propertyBuilder;
        }

        /// <summary>
        /// Emits IL to load the contents of a property onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="local">The <see cref="ILocal"/> that contains the property to read.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter GetProperty(this IEmitter emitter, string propertyName, ILocal local)
        {
            return emitter
                .LdLocS(local)
                .GetProperty(propertyName, local.LocalType);
        }

        /// <summary>
        /// Emits IL to load the contents of a property onto the evaluation stack.
        /// </summary>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter GetProperty<TProperty>(this IEmitter emitter, string propertyName)
        {
            return emitter
                .GetProperty(propertyName, typeof(TProperty));
        }

        /// <summary>
        /// Emits IL to load the contents of a property onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter GetProperty(this IEmitter emitter, string propertyName, Type propertyType)
        {
            MethodInfo getMethod = propertyType.GetProperty(propertyName).GetGetMethod();
            return emitter
                .CallVirt(getMethod);
        }

        /// <summary>
        /// Emits IL to pass the value on the top of set evaluation stack to a property set method.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="local">The <see cref="ILocal"/> that contains the property to set.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter SetProperty(this IEmitter emitter, string propertyName, ILocal local)
        {
            return emitter
                .LdLocS(local)
                .SetProperty(propertyName, local.LocalType);
        }

        /// <summary>
        /// Emits IL to call the set method of a property.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter SetProperty(this IEmitter emitter, string propertyName, Type propertyType)
        {
            var setMethod = propertyType.GetProperty(propertyName).GetSetMethod();
            return emitter
                .CallVirt(setMethod);
        }

        /// <summary>
        /// Emits IL to call the set method of a property.
        /// </summary>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter SetProperty<TProperty>(this IEmitter emitter, string propertyName)
        {
            return emitter.SetProperty(propertyName, typeof(TProperty));
        }
    }
}