namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using FluentIL.Builders;

    /// <summary>
    /// Reflection extension methods.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets a method action.
        /// </summary>
        /// <typeparam name="T">The first paremeter type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Action<T> GetMethodAction<T>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T) });
            return (Action<T>)methodInfo.CreateDelegate(typeof(Action<T>), instance);
        }

        /// <summary>
        /// Gets a method action.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Action<T1, T2> GetMethodAction<T1, T2>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2) });
            return (Action<T1, T2>)methodInfo.CreateDelegate(typeof(Action<T1, T2>), instance);
        }

        /// <summary>
        /// Gets a method action.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <typeparam name="T3">The third paremeter type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Action<T1, T2, T3> GetMethodAction<T1, T2, T3>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2), typeof(T3) });
            return (Action<T1, T2, T3>)methodInfo.CreateDelegate(typeof(Action<T1, T2, T3>), instance);
        }

        /// <summary>
        /// Gets a method action.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <typeparam name="T3">The third paremeter type.</typeparam>
        /// <typeparam name="T4">The fourth paremeter type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Action<T1, T2, T3, T4> GetMethodAction<T1, T2, T3, T4>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) });
            return (Action<T1, T2, T3, T4>)methodInfo.CreateDelegate(typeof(Action<T1, T2, T3, T4>), instance);
        }

        /// <summary>
        /// Gets a method <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Func<TResult> GetMethodFunc<TResult>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, Type.EmptyTypes);
            return (Func<TResult>)methodInfo.CreateDelegate(typeof(Func<TResult>), instance);
        }

        /// <summary>
        /// Gets a method <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="T">The first parameter type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Func<T, TResult> GetMethodFunc<T, TResult>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T) });
            return (Func<T, TResult>)methodInfo.CreateDelegate(typeof(Func<T, TResult>), instance);
        }

        /// <summary>
        /// Gets a method <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Func<T1, T2, TResult> GetMethodFunc<T1, T2, TResult>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2) });
            return (Func<T1, T2, TResult>)methodInfo.CreateDelegate(typeof(Func<T1, T2, TResult>), instance);
        }

        /// <summary>
        /// Gets a method <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <typeparam name="T3">The third paremeter type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Func<T1, T2, T3, TResult> GetMethodFunc<T1, T2, T3, TResult>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2), typeof(T3) });
            return (Func<T1, T2, T3, TResult>)methodInfo.CreateDelegate(typeof(Func<T1, T2, T3, TResult>), instance);
        }

        /// <summary>
        /// Gets a method <see cref="Func{TResult}"/>.
        /// </summary>
        /// <typeparam name="T1">The first paremeter type.</typeparam>
        /// <typeparam name="T2">The second paremeter type.</typeparam>
        /// <typeparam name="T3">The third paremeter type.</typeparam>
        /// <typeparam name="T4">The fourth paremeter type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="instance">The object to get the method from.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The delegate.</returns>
        public static Func<T1, T2, T3, T4, TResult> GetMethodFunc<T1, T2, T3, T4, TResult>(this object instance, string methodName)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) });
            return (Func<T1, T2, T3, T4, TResult>)methodInfo.CreateDelegate(typeof(Func<T1, T2, T3, T4, TResult>), instance);
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <typeparam name="T">The methods generic argument.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetGenericMethod<T>(this Type type, string name)
        {
            return type.GetGenericMethod(name, typeof(T));
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <typeparam name="T1">The methods first generic argument.</typeparam>
        /// <typeparam name="T2">The methods second generic argument.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetGenericMethod<T1, T2>(this Type type, string name)
        {
            return type.GetGenericMethod(name, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <typeparam name="T1">The methods first generic argument.</typeparam>
        /// <typeparam name="T2">The methods second generic argument.</typeparam>
        /// <typeparam name="T3">The methods third generic argument.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetGenericMethod<T1, T2, T3>(this Type type, string name)
        {
            return type.GetGenericMethod(name, typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <typeparam name="T1">The methods first generic argument.</typeparam>
        /// <typeparam name="T2">The methods second generic argument.</typeparam>
        /// <typeparam name="T3">The methods third generic argument.</typeparam>
        /// <typeparam name="T4">The methods fourth generic argument.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetGenericMethod<T1, T2, T3, T4>(this Type type, string name)
        {
            return type.GetGenericMethod(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="genericArgumentCount">The number of generic arguments expected.</param>
        /// <param name="argumentTypes">An array of argument types.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethod(this Type type, string name, int genericArgumentCount, params Type[] argumentTypes)
        {
            IEnumerable<MethodInfo> methodInfos = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m => m.Name == name);

            MethodInfo methodInfo = methodInfos
                .Where(m => m.Name == name)
                .Select(
                    m => new
                    {
                        Method = m,
                        Parms = m.GetParameters(),
                        GenericArgs = m.GetGenericArguments()
                    })
                .Where(x =>
                    x.GenericArgs.Length == genericArgumentCount &&
                    ParameterTypesMatch(x.Parms, argumentTypes))
                .Select(x => x.Method)
                .FirstOrDefault();

            if (methodInfo == null)
            {
                // Check for extension methods
                foreach (var a in AssemblyCache.GetAssemblies())
                {
                    if (a.IsDynamic == false)
                    {
                        methodInfos = a.GetTypes()
                            .Where(t =>
                                t.IsSealed &&
                                !t.IsGenericType &&
                                !t.IsNested)
                            .SelectMany(
                                (t) =>
                                {
                                    return t
                                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                                        .Where(m => m.Name == name);
                                });

                        if (!methodInfos.IsNullOrEmpty())
                        {
                            methodInfo = methodInfos.Select(
                                m => new
                                {
                                    Method = m,
                                    Parms = m.GetParameters(),
                                    Args = m.GetGenericArguments()
                                })
                                .Where(x =>
                                    x.Method.IsDefined(typeof(ExtensionAttribute), false) &&
                                    x.Parms[0].ParameterType == type &&
                                    ParameterTypesMatch(x.Parms, 1, argumentTypes, 1, argumentTypes.Length - 1))
                                .Select(x => x.Method)
                                .FirstOrDefault();

                            if (methodInfo != null)
                            {
                                return methodInfo;
                            }
                        }
                    }
                }
            }

            return methodInfo;
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <typeparam name="T">The methods parameter type.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters<T>(this Type type, string name)
        {
            return type.GetMethodWithParameters<T>(name, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets a generic method by name.
        /// </summary>
        /// <typeparam name="T">The methods parameter type.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters<T>(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetMethodWithParameters(name, bindingFlags, typeof(T));
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <typeparam name="T1">The methods first parameter type.</typeparam>
        /// <typeparam name="T2">The methods second parameter type.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters<T1, T2>(this Type type, string name)
        {
            return type.GetMethodWithParameters<T1, T2>(name, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <typeparam name="T1">The methods first parameter type.</typeparam>
        /// <typeparam name="T2">The methods second parameter type.</typeparam>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters<T1, T2>(this Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetMethodWithParameters(name, bindingFlags, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="parameterTypes">The parameters the method must have.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters(this Type type, string name, params Type[] parameterTypes)
        {
            return type.GetMethodWithParameters(name, BindingFlags.Public | BindingFlags.Instance, parameterTypes);
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <param name="parameterTypes">The parameters the method must have.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethodWithParameters(this Type type, string name, BindingFlags bindingFlags, params Type[] parameterTypes)
        {
            List<MethodInfo> mis = type
                .GetMethods(bindingFlags).Where(m => m.Name == name).ToList();

            MethodInfo mi = mis.Select(
                m => new
                {
                    Method = m,
                    Parms = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => ParameterTypesAreSimilar(x.Parms, parameterTypes))
                .Select(x => x.Method)
                .FirstOrDefault();

            return mi;
        }

        /// <summary>
        /// Gets a method of the given name with the given parameters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="parameters">The parameters the method must have.</param>
        /// <returns>A <see cref="MethodInfo"/> representing the method if one has been found; otherwise null.</returns>
        public static MethodInfo GetMethodWithParameters(this Type type, string name, params ParameterInfo[] parameters)
        {
            return type.GetMethodWithParameters(name, BindingFlags.Instance | BindingFlags.Public, parameters);
        }

        /// <summary>
        /// Gets a method of the given name with the given parameters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <param name="parameters">The parameters the method must have.</param>
        /// <returns>A <see cref="MethodInfo"/> representing the method if one has been found; otherwise null.</returns>
        public static MethodInfo GetMethodWithParameters(this Type type, string name, BindingFlags bindingFlags, params ParameterInfo[] parameters)
        {
            return type.GetMethodWithParametersOrAttribute(name, bindingFlags, parameters);
        }

        /// <summary>
        /// Gets a method of the given name with the given parameters or attribute.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <param name="parameters">The parameters the method must have.</param>
        /// <param name="attributeType">The attribute to check for.</param>
        /// <returns>A <see cref="MethodInfo"/> representing the method if one has been found; otherwise null.</returns>
        public static MethodInfo GetMethodWithParametersOrAttribute(this Type type, string name, BindingFlags bindingFlags, ParameterInfo[] parameters, Type attributeType = null)
        {
            IEnumerable<MethodInfo> methodInfos = type
                .GetMethods(bindingFlags).Where(m => m.Name == name);

            MethodInfo methodInfo = methodInfos.Select(
                m => new
                {
                    Method = m,
                    Parms = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => ParameterTypesMatchOrAttribute(x.Parms, parameters, attributeType))
                .Select(x => x.Method)
                .FirstOrDefault();

            if (methodInfo == null)
            {
                // Check for extension methods
                bindingFlags |= BindingFlags.Static;

                var extensionTypes = type
                    .Assembly
                    .GetTypes()
                    .Where(t =>
                        t.IsSealed &&
                        !t.IsGenericType &&
                        !t.IsNested);

                foreach (var extensionType in extensionTypes)
                {
                    methodInfos = extensionType
                        .GetMethods(bindingFlags).Where(m => m.Name == name);

                    if (!methodInfos.IsNullOrEmpty())
                    {
                        methodInfo = methodInfos.Select(
                            m => new
                            {
                                Method = m,
                                Parms = m.GetParameters(),
                                Args = m.GetGenericArguments()
                            })
                            .Where(x =>
                                x.Method.IsDefined(typeof(ExtensionAttribute), false) &&
                                x.Parms[0].ParameterType == type &&
                                ParameterTypesMatchOrAttribute(x.Parms, 1, parameters, 0, parameters.Length, attributeType))
                            .Select(x => x.Method)
                            .FirstOrDefault();

                        if (methodInfo != null)
                        {
                            return methodInfo;
                        }
                    }
                }
            }

            return methodInfo;
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="bindingFlags">The binding flags to use.</param>
        /// <param name="genericArgs">An array of generic argument types.</param>
        /// <param name="parameters">An array of parameter types.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags, Type[] genericArgs, Type[] parameters)
        {
            var mis = type
                .GetMethods(bindingFlags).Where(m => m.Name == name);

            MethodInfo mi = mis.Select(
                m => new
                {
                    Method = m,
                    Parms = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x =>
                    TypeListsMatch(x.Args, genericArgs) &&
                    ParameterTypesMatch(x.Parms, parameters))
                .Select(x => x.Method)
                .FirstOrDefault();

            return mi;
        }

        /// <summary>
        /// Gets a method by name and paramters.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="methodToken">The method token.</param>
        /// <returns>A <see cref="MethodInfo"/> if the method is found; otherwise false.</returns>
        public static MethodInfo GetMethod(this Type type, int methodToken)
        {
            return type
                .GetMethods()
                .FirstOrDefault((m) => m.MetadataToken == methodToken);
        }

        /// <summary>
        /// Searches a <see cref="Type"/> for a method similar to the supplied one.
        /// </summary>
        /// <param name="type">The type to search.</param>
        /// <param name="methodInfo">The method to search for.</param>
        /// <returns>A <see cref="MethodInfo"/> if found; otherwise null.</returns>
        public static MethodInfo GetSimilarMethod(this Type type, MethodInfo methodInfo)
        {
            return type.GetSimilarMethod(methodInfo, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Searches a <see cref="Type"/> for a method similar to the supplied one.
        /// </summary>
        /// <param name="type">The type to search.</param>
        /// <param name="methodInfo">The method to search for.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="attributeType">An attribute that a method parameter can have.</param>
        /// <returns>A <see cref="MethodInfo"/> if found; otherwise null.</returns>
        public static MethodInfo GetSimilarMethod(this Type type, MethodInfo methodInfo, BindingFlags bindingFlags, Type attributeType = null)
        {
            if (type == null)
            {
                return null;
            }

            var parameters = methodInfo.GetParameters();
            var genArgs = methodInfo.GetGenericArguments();

            MethodInfo returnValue = null;
            var methods = type
                .GetMethods(bindingFlags)
                .Where(m => m.Name == methodInfo.Name)
                .ToList();

            if (methods.IsNullOrEmpty() == false)
            {
                returnValue = FindMethod(methods, genArgs, parameters, attributeType);
            }

            bindingFlags &= ~BindingFlags.Instance;
            bindingFlags |= BindingFlags.Static;

            // Has a method been found?
            if (returnValue == null)
            {
                var extensionMethods = type
                    .Assembly
                    .GetTypes()
                    .Where(t =>
                        t.IsSealed &&
                        !t.IsGenericType &&
                        !t.IsNested)
                    .SelectMany(
                        t =>
                        {
                            return type
                                .GetMethods(bindingFlags)
                                .Where(m => m.Name == methodInfo.Name);
                        });

                foreach (var extensionMethod in extensionMethods)
                {
                    returnValue = FindExtensionMethod(methods, genArgs, parameters, type, attributeType);
                    if (methodInfo != null)
                    {
                        return methodInfo;
                    }
                }
            }

            // Has a method been found?
            if (returnValue == null)
            {
                // Search all extension methods
                var extensionMethods = AssemblyCache
                    .GetAssemblies()
                    .SelectMany(a => a.GetTypes()
                        .Where(t =>
                            t.IsSealed &&
                            !t.IsGenericType &&
                            !t.IsNested)
                        .SelectMany(
                            t =>
                            {
                                return type
                                    .GetMethods(bindingFlags)
                                    .Where(m => m.Name == methodInfo.Name);
                            }));

                foreach (var extensionMethod in extensionMethods)
                {
                    returnValue = FindExtensionMethod(methods, genArgs, parameters, type, attributeType);
                    if (methodInfo != null)
                    {
                        return methodInfo;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Finds a method.
        /// </summary>
        /// <param name="methods">A list of <see cref="MethodInfo"/> instances.</param>
        /// <param name="genArgs">A list of generic arguments.</param>
        /// <param name="parameters">A list of parameters.</param>
        /// <param name="attributeType">An attribute type.</param>
        /// <returns>A <see cref="MethodInfo"/> if found; otherwise null.</returns>
        private static MethodInfo FindMethod(IEnumerable<MethodInfo> methods, Type[] genArgs, ParameterInfo[] parameters, Type attributeType)
        {
            return methods.Select(
                m => new
                {
                    Method = m,
                    Parms = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x =>
                    x.Args.Length == genArgs.Length &&
                    ParameterTypesMatchOrAttribute(x.Parms, 0, parameters, 0, parameters.Length, attributeType))
                .Select(x => x.Method)
                .FirstOrDefault();
        }

        /// <summary>
        /// Finds an extension method.
        /// </summary>
        /// <param name="methods">A list of <see cref="MethodInfo"/> instance.</param>
        /// <param name="genArgs">A list of generic type arguments.</param>
        /// <param name="parameters">A list of parameters.</param>
        /// <param name="extensionType">The extension type.</param>
        /// <param name="attributeType">An attribute type.</param>
        /// <returns>A <see cref="MethodInfo"/> if found; otherwise null.</returns>
        private static MethodInfo FindExtensionMethod(
            IEnumerable<MethodInfo> methods,
            Type[] genArgs,
            ParameterInfo[] parameters,
            Type extensionType,
            Type attributeType)
        {
            return methods.Select(
                m => new
                {
                    Method = m,
                    Parms = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => x.Method.IsDefined(typeof(ExtensionAttribute), false) &&
                    extensionType == x.Parms[0].ParameterType &&
                    x.Args.Length == genArgs.Length &&
                    ParameterTypesMatchOrAttribute(x.Parms, 1, parameters, 0, parameters.Length, attributeType))
                .Select(x => x.Method)
                .FirstOrDefault();
        }

        /// <summary>
        /// Checks a parameter list against a type lits to see if the types are similar.
        /// </summary>
        /// <param name="source">The source parameter list.</param>
        /// <param name="dest">The destination parameter type list.</param>
        /// <returns>True if the lists are similar; otherwise false.</returns>
        private static bool ParameterTypesAreSimilar(ParameterInfo[] source, Type[] dest)
        {
            if (source.Length != dest.Length)
            {
                return false;
            }

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].ParameterType.IsSimilarType(dest[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks a parameter list against a type list to see if they match.
        /// </summary>
        /// <param name="parameters">The source parameter list.</param>
        /// <param name="parameterTypes">The destination parameter type list.</param>
        /// <returns>True if the lists are similar; otherwise false.</returns>
        private static bool ParameterTypesMatch(ParameterInfo[] parameters, Type[] parameterTypes)
        {
            return ParameterTypesMatch(parameters, 0, parameterTypes, 0, parameterTypes.Length);
        }

        /// <summary>
        /// Checks a parameter list against a type list to see if they match.
        /// </summary>
        /// <param name="parameters">The parameter list.</param>
        /// <param name="parametersStartIndex">The parameter list index to start at.</param>
        /// <param name="parameterTypes">The parameter type list.</param>
        /// <param name="parameterTypesStartIndex">The parameter type list index to start at.</param>
        /// <param name="length">The number of parameters to check.</param>
        /// <returns>True if the lists are similar; otherwise false.</returns>
        private static bool ParameterTypesMatch(
            ParameterInfo[] parameters,
            int parametersStartIndex,
            Type[] parameterTypes,
            int parameterTypesStartIndex,
            int length)
        {
            if (parameters.Length - parametersStartIndex < length ||
                parameterTypes.Length - parameterTypesStartIndex < length)
            {
                return false;
            }

            for (int i = 0; i < length; i++)
            {
                if (parameters[parametersStartIndex + i].ParameterType.IsGenericParameter == false &&
                    parameters[parametersStartIndex + i].ParameterType.IsSimilarType(parameterTypes[parameterTypesStartIndex + i]) == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the parameter lists to see if the parameters are similar or if the destination parameter has a specific attribute applied.
        /// </summary>
        /// <param name="source">The source parameter list.</param>
        /// <param name="dest">The destination parameter list.</param>
        /// <param name="attribute">The optional attribute to check for.</param>
        /// <returns>True if the lists are similar or they have the attribute applied; otherwise false.</returns>
        private static bool ParameterTypesMatchOrAttribute(ParameterInfo[] source, ParameterInfo[] dest, Type attribute = null)
        {
            return ParameterTypesMatchOrAttribute(source, 0, dest, 0, source.Length, attribute);
        }

        /// <summary>
        /// Checks if two parameter lists have similar types or the parameter has an attribute.
        /// </summary>
        /// <param name="source">The first parameter list.</param>
        /// <param name="sourceIndex">The first start index.</param>
        /// <param name="dest">The second parameter list.</param>
        /// <param name="destIndex">The second start index.</param>
        /// <param name="length">The number of parameters to check.</param>
        /// <param name="attribute">The attribute to check for.</param>
        /// <returns>True if they are similar or they have the atrribute; otherwise false.</returns>
        private static bool ParameterTypesMatchOrAttribute(ParameterInfo[] source, int sourceIndex, ParameterInfo[] dest, int destIndex, int length, Type attribute = null)
        {
            ////if (source.Length != dest.Length)
            ////{
            ////    return false;
            ////}

            for (int i = 0; i < length; i++)
            {
                if (source[sourceIndex + i].ParameterType != dest[destIndex + i].ParameterType)
                {
                    if (attribute != null && dest[destIndex + i].HasAttribute(attribute) == false)
                    {
                        return false;
                    }

                    if (source[sourceIndex + i].ParameterType.IsGenericParameter == false &&
                        source[sourceIndex + i].ParameterType.IsSimilarType(dest[destIndex + i].ParameterType) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if the two types are either identical, or are both generic
        /// parameters or generic types with generic parameters in the same
        /// locations (generic parameters match any other generic paramter,
        /// but NOT concrete types).
        /// </summary>
        /// <param name="thisType">The <see cref="Type"/> being compared with.</param>
        /// <param name="type">The <see cref="Type"/> being compared.</param>
        /// <returns>True if they are similar; otherwise false.</returns>
        public static bool IsSimilarType(this Type thisType, Type type)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
            {
                thisType = thisType.GetElementType();
            }

            if (type.IsByRef)
            {
                type = type.GetElementType();
            }

            // Handle array types
            if (thisType.IsArray && type.IsArray)
            {
                return thisType.GetElementType().IsSimilarType(type.GetElementType());
            }

            // If the types are identical, or they're both generic parameters
            // or the special 'T' type, treat as a match
            if (thisType == type ||
                (thisType.IsGenericParameter == true && type.IsGenericParameter == false) ||
                (thisType.IsInterface == false && type.IsInterface == true) ||
                (thisType.IsEnum == true && type.IsEnum == true &&
                Enum.GetUnderlyingType(thisType) == Enum.GetUnderlyingType(type)))
            {
                return true;
            }

            // Handle any generic arguments
            if (thisType.IsGenericType && type.IsGenericType)
            {
                Type[] thisArguments = thisType.GetGenericArguments();
                Type[] arguments = type.GetGenericArguments();
                if (thisArguments.Length == arguments.Length)
                {
                    for (int i = 0; i < thisArguments.Length; ++i)
                    {
                        if (thisArguments[i].IsSimilarType(arguments[i]) == false)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a named method from a type with specific generic type arguments.
        /// </summary>
        /// <param name="type">The type to search for the method.</param>
        /// <param name="name">The method name.</param>
        /// <param name="genericTypes">A list of generic type arguments.</param>
        /// <returns>A <see cref="MethodInfo"/> if found; otherwise null.</returns>
        private static MethodInfo GetGenericMethod(this Type type, string name, params Type[] genericTypes)
        {
            MethodInfo mi = type
                .GetMethods()
                .Where(m => m.Name == name)
                .Select(
                    m => new
                    {
                        Method = m,
                        Parms = m.GetParameters(),
                        GenericArgs = m.GetGenericArguments()
                    })
                .Where(x => TypeNamesMatch(x.GenericArgs, genericTypes))
                .Select(x => x.Method)
                .FirstOrDefault();

            return mi;
        }

        /// <summary>
        /// Checks if two Type arrays match.
        /// </summary>
        /// <param name="array1">The first type array.</param>
        /// <param name="array2">The second type array.</param>
        /// <returns></returns>
        private static bool TypeNamesMatch(Type[] array1, Type[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i].Name != array2[i].Name)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TypeListsMatch(Type[] source, Type[] dest)
        {
            if (source.Length != dest.Length)
            {
                return false;
            }

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != dest[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Builds <see cref="MethodInfo"/> instances.
        /// </summary>
        /// <param name="type">The type to build a <see cref="MethodInfo"/> for.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="MethodInfoBuilder"/> instance.</returns>
        public static MethodInfoBuilder BuildMethodInfo(this Type type, string methodName)
        {
            return new MethodInfoBuilder(type, methodName);
        }
    }
}
