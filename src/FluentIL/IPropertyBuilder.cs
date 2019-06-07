namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the property builder interface.
    /// </summary>
    public interface IPropertyBuilder
    {
        /// <summary>
        /// Gets or sets the property attributes.
        /// </summary>
        PropertyAttributes PropertyAttributes { get; set; }

        /// <summary>
        /// Gets the set method.
        /// </summary>
        IMethodBuilder SetMethod { get; set; }

        /// <summary>
        /// Gets the get method.
        /// </summary>
        IMethodBuilder GetMethod { get; set; }

        /// <summary>
        /// Sets the attributes for the property.
        /// </summary>
        /// <param name="attributes">The attributes to set.</param>
        /// <returns>The <see cref="IPropertyBuilder"/> instance.</returns>
        IPropertyBuilder Attributes(PropertyAttributes attributes);

        /// <summary>
        /// Gets or adds the property get method builder.
        /// </summary>
        /// <param name="action">A method builder action.</param>
        /// <returns>The <see cref="IPropertyBuilder"/> instance.</returns>
        IPropertyBuilder Getter(Action<IMethodBuilder> action = null);

        /// <summary>
        /// Gets or adds the property get method builder.
        /// </summary>
        /// <returns>The get methods <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Getter();

        /// <summary>
        /// Gets or adds the property set method builder.
        /// </summary>
        /// <param name="action">A method builder action.</param>
        /// <returns>The <see cref="IPropertyBuilder"/> instance.</returns>
        IPropertyBuilder Setter(Action<IMethodBuilder> action = null);

        /// <summary>
        /// Gets or adds the property set method builder.
        /// </summary>
        /// <returns>The set methods <see cref="IMethodBuilder"/> instance.</returns>
        IMethodBuilder Setter();

        /// <summary>
        /// Defines the property.
        /// </summary>
        /// <returns>A <see cref="PropertyBuilder"/>.</returns>
        PropertyBuilder Define();
    }
}