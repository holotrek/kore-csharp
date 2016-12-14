// ***********************************************************************
// <copyright file="TypeExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// Provides common extension methods for the Type class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the default value of the type.
        /// </summary>
        /// <param name="t">The type.</param>
        /// <returns>The object with the default value of the type.</returns>
        public static object GetDefaultTypeValue(this Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }
    }
}
