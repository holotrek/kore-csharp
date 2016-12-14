// ***********************************************************************
// <copyright file="MemoryCachingProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Runtime.Caching;

namespace KoreAsp.Providers.Caching.Memory
{
    /// <summary>
    /// A lightweight implementation of a cache provider that caches to memory.
    /// </summary>
    /// <seealso cref="KoreAsp.Providers.Caching.ICachingProvider" />
    public class MemoryCachingProvider : ICachingProvider
    {
        /// <summary>
        /// The default expiration
        /// </summary>
        private TimeSpan _defaultExpiration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCachingProvider" /> class.
        /// </summary>
        /// <param name="defaultExpiration">The default expiration.</param>
        public MemoryCachingProvider(TimeSpan defaultExpiration)
        {
            this._defaultExpiration = defaultExpiration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCachingProvider" /> class.
        /// </summary>
        public MemoryCachingProvider()
            : this(new TimeSpan(1, 0, 0, 0))
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public virtual object this[string key]
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
        public virtual void Set(string key, object value)
        {
            this.Set(key, value, this._defaultExpiration);
        }

        /// <summary>
        /// Adds/overwrites the specified value to the cache referenced by the specified key.
        /// Uses the specified expiration to remove the value from the cache if not accessed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The amount of time it will remain in the cache before expiration.</param>
        public virtual void Set(string key, object value, TimeSpan expiration)
        {
            if (value == null)
            {
                MemoryCache.Default.Set(key, "null", DateTimeOffset.Now.Add(expiration));
            }
            else
            {
                MemoryCache.Default.Set(key, value, DateTimeOffset.Now.Add(expiration));
            }
        }

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value or null if it does not exist.</returns>
        public virtual object Get(string key)
        {
            object o = MemoryCache.Default[key];
            if (o == null || o.ToString() == "null")
            {
                return null;
            }
            else
            {
                return o;
            }
        }

        /// <summary>
        /// Gets the value from the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">Type of object expected.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value or null if it does not exist or is not the expected type.</returns>
        public virtual T Get<T>(string key)
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
    }
}
