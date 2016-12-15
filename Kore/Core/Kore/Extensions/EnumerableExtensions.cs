// ***********************************************************************
// <copyright file="EnumerableExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;

namespace Kore.Extensions
{
    /// <summary>
    /// Extension methods for Enumeration types.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts a single item to an enumerable of that item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>Enumerable of the single item.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}
