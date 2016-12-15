// ***********************************************************************
// <copyright file="ICachingProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace Kore.Providers.Caching
{
    /// <summary>
    /// Contract for storing and retrieving values from cache that is not dependent on a particular library.
    /// </summary>
    public interface ICachingProvider
    {
        /// <summary>
        /// Gets or sets the <see cref="System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Adds/overwrites the specified value to the cache referenced by the specified key.
        /// Uses the default expiration to remove the value from the cache if not accessed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, object value);

        /// <summary>
        /// Adds/overwrites the specified value to the cache referenced by the specified key.
        /// Uses the specified expiration to remove the value from the cache if not accessed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The amount of time it will remain in the cache before expiration.</param>
        void Set(string key, object value, TimeSpan expiration);

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value or null if it does not exist.</returns>
        object Get(string key);

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">Type of object expected.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        T Get<T>(string key);
    }
}
