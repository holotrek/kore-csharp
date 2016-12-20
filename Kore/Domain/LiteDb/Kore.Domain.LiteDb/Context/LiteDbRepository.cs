// ***********************************************************************
// <copyright file="LiteDbRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Kore.Domain.Context;
using Kore.Providers.Authentication;
using LiteDB;

namespace Kore.Domain.LiteDb.Context
{
    /// <summary>
    /// A base No-SQL implementation of the <see cref="Kore.Domain.Context.IRepository" />.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.IRepository" />
    public abstract class LiteDbRepository : IRepository
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
        /// Initializes a new instance of the <see cref="LiteDbRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public LiteDbRepository(LiteDbUnitOfWork unitOfWork, RepositoryConfiguration configuration)
            : this()
        {
            this.UnitOfWork = unitOfWork;
            this.UnitOfWork.AddRepository(this);
            this.Connection = unitOfWork.Connection;
            this.CustomConfiguration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public LiteDbRepository(LiteDbUnitOfWork unitOfWork)
            : this(unitOfWork, new RepositoryConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbRepository"/> class.
        /// </summary>
        protected LiteDbRepository()
        {
            this._trackedEntities = new List<IEntity>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public virtual LiteDatabase Connection { get; private set; }

        /// <summary>
        /// Gets the custom configuration.
        /// </summary>
        public virtual RepositoryConfiguration CustomConfiguration { get; private set; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        public virtual IUnitOfWork UnitOfWork { get; private set; }

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

        #region Public Methods

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        public virtual IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class, IEntity, new()
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                this.GetSet<TEntity>().EnsureIndex(x => x.Deleted);
                return this.GetSet<TEntity>().Find(x => !x.Deleted);
            }
            else
            {
                return this.GetSet<TEntity>().FindAll();
            }
        }

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        public IEnumerable<TEntity> GetDeleted<TEntity>()
            where TEntity : class, IEntity, new()
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                this.GetSet<TEntity>().EnsureIndex(x => x.Deleted);
                return this.GetSet<TEntity>().Find(x => x.Deleted);
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
            this.SetEntityStubFields(entity);
            this.GetSet<TEntity>().Insert(entity);
            this._trackedEntities.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            this.SetEntityStubFields(entity);
            this.GetSet<TEntity>().Update(entity);
            this._trackedEntities.Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new()
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                this.Update(entity);
            }
            else
            {
                this.GetSet<TEntity>().Delete(x => x.UniqueId == entity.UniqueId);
                this._trackedEntities.Add(entity);
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
            return this._trackedEntities.Where(po => po.Events.Any()).ToList();
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
                foreach (string colName in this.Connection.GetCollectionNames())
                {
                    this.Connection.DropCollection(colName);
                }

                this._disposed = true;
            }
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="data">The data.</param>
        protected void SeedData<TEntity>(IEnumerable<TEntity> data)
            where TEntity : class, IEntity, new()
        {
            string entName = typeof(TEntity).Name;
            if (this.Connection.CollectionExists(entName))
            {
                this.Connection.DropCollection(entName);
            }

            this.Connection.GetCollection<TEntity>(entName).Insert(data);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the LiteDB entity set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The LiteDB entity set.</returns>
        private LiteCollection<TEntity> GetSet<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return this.Connection.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        #endregion
    }
}
