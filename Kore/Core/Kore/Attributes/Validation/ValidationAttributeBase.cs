// ***********************************************************************
// <copyright file="ValidationAttributeBase.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.Resources;
using Kore.Attributes.Validation.Resource;

namespace Kore.Attributes.Validation
{
    /// <summary>
    /// Base validation attribute for validation attributes that compare 2 properties.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public abstract class ValidationAttributeBase : ValidationAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttributeBase" /> class.
        /// </summary>
        public ValidationAttributeBase()
        {
            this.ErrorMessageResourceType = typeof(AttributeValidationMessages);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttributeBase"/> class.
        /// </summary>
        /// <param name="errorMessageResourceName">Name of the error message resource.</param>
        public ValidationAttributeBase(string errorMessageResourceName)
            : this()
        {
            this.ErrorMessageResourceName = errorMessageResourceName;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the error message directly or from the resource file if it is specified.
        /// </summary>
        /// <returns>The error message.</returns>
        protected virtual string GetErrorMessage()
        {
            if (this.ErrorMessageResourceType != null)
            {
                if (string.IsNullOrWhiteSpace(this.ErrorMessageResourceName))
                {
                    this.ErrorMessageResourceName = this.ErrorMessage;
                }

                ResourceManager erm = new ResourceManager(this.ErrorMessageResourceType);
                this.ErrorMessage = erm.GetString(this.ErrorMessageResourceName);
            }

            return this.ErrorMessage;
        }

        #endregion
    }
}
