// ***********************************************************************
// <copyright file="MustBeWhenNotAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Kore.Attributes.Validation
{
    /// <summary>
    /// Validates that the annotated property is a specific value when some other property is a defined value.
    /// </summary>
    /// <seealso cref="Kore.Attributes.Validation.DualPropertyAttributeBase" />
    public class MustBeWhenNotAttribute : DualPropertyAttributeBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the required property value.
        /// </summary>
        /// <value>The required property value.</value>
        public object RequiredPropertyValue { get; set; }

        /// <summary>
        /// Gets or sets the required property value for error.
        /// </summary>
        /// <value>The required property value for error.</value>
        public string RequiredPropertyValueForError { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustBeWhenNotAttribute" /> class.
        /// </summary>
        public MustBeWhenNotAttribute()
            : base("MustBeWhenNot")
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the other property value for error.
        /// </summary>
        /// <returns>Other property value for error.</returns>
        public virtual string GetRequiredPropertyValueForError()
        {
            if (string.IsNullOrWhiteSpace(this.RequiredPropertyValueForError))
            {
                return this.RequiredPropertyValue == null ? string.Empty : this.RequiredPropertyValue.ToString();
            }

            return this.RequiredPropertyValueForError;
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.GetErrorMessage(), name, this.GetRequiredPropertyValueForError(), this.GetOtherPropertyDisplayName(), this.GetOtherPropertyValueForError());
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
                        if (!value.Equals(this.RequiredPropertyValue))
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
