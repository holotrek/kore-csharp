// ***********************************************************************
// <copyright file="AttributeModel.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kore.Attributes
{
    /// <summary>
    /// A model containing all the attributes of a specific property within a model.
    /// </summary>
    public class AttributeModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the primary key of the model that this property is contained in.
        /// </summary>
        /// <value>The name of the primary key.</value>
        public string PrimaryKeyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public Type PropertyType { get; set; }

        /// <summary>
        /// Gets the short name of the type of the property.
        /// </summary>
        /// <value>The name of the property type.</value>
        public string PropertyTypeName
        {
            get
            {
                Type effectiveType = this.PropertyType;
                Type underlyingType = Nullable.GetUnderlyingType(this.PropertyType);
                if (underlyingType != null)
                {
                    effectiveType = underlyingType;
                }
                
                switch (Type.GetTypeCode(effectiveType))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        return "int";
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return "decimal";
                    case TypeCode.DateTime:
                        return "datetime";
                    case TypeCode.Boolean:
                        return "bool";
                    default:
                        return "string";
                }
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the group.
        /// </summary>
        /// <value>The display name of the group.</value>
        public string DisplayGroupName { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the prompt.
        /// </summary>
        /// <value>The prompt.</value>
        public string Prompt { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the validations.
        /// </summary>
        /// <value>The validations.</value>
        public Dictionary<string, ValidationAttribute> Validations { get; set; }

        #endregion
    }
}