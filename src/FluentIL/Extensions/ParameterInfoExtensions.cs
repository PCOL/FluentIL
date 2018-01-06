namespace FluentIL
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensions to the <see cref="ParameterInfo"/> class.
    /// </summary>
    public static class ParameterInfoExtensions
    {
        /// <summary>
        /// Checks if a parameter has an attribute applied to it.
        /// </summary>
        /// <param name="parameterInfo">The parameter.</param>
        /// <param name="attributeType">The attribute type.</param>
        /// <returns>True if it has; otherwise false.</returns>
        public static bool HasAttribute(this ParameterInfo parameterInfo, Type attributeType)
        {
            return parameterInfo.GetCustomAttributes(attributeType).FirstOrDefault() != null;
        }
    }
}
