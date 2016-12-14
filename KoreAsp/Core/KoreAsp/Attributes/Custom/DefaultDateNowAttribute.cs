// ***********************************************************************
// <copyright file="DefaultDateNowAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Attributes.Custom
{
    /// <summary>
    /// An attribute that allows annotation of a model property with a defined default value of <c>DateTime.Now</c>.
    /// </summary>
    /// <seealso cref="KoreAsp.Attributes.Custom.DefaultValueAttribute" />
    public class DefaultDateNowAttribute : DefaultValueAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDateNowAttribute" /> class.
        /// </summary>
        public DefaultDateNowAttribute()
            : base(null)
        {
            this.DefaultValue = DateTime.UtcNow;
        }
    }
}
