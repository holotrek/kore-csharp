// ***********************************************************************
// <copyright file="IRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Kore.Providers.Authentication;

namespace Kore.Domain.Context
{
    /// <summary>
    /// A contract defining a generic repository that will contain methods to interact with an underlying ORM.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the current user.
        /// </summary>
        IUser CurrentUser { get; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        IEnumerable<TEntity> GetDeleted<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the non-deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting non-deleted entity.</returns>
        TEntity GetByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting deleted entity.</returns>
        TEntity GetDeletedByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity;

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Sets the entity stub fields.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void SetEntityStubFields<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the collection of entities that contain domain events.
        /// </summary>
        /// <returns>The resulting entities.</returns>
        IEnumerable<IEntity> GetEntitiesWithEvents();

        #endregion
    }

    /// <summary>
    /// Determines how to mark the record's stub fields
    /// </summary>
    public enum RecordStubMode
    {
        /// <summary>
        /// Mark record by Unique ID
        /// </summary>
        UniqueId = 0,

        /// <summary>
        /// Mark record by User Name
        /// </summary>
        UserName = 1,

        /// <summary>
        /// Mark record in the format: LastName, FirstName (UserName)
        /// </summary>
        LastFirstUserName = 2
    }
}
