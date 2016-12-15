// ***********************************************************************
// <copyright file="StringExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Linq;

namespace Kore.Extensions
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the string is within the specified list of values.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="checkValues">The check values.</param>
        /// <returns><c>true</c> if the string is within the specified list of values, <c>false</c> otherwise.</returns>
        public static bool In(this string value, params string[] checkValues)
        {
            return checkValues.Contains(value);
        }
    }
}
