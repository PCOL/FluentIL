namespace FluentIL
{
    using System;
    using FluentIL.Builders;

    /// <summary>
    /// Represents a dynamic method factory.
    /// </summary>
    public class DynamicMethodFactory
    {
        /// <summary>
        /// The default <see cref="DynamicMethodFactory"/> instance.
        /// </summary>
        private static Lazy<DynamicMethodFactory> instance = new Lazy<DynamicMethodFactory>(() => new DynamicMethodFactory(), true);

        /// <summary>
        /// Gets the default dynamic method factory.
        /// </summary>
        public static DynamicMethodFactory Default
        {
            get
            {
                return instance.Value;
            }
        }

        /// <summary>
        /// Creates a new dynamic method.
        /// </summary>
        /// <param name="methodName">The methods name.</param>
        /// <param name="methodOwner">The methods owner.</param>
        /// <returns>A <see cref="IDynamicMethodBuilder"/> instance.</returns>
        public IDynamicMethodBuilder NewDynamicMethod(string methodName, Type methodOwner)
        {
            return new FluentDynamicMethodBuilder(methodName, methodOwner);
        }
    }
}