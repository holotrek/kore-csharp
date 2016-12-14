// ***********************************************************************
// <copyright file="MustBeLesserThanAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KoreAsp.Attributes.Validation
{
    /// <summary>
    /// Validates that the annotated property is a specific value when some other property is a defined value.
    /// </summary>
    /// <seealso cref="KoreAsp.Attributes.Validation.DualPropertyAttributeBase" />
    public class MustBeLesserThanAttribute : DualPropertyAttributeBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeLesserThanAttribute" /> class.
        /// </summary>
        public MustBeLesserThanAttribute()
            : base("MustBeLesserThan")
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            if (this.OtherPropertyValue != null)
            {
                return string.Format(this.GetErrorMessage(), name, this.GetOtherPropertyValueForError());
            }
            else
            {
                return string.Format(this.GetErrorMessage(), name, this.GetOtherPropertyDisplayName());
            }
        }

        #endregion

        #region Private Methods

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
                object otherPropValue = null;
                if (this.OtherPropertyValue != null)
                {
                    otherPropValue = this.OtherPropertyValue;
                }
                else
                {
                    PropertyInfo otherPropInfo = validationContext.ObjectType.GetProperty(this.OtherPropertyName);
                    if (otherPropInfo != null)
                    {
                        otherPropValue = otherPropInfo.GetValue(validationContext.ObjectInstance);
                    }
                }

                IComparable thisVal = value as IComparable;
                bool passed = false;
                if (thisVal == null)
                {
                    // Assuming null is always lesser than something, if otherPropValue is not null it will be "greater"
                    passed = otherPropValue != null;
                }
                else if (otherPropValue == null)
                {
                    // If otherPropValue is null, then this value can not possibly be lesser
                    passed = false;
                }
                else
                {
                    passed = thisVal.CompareTo(otherPropValue) < 0;
                }

                if (!passed)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return null;
        }

        #endregion
    }
}
