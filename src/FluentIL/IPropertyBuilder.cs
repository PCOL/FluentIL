namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public interface IPropertyBuilder
    {
        /// <summary>
        /// Gets or sets the property attributes.
        /// </summary>
        /// <returns></returns>
        PropertyAttributes PropertyAttributes { get; set; }

        /// <summary>
        /// Sets the attributes for the property.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IPropertyBuilder Attributes(PropertyAttributes attributes);

        /// <summary>
        /// Gets or adds the property get method builder.
        /// </summary>
        /// <returns></returns>
        IPropertyBuilder Getter(Action<IMethodBuilder> action = null);

        /// <summary>
        /// Gets or adds the property get method builder.
        /// </summary>
        /// <returns></returns>
        IMethodBuilder Getter();

        /// <summary>
        /// Gets or adds the property set method builder.
        /// </summary>
        IPropertyBuilder Setter(Action<IMethodBuilder> action = null);

        /// <summary>
        /// Gets or adds the property set method builder.
        /// </summary>
        IMethodBuilder Setter();

        /// <summary>
        /// Defines the property.
        /// </summary>
        /// <returns>A <see cref="PropertyBuilder"/>.</returns>
        PropertyBuilder Define();
    }
}