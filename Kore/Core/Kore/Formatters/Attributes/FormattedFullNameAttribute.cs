// ***********************************************************************
// <copyright file="FormattedFullNameAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Reflection;
using Kore.Composites;

namespace Kore.Formatters
{
    /// <summary>
    /// Indicates that the property is a string that will receive the formatted full name with the data from
    /// another property of type <see cref="Kore.Composites.FullName"/>
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class FormattedFullNameAttribute : BaseFormatAttribute, IFormatAttribute<IFullNameFormatter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedFullNameAttribute" /> class.
        /// </summary>
        /// <param name="fullNameProperty">The full name property.</param>
        public FormattedFullNameAttribute(string fullNameProperty)
        {
            this.FullNameProperty = fullNameProperty;
        }

        /// <summary>
        /// Gets the specific formatter for this attribute.
        /// </summary>
        public IFullNameFormatter SpecificFormatter
        {
            get
            {
                return (IFullNameFormatter)this.Formatter;
            }
        }

        /// <summary>
        /// Gets or sets the full name property.
        /// </summary>
        /// <value>
        /// The full name property.
        /// </value>
        public string FullNameProperty { get; set; }

        /// <summary>
        /// Formats the specified full name within the model and sets the annotated property.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="annotatedPropertyName">Name of the annotated property.</param>
        public override void Format<T>(T model, string annotatedPropertyName)
        {
            Type containerType = typeof(T);
            PropertyInfo fullNameProp = containerType.GetProperty(this.FullNameProperty);
            PropertyInfo stringProp = containerType.GetProperty(annotatedPropertyName);

            if (fullNameProp != null && stringProp != null)
            {
                FullName fn = fullNameProp.GetValue(model) as FullName;
                if (fn != null)
                {
                    stringProp.SetValue(model, this.SpecificFormatter.ToFullName(fn));
                }
            }
        }
    }
}
