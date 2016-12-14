// ***********************************************************************
// <copyright file="LinqExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using KoreAsp.Comparers;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// Extension methods for added functionality over existing <see cref="System.Linq" /> methods.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Excepts the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the entities contained in the sets.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The target collection.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>The excepted set.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> expression)
        {
            return first.Except(second, new LambdaEqualityComparer<T>(expression));
        }

        /// <summary>
        /// Intersects the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the entities contained in the sets.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The target collection.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>The intersected set.</returns>
        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> expression)
        {
            return first.Intersect(second, new LambdaEqualityComparer<T>(expression));
        }

        /// <summary>
        /// Unions the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the entities contained in the sets.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The target collection.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>The resulting set.</returns>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> expression)
        {
            return first.Union(second, new LambdaEqualityComparer<T>(expression));
        }

        /// <summary>
        /// Runs distinct on the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the entities contained in the sets.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>The distinct set.</returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> expression)
        {
            return source.Distinct(new LambdaEqualityComparer<T>(expression));
        }
    }
}
