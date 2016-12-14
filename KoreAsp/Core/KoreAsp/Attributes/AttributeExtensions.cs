// ***********************************************************************
// <copyright file="AttributeExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using KoreAsp.Attributes.Custom;
using KoreAsp.Extensions;

namespace KoreAsp.Attributes
{
    /// <summary>
    /// Extensions for working with attributes and getting all the values or specific values.
    /// </summary>
    public static class AttributeExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the attributes of all the properties within the specified model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>The collection of attributes.</returns>
        public static Dictionary<string, AttributeModel> GetAttributes<T>(this T model)
        {
            return typeof(T).GetAttributes();
        }

        /// <summary>
        /// Gets the attributes of all the properties within the specified model.
        /// </summary>
        /// <param name="type">The type of the model.</param>
        /// <returns>The collection of attributes.</returns>
        public static Dictionary<string, AttributeModel> GetAttributes(this Type type)
        {
            PropertyInfo[] pinfos = type.GetProperties();
            Dictionary<string, AttributeModel> attributes = new Dictionary<string, AttributeModel>();
            string primaryKeyName = type.GetPrimaryKeyName();
            foreach (PropertyInfo pi in pinfos)
            {
                if (pi != null)
                {
                    AttributeModel attr = new AttributeModel
                    {
                        PropertyName = pi.Name,
                        PropertyType = pi.PropertyType,
                        PrimaryKeyName = primaryKeyName,
                        DefaultValue = pi.GetDefaultValue()
                    };

                    attr.ApplyDisplayAttributes(pi);
                    attr.ApplyDefaultAttributes(pi);
                    attr.ApplyValidationAttributes(pi);
                    attributes.Add(attr.PropertyName, attr);
                }
            }

            return attributes;
        }

        /// <summary>
        /// Will get the default value of a property, uses the DefaultValueAttribute if it exists
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The default value.</returns>
        public static object GetDefaultValue(this PropertyInfo propertyInfo)
        {
            if (propertyInfo != null)
            {
                DefaultValueAttribute defaultAttribute = propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                return defaultAttribute?.DefaultValue ?? propertyInfo.PropertyType.GetDefaultTypeValue();
            }

            return null;
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <typeparam name="T">The type of the model</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The default value.</returns>
        public static object GetDefaultValue<T>(this T model, string propertyName)
        {
            Type t = typeof(T);
            PropertyInfo pi = t.GetProperty(propertyName);
            if (pi != null)
            {
                DefaultValueAttribute def = (DefaultValueAttribute)pi.GetCustomAttribute(typeof(DefaultValueAttribute));
                if (def != null)
                {
                    return def.DefaultValue;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <typeparam name="T">The type of the model</typeparam>
        /// <typeparam name="P">The type of the property</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="exp">The expression to get the property.</param>
        /// <returns>The default value.</returns>
        public static P GetDefaultValue<T, P>(this T model, Expression<Func<T, P>> exp)
        {
            if (exp.Body is MemberExpression)
            {
                string propertyName = ((MemberExpression)exp.Body).Member.Name;
                return (P)model.GetDefaultValue(propertyName);
            }

            return default(P);
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Gets the name of the property that is the primary key for the model.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The primary key property name.</returns>
        private static string GetPrimaryKeyName(this Type type)
        {
            PropertyInfo[] infos = type.GetProperties();
            foreach (PropertyInfo pi in infos)
            {
                KeyAttribute k = (KeyAttribute)pi.GetCustomAttribute(typeof(KeyAttribute));
                if (k != null)
                {
                    return pi.Name;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Applies the display attributes.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="pi">The property info.</param>
        private static void ApplyDisplayAttributes(this AttributeModel attr, PropertyInfo pi)
        {
            DisplayAttribute disp = (DisplayAttribute)pi.GetCustomAttribute(typeof(DisplayAttribute));
            attr.DisplayName = disp == null ? pi.Name : disp.GetName();
            attr.DisplayGroupName = disp?.GetGroupName();
            attr.ShortName = disp?.GetShortName();
            attr.Prompt = disp?.GetPrompt();
        }

        /// <summary>
        /// Applies the default attributes.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="pi">The property info.</param>
        private static void ApplyDefaultAttributes(this AttributeModel attr, PropertyInfo pi)
        {
            DefaultValueAttribute def = (DefaultValueAttribute)pi.GetCustomAttribute(typeof(DefaultValueAttribute));
            if (def != null)
            {
                attr.DefaultValue = def.DefaultValue;
            }
        }

        /// <summary>
        /// Applies the validation attributes.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="pi">The property info.</param>
        private static void ApplyValidationAttributes(this AttributeModel attr, PropertyInfo pi)
        {
            attr.Validations = new Dictionary<string, ValidationAttribute>();
            Type modelType = typeof(AttributeModel);
            IEnumerable<Attribute> attrs = pi.GetCustomAttributes();
            foreach (Attribute a in attrs)
            {
                if (a is ValidationAttribute)
                {
                    ValidationAttribute va = (ValidationAttribute)a;
                    va.ErrorMessage = va.FormatErrorMessage(attr.DisplayName);
                    Type attributeType = a.GetType();
                    string valName = attributeType.Name.Replace("Attribute", string.Empty);
                    attr.Validations[valName] = va;
                }
            }
        }

        #endregion
    }
}
