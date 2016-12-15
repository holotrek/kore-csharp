// ***********************************************************************
// <copyright file="LambdaEqualityComparer.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Kore.Comparers
{
    /// <summary>
    /// Allows for comparison of two items by their properties using the lambda syntax.
    /// </summary>
    /// <typeparam name="T">The type of the items being compared.</typeparam>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{T}" />
    public class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// The expression
        /// </summary>
        private readonly Func<T, T, bool> _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public LambdaEqualityComparer(Func<T, T, bool> expression)
        {
            this._expression = expression;
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(T x, T y)
        {
            return this._expression(x, y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(T obj)
        {
            // Return 0 to force it to use Equals
            return 0;
        }
    }
}
