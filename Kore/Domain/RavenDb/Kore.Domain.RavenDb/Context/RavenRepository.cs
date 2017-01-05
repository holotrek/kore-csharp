// ***********************************************************************
// <copyright file="RavenRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Kore.Domain.Context;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;
using Raven.Client;

namespace Kore.Domain.RavenDb.Context
{
    /// <summary>
    /// An implementation of the Core Repository pattern for Raven DB.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.IRepository" />
    public class RavenRepository : IRepository
    {
        #region Private Fields

        /// <summary>
        /// The tracked entities
        /// </summary>
        private List<IEntity> _trackedEntities = new List<IEntity>();

        /// <summary>
        /// Whether the unit of work has been disposed.
        /// </summary>
        private bool _disposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="messageProvider">The message provider.</param>
        public RavenRepository(RavenUnitOfWork unitOfWork, RepositoryConfiguration configuration, IMessageProvider messageProvider)
            : this()
        {
            this.UnitOfWork = unitOfWork;
            this.UnitOfWork.AddRepository(this);
            this.DocumentSession = unitOfWork.DocumentSession;
            this.CustomConfiguration = configuration;
            this.MessageProvider = messageProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public RavenRepository(RavenUnitOfWork unitOfWork, RepositoryConfiguration configuration)
            : this(unitOfWork, configuration, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="messageProvider">The message provider.</param>
        public RavenRepository(RavenUnitOfWork unitOfWork, IMessageProvider messageProvider)
            : this(unitOfWork, new RepositoryConfiguration(), messageProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public RavenRepository(RavenUnitOfWork unitOfWork)
            : this(unitOfWork, new RepositoryConfiguration(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenRepository" /> class.
        /// </summary>
        protected RavenRepository()
        {
            this._trackedEntities = new List<IEntity>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the document session.
        /// </summary>
        public virtual IDocumentSession DocumentSession { get; private set; }

        /// <summary>
        /// Gets the custom configuration.
        /// </summary>
        public virtual RepositoryConfiguration CustomConfiguration { get; private set; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        public virtual IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the message provider.
        /// </summary>
        public virtual IMessageProvider MessageProvider { get; private set; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public virtual IUser CurrentUser
        {
            get
            {
                return this.UnitOfWork.AuthenticationProvider.CurrentUser;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        public virtual IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class, IEntity
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                return this.DocumentSession.Query<TEntity>().Where(x => !x.Deleted);
            }
            else
            {
                return this.DocumentSession.Query<TEntity>();
            }
        }

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        public virtual IEnumerable<TEntity> GetDeleted<TEntity>()
            where TEntity : class, IEntity
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                return this.DocumentSession.Query<TEntity>();
            }
            else
            {
                throw new MethodAccessException("GetDeleted method not supported unless UseLogicalDeletes is true.");
            }
        }

        /// <summary>
        /// Returns the non-deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting non-deleted entity.</returns>
        public virtual TEntity GetByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity
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
            where TEntity : class, IEntity
        {
            return this.GetDeleted<TEntity>().Where(x => x.UniqueId == uniqueId).FirstOrDefault();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Add<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            this.SetEntityStubFields(entity);
            this.DocumentSession.Store(entity, entity.UniqueId);
            this._trackedEntities.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            // Raven uses "UPSERT" type of method "Store", so there's no concept of add vs update.
            this.SetEntityStubFields(entity);
            this.Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                this.Update(entity);
            }
            else
            {
                this.DocumentSession.Delete(entity.UniqueId);
            }
            
            this._trackedEntities.Add(entity);
        }

        /// <summary>
        /// Deletes all entities of a specific type. Does not use logical deletes. Should be used only for seeding data.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        public virtual void Truncate<TEntity>()
            where TEntity : class, IEntity
        {
            foreach (TEntity entity in this.Get<TEntity>().ToList())
            {
                this.DocumentSession.Delete(entity);
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
            string user = this.CurrentUser.GetRecordIdentifier(this.CustomConfiguration.StubMode);
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
            return this._trackedEntities.Where(e => e.Events.Any()).ToList();
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
                this._disposed = true;
            }
        }

        #endregion
    }
}
