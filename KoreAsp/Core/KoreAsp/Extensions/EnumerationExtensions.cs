// ***********************************************************************
// <copyright file="EnumerationExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// Extension methods for Enumeration types.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The enum description (from the corresponding value's annotation).</returns>
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets the enum descriptions for all properties within the specified enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The dictionary of enum values to their corresponding descriptions.</returns>
        public static Dictionary<T, string> GetEnumDescriptions<T>(this T value)
        {
            Dictionary<T, string> desc = new Dictionary<T, string>();
            Type enumType = typeof(T);

            foreach (T e in enumType.GetEnumValues())
            {
                MemberInfo mi = enumType.GetMember(e.ToString()).First();
                DescriptionAttribute da = mi.GetCustomAttribute<DescriptionAttribute>();
                if (da != null)
                {
                    desc.Add(e, da.Description);
                }
                else
                {
                    desc.Add(e, e.ToString());
                }
            }

            return desc;
        }
    }
}
