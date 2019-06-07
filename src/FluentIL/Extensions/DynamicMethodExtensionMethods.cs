using System;

namespace FluentIL
{
    /// <summary>
    /// <see cref="IDynamicMethodBuilder"/> extension methods.
    /// </summary>
    public static class DynamicMethodExtensionMethods
    {
        /// <summary>
        /// Defines the method.
        /// </summary>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <param name="delegateType">The delegates type.</param>
        /// <returns>The <see cref="Delegate"/> instance.</returns>
        public static Delegate Create(this IDynamicMethodBuilder dynamicMethodBuilder, Type delegateType)
        {
            return dynamicMethodBuilder.Define().CreateDelegate(delegateType);
        }

        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Action"/> instance.</returns>
        public static Action CreateAction(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Action)dynamicMethodBuilder.Define().CreateDelegate(typeof(Action));
        }

        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Action"/> instance.</returns>
        public static Action<T> CreateAction<T>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Action<T>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Action<T>));
        }

        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <typeparam name="T1">The first parameter type.</typeparam>
        /// <typeparam name="T2">The second parameter type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Action"/> instance.</returns>
        public static Action<T1, T2> CreateAction<T1, T2>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Action<T1, T2>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Action<T1, T2>));
        }

        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <typeparam name="T1">The first parameter type.</typeparam>
        /// <typeparam name="T2">The second parameter type.</typeparam>
        /// <typeparam name="T3">The third parameter type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Action"/> instance.</returns>
        public static Action<T1, T2, T3> CreateAction<T1, T2, T3>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Action<T1, T2, T3>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Action<T1, T2, T3>));
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Func{TReturn}"/> instance.</returns>
        public static Func<TReturn> CreateFunc<TReturn>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Func<TReturn>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Func<TReturn>));
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Func{T, TReturn}"/> instance.</returns>
        public static Func<T, TReturn> CreateFunc<T, TReturn>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Func<T, TReturn>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Func<T, TReturn>));
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="T1">The first parameter type.</typeparam>
        /// <typeparam name="T2">The second parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Func{T1, T2, TReturn}"/> instance.</returns>
        public static Func<T1, T2, TReturn> CreateFunc<T1, T2, TReturn>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Func<T1, T2, TReturn>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Func<T1, T2, TReturn>));
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="T1">The first parameter type.</typeparam>
        /// <typeparam name="T2">The second parameter type.</typeparam>
        /// <typeparam name="T3">The third parameter type.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="dynamicMethodBuilder">The dynamic method builder.</param>
        /// <returns>The <see cref="Func{T1, T2, T3, TReturn}"/> instance.</returns>
        public static Func<T1, T2, T3, TReturn> CreateFunc<T1, T2, T3, TReturn>(this IDynamicMethodBuilder dynamicMethodBuilder)
        {
            return (Func<T1, T2, T3, TReturn>)dynamicMethodBuilder.Define().CreateDelegate(typeof(Func<T1, T2, T3, TReturn>));
        }
    }
}