// ***********************************************************************
// <copyright file="NotAllowedWhenNotAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using Kore.Attributes.Validation.Resource;

namespace Kore.Attributes.Validation
{
    /// <summary>
    /// Validates that the annotated property is not specified when some other property is not a defined value.
    /// </summary>
    /// <seealso cref="Kore.Attributes.Validation.DualPropertyAttributeBase" />
    public class NotAllowedWhenNotAttribute : DualPropertyAttributeBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAllowedWhenNotAttribute" /> class.
        /// </summary>
        public NotAllowedWhenNotAttribute()
            : base("NotAllowedWhenNot")
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Returns true if this validator is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance != null)
            {
                PropertyInfo otherPropInfo = validationContext.ObjectType.GetProperty(this.OtherPropertyName);
                if (otherPropInfo != null)
                {
                    object otherPropValue = otherPropInfo.GetValue(validationContext.ObjectInstance);
                    if ((otherPropValue == null && this.OtherPropertyValue != null) || 
                        !otherPropInfo.GetValue(validationContext.ObjectInstance).Equals(this.OtherPropertyValue))
                    {
                        if (value != null &&
                            !string.IsNullOrEmpty(value.ToString()) &&
                            value.ToString() != false.ToString() &&
                            value.ToString() != 0.ToString())
                        {
                            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
