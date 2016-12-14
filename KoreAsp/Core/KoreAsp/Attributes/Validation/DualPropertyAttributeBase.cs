// ***********************************************************************
// <copyright file="DualPropertyAttributeBase.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Resources;

namespace KoreAsp.Attributes.Validation
{
    /// <summary>
    /// Base validation attribute for validation attributes that compare 2 properties.
    /// </summary>
    /// <seealso cref="KoreAsp.Attributes.Validation.ValidationAttributeBase" />
    public abstract class DualPropertyAttributeBase : ValidationAttributeBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the other property.
        /// </summary>
        /// <value>The name of the other property.</value>
        public string OtherPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the other property display name.
        /// </summary>
        /// <value>The name of the other property display name.</value>
        public string OtherPropertyDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the other property display name.
        /// </summary>
        /// <value>The name of the other property display name.</value>
        public Type OtherPropertyResourceType { get; set; }

        /// <summary>
        /// Gets or sets the other property value.
        /// </summary>
        /// <value>The other property value.</value>
        public object OtherPropertyValue { get; set; }

        /// <summary>
        /// Gets or sets the other property value for error.
        /// </summary>
        /// <value>The other property value for error.</value>
        public string OtherPropertyValueForError { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DualPropertyAttributeBase" /> class.
        /// </summary>
        public DualPropertyAttributeBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DualPropertyAttributeBase"/> class.
        /// </summary>
        /// <param name="errorMessageResourceName">Name of the error message resource.</param>
        public DualPropertyAttributeBase(string errorMessageResourceName)
            : base(errorMessageResourceName)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the display name of the other property.
        /// </summary>
        /// <returns>Other property display name.</returns>
        public virtual string GetOtherPropertyDisplayName()
        {
            if (string.IsNullOrWhiteSpace(this.OtherPropertyDisplayName))
            {
                this.OtherPropertyDisplayName = this.OtherPropertyName;
            }

            if (this.OtherPropertyResourceType != null)
            {
                ResourceManager rm = new ResourceManager(this.OtherPropertyResourceType);
                this.OtherPropertyDisplayName = rm.GetString(this.OtherPropertyDisplayName);
            }

            return this.OtherPropertyDisplayName;
        }

        /// <summary>
        /// Gets the other property value for error.
        /// </summary>
        /// <returns>Other property value for error.</returns>
        public virtual string GetOtherPropertyValueForError()
        {
            if (string.IsNullOrWhiteSpace(this.OtherPropertyValueForError))
            {
                return this.OtherPropertyValue == null ? string.Empty : this.OtherPropertyValue.ToString();
            }

            return this.OtherPropertyValueForError;
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.GetErrorMessage(), name, this.GetOtherPropertyDisplayName(), this.GetOtherPropertyValueForError());
        }

        #endregion
    }
}
