// ***********************************************************************
// <copyright file="IFormatAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Formatters
{
    /// <summary>
    /// A contract for an attribute that can format a property a certain way.
    /// </summary>
    public interface IFormatAttribute
    {
        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        /// <value>The formatter.</value>
        IFormatter Formatter { get; set; }

        /// <summary>
        /// Formats the specified date within the model and sets the annotated property.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="annotatedPropertyName">Name of the annotated property.</param>
        void Format<T>(T model, string annotatedPropertyName);
    }

    /// <summary>
    /// A contract for an attribute that can format a property a certain way.
    /// </summary>
    /// <typeparam name="TFormatter">The type of the formatter.</typeparam>
    /// <seealso cref="Kore.Formatters.IFormatAttribute" />
    public interface IFormatAttribute<TFormatter> : IFormatAttribute
        where TFormatter : IFormatter
    {
        /// <summary>
        /// Gets the specific formatter for this attribute.
        /// </summary>
        TFormatter SpecificFormatter { get; }
    }
}
