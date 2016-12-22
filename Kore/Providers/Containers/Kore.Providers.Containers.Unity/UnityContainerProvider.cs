// ***********************************************************************
// <copyright file="UnityContainerProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Kore.Providers.Containers.Unity
{
    /// <summary>
    /// The Unity container provider.
    /// </summary>
    /// <seealso cref="Kore.Providers.Containers.IContainerProvider" />
    public class UnityContainerProvider : IContainerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerProvider" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityContainerProvider(IUnityContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// Resolves the specified type and name.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="name">The name.</param>
        /// <returns>The resolved object of the specified type identified by the name.</returns>
        public object Resolve(Type t, string name)
        {
            return this.Container.Resolve(t, name);
        }

        /// <summary>
        /// Resolves the specified name for the generic type.
        /// </summary>
        /// <typeparam name="T">The type of the object to resolve.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The resolved object of the specified type identified by the name.</returns>
        public T Resolve<T>(string name)
            where T : class
        {
            return this.Container.Resolve<T>(name);
        }

        /// <summary>
        /// Resolves the specified type and returns all matching instances.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The collection of resolved objects of the specified type.</returns>
        public IEnumerable<object> ResolveAll(Type t)
        {
            return this.Container.ResolveAll(t);
        }

        /// <summary>
        /// Resolves the generic type and returns all matching instances.
        /// </summary>
        /// <typeparam name="T">The type of the object to resolve.</typeparam>
        /// <returns>The collection of resolved objects of the specified type.</returns>
        public IEnumerable<T> ResolveAll<T>()
            where T : class
        {
            return this.Container.ResolveAll<T>();
        }
    }
}
