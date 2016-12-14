// ***********************************************************************
// <copyright file="MockCachingProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace KoreAsp.Providers.Caching
{
    /// <summary>
    /// A lightweight implementation of a cache provider that caches to memory.
    /// </summary>
    /// <seealso cref="KoreAsp.Providers.Caching.ICachingProvider" />
    public class MockCachingProvider : ICachingProvider
    {
        /// <summary>
        /// The fake cache
        /// </summary>
        private Dictionary<string, object> fakeCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockCachingProvider" /> class.
        /// </summary>
        public MockCachingProvider()
        {
            this.fakeCache = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public object this[string key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.Set(key, value);
            }
        }

        /// <summary>
        /// Adds/overwrites the specified value to the cache referenced by the specified key.
        /// Uses the default expiration to remove the value from the cache if not accessed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(string key, object value)
        {
            this.fakeCache[key] = value;
        }

        /// <summary>
        /// Adds/overwrites the specified value to the cache referenced by the specified key.
        /// Uses the specified expiration to remove the value from the cache if not accessed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The amount of time it will remain in the cache before expiration.</param>
        public void Set(string key, object value, TimeSpan expiration)
        {
            this.fakeCache[key] = value;
        }

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value or null if it does not exist.</returns>
        public object Get(string key)
        {
            return this.fakeCache[key];
        }

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">Type of object expected.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value or null if it does not exist or is not the expected type.</returns>
        public T Get<T>(string key)
        {
            object o = this.Get(key);
            if (o == null)
            {
                return default(T);
            }

            if (o.GetType() == typeof(T))
            {
                return (T)o;
            }

            return default(T);
        }

        /// <summary>
        /// Forces the expiration.
        /// </summary>
        /// <param name="key">The key.</param>
        public void ForceExpiration(string key)
        {
            this.fakeCache[key] = null;
        }
    }
}
