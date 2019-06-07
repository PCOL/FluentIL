namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensions to Type and Object classes.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if an object has a name property.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>True if the object has the property; otherwise false.</returns>
        public static bool HasProperty(this object obj, string propertyName)
        {
            if (obj != null)
            {
                return obj.GetType().GetProperty(propertyName) != null;
            }

            return false;
        }

        /// <summary>
        /// Gets the value of a named property.
        /// </summary>
        /// <param name="obj">The object which has the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The property value.</returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj != null)
            {
                PropertyInfo pi = obj.GetType().GetProperty(propertyName);
                if (pi != null)
                {
                    return pi.GetValue(obj);
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the value of a named property.
        /// </summary>
        /// <param name="obj">The object to set the property on.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value to set the property to.</param>
        public static void SetPropertyValue(this object obj, string propertyName, object propertyValue)
        {
            if (obj != null)
            {
                PropertyInfo pi = obj.GetType().GetProperty(propertyName);
                if (pi != null)
                {
                    pi.SetValue(obj, propertyValue);
                }
            }
        }

        /// <summary>
        /// Returns the representation an object as a string containing the properties with values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>String containing the object property values.</returns>
        public static string PropertyValuesAsString(this object obj)
        {
            string ret = string.Empty;
            const string Separator = ", ";

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                object value = obj.GetPropertyValue(info.Name);
                if (value != null)
                {
                    if (ret.Length > 0)
                    {
                        ret += Separator;
                    }

                    ret += info.Name + ": " + value.ToString();
                }
            }

            return ret;
        }

        /// <summary>
        /// Checks if an <see cref="IEnumerable{T}"/> list is null or empty.
        /// </summary>
        /// <typeparam name="T">The list type.</typeparam>
        /// <param name="list">The list instance.</param>
        /// <returns>True if the list is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Any() == false;
        }

        /// <summary>
        /// Checks if a <see cref="IDictionary{TKey, TValue}"/> is null or empty.
        /// </summary>
        /// <typeparam name="TKey">The dictionary key type.</typeparam>
        /// <typeparam name="TValue">The dictionary value type.</typeparam>
        /// <param name="dictionary">The dictionary instance.</param>
        /// <returns>True if the dictionary is null or empty.</returns>
        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary == null || dictionary.Any() == false;
        }

        /// <summary>
        /// Checks if a <see cref="Type"/> is a delegate.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if it is; otherwise false.</returns>
        public static bool IsDelegate(this Type type)
        {
            return type != null &&
                typeof(Delegate).IsAssignableFrom(type);
        }
    }
}
