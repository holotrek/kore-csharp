// ***********************************************************************
// <copyright file="MockRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Kore.Domain.Context;
using Kore.Providers.Authentication;
using Kore.Providers.Serialization;

namespace Kore.Domain.Tests
{
    /// <summary>
    /// An implementation of the Repository that can be used in tests.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.IRepository" />
    public class MockRepository : IRepository
    {
        #region Private Fields

        /// <summary>
        /// Whether the repository has been disposed.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// The initial serialized data before changes are made
        /// </summary>
        private string _initialData;

        /// <summary>
        /// The data
        /// </summary>
        private List<IEntity> _data;

        /// <summary>
        /// The serialization provider
        /// </summary>
        private ISerializationProvider _serializationProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MockRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        /// <param name="seedData">The seed data.</param>
        public MockRepository(MockUnitOfWork unitOfWork, ISerializationProvider serializationProvider, IEnumerable<IEntity> seedData)
        {
            this.UnitOfWork = unitOfWork;
            this.UnitOfWork.AddRepository(this);
            this._data = seedData.ToList();
            this._initialData = serializationProvider.SerializeObject(seedData);
            this._serializationProvider = serializationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        public MockRepository(MockUnitOfWork unitOfWork, ISerializationProvider serializationProvider)
            : this(unitOfWork, serializationProvider, new List<IEntity>())
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MockRepository"/> class from being created.
        /// </summary>
        private MockRepository()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public IUser CurrentUser
        {
            get
            {
                return this.UnitOfWork.AuthenticationProvider.CurrentUser;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        public virtual IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return this.GetCollection<TEntity>().Where(x => !x.Deleted);
        }

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        public virtual IEnumerable<TEntity> GetDeleted<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return this.GetCollection<TEntity>().Where(x => x.Deleted);
        }

        /// <summary>
        /// Returns the non-deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting non-deleted entity.</returns>
        public virtual TEntity GetByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity, new()
        {
            return this.Get<TEntity>().Where(x => x.UniqueId == uniqueId).FirstOrDefault();
        }

        /// <summary>
        /// Returns the deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting deleted entity.</returns>
        public virtual TEntity GetDeletedByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity, new()
        {
            return this.GetDeleted<TEntity>().Where(x => x.UniqueId == uniqueId).FirstOrDefault();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Add<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            this.SetEntityStubFields<TEntity>(entity);
            if (!this._data.Where(x => x.UniqueId == entity.UniqueId).Any())
            {
                this._data.Add(entity);
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            this.SetEntityStubFields<TEntity>(entity);
            IEntity foundEntity = this._data.Where(x => x.UniqueId == entity.UniqueId).FirstOrDefault();
            if (foundEntity != null)
            {
                this._data.Remove(foundEntity);
                this._data.Add(entity);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            // Using logical deletes, so just update it
            if (entity.Deleted)
            {
                this.Update(entity);
            }
        }

        /// <summary>
        /// Sets the entity stub fields.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void SetEntityStubFields<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            DateTime now = DateTime.Now;
            string user = this.CurrentUser.UniqueId;
            if (entity.EntityState == DomainState.Modified || entity.EntityState == DomainState.Deleted)
            {
                entity.UpdatedBy = user;
                entity.UpdatedDateTime = now;
            }
            else if (entity.EntityState == DomainState.Added)
            {
                entity.CreatedBy = user;
                entity.CreatedDateTime = now;
            }
        }

        /// <summary>
        /// Returns the collection of entities that contain domain events.
        /// </summary>
        /// <returns>The resulting entities.</returns>
        public virtual IEnumerable<IEntity> GetEntitiesWithEvents()
        {
            return this._data.Where(x => x.Events.Any());
        }

        /// <summary>
        /// Writes the changes to the JSON "database".
        /// </summary>
        public void WriteChanges()
        {
            this._initialData = this._serializationProvider.SerializeObject(this._data);
        }

        /// <summary>
        /// Pulls the initial data in from the JSON "database" and removes any changes made.
        /// </summary>
        public void DiscardChanges()
        {
            this._data = this._serializationProvider.DeserializeObject<List<IEntity>>(this._initialData);
        }

        /// <summary>
        /// Disposes the unit of work and all corresponding repositories.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                this.DiscardChanges();
                this._disposed = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The collection of entities.</returns>
        private List<TEntity> GetCollection<TEntity>()
        {
            return this._data.OfType<TEntity>().ToList();
        }

        #endregion
    }
}
