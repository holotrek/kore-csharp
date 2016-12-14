// ***********************************************************************
// <copyright file="IContainerProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace KoreAsp.Providers.Containers
{
    /// <summary>
    /// Contract for common functionality shared by IoC Containers
    /// </summary>
    public interface IContainerProvider
    {
        /// <summary>
        /// Resolves the specified type and name.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="name">The name.</param>
        /// <returns>The resolved object of the specified type identified by the name.</returns>
        object Resolve(Type t, string name);

        /// <summary>
        /// Resolves the specified name for the generic type.
        /// </summary>
        /// <typeparam name="T">The type of the object to resolve.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The resolved object of the specified type identified by the name.</returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Resolves the specified type and returns all matching instances.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The collection of resolved objects of the specified type.</returns>
        IEnumerable<object> ResolveAll(Type t);

        /// <summary>
        /// Resolves the generic type and returns all matching instances.
        /// </summary>
        /// <typeparam name="T">The type of the object to resolve.</typeparam>
        /// <returns>The collection of resolved objects of the specified type.</returns>
        IEnumerable<T> ResolveAll<T>();
    }
}
