// ***********************************************************************
// <copyright file="BaseFormatAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Reflection;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// Indicates that the property is a string that will receive a formatted value based on another property.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public abstract class BaseFormatAttribute : Attribute, IFormatAttribute
    {
        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        /// <value>The formatter.</value>
        public virtual IFormatter Formatter { get; set; }

        /// <summary>
        /// Formats the specified date within the model and sets the annotated property.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="annotatedPropertyName">Name of the annotated property.</param>
        public abstract void Format<T>(T model, string annotatedPropertyName);
    }
}
