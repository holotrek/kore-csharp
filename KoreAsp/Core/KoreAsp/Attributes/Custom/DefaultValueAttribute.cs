// ***********************************************************************
// <copyright file="DefaultValueAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Attributes.Custom
{
    /// <summary>
    /// An attribute that allows annotation of a model property with a defined default value.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class DefaultValueAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public object DefaultValue { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValueAttribute" /> class.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        public DefaultValueAttribute(object defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        #endregion
    }
}
