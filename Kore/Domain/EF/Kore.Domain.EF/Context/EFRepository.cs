// ***********************************************************************
// <copyright file="EFRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Kore.Domain.Context;
using Kore.Providers.Authentication;

namespace Kore.Domain.EF.Context
{
    /// <summary>
    /// A base EF repository that is a database context.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public abstract class EFRepository : DbContext, IRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public EFRepository(DbConnection connection)
            : base(connection, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository"/> class.
        /// </summary>
        protected EFRepository()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public abstract IUser CurrentUser { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        public abstract IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        public abstract IEnumerable<TEntity> GetDeleted<TEntity>()
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Returns the non-deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting non-deleted entity.</returns>
        public abstract TEntity GetByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Returns the deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting deleted entity.</returns>
        public abstract TEntity GetDeletedByUniqueId<TEntity>(string uniqueId)
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public abstract void Add<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public abstract void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public abstract void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Sets the entity stub fields.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public abstract void SetEntityStubFields<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the collection of entities that contain domain events.
        /// </summary>
        /// <returns>The resulting entities.</returns>
        public abstract IEnumerable<IEntity> GetEntitiesWithEvents();

        #endregion
    }

    /// <summary>
    /// A base EF implementation of the <see cref="Kore.Domain.Context.IRepository" />.
    /// </summary>
    /// <typeparam name="TContext">The type of the Entity Framework context.</typeparam>
    /// <seealso cref="Kore.Domain.EF.Context.EFRepository" />
    /// <seealso cref="Kore.Domain.Context.IRepository" />
    public abstract class EFRepository<TContext> : EFRepository, IRepository
        where TContext : DbContext
    {
        #region Private Fields

        /// <summary>
        /// The authentication provider
        /// </summary>
        private IAuthenticationProvider _authenticationProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository{TContext}" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public EFRepository(EFUnitOfWork unitOfWork, ContextConfiguration<TContext> configuration)
            : base(unitOfWork.Connection)
        {
            this.ConfigureContext(configuration);
            unitOfWork.AddRepository(this);
            this._authenticationProvider = unitOfWork.AuthenticationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository{TContext}" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public EFRepository(EFUnitOfWork unitOfWork)
            : this(unitOfWork, new ContextConfiguration<TContext>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository{TContext}"/> class.
        /// </summary>
        protected EFRepository()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the custom configuration.
        /// </summary>
        public virtual ContextConfiguration<TContext> CustomConfiguration { get; private set; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public override IUser CurrentUser
        {
            get
            {
                return this._authenticationProvider.CurrentUser;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Configures the repository using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public abstract void Configure(DbModelBuilder modelBuilder);

        /// <summary>
        /// Returns the query of non-deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of non-deleted entities.</returns>
        public override IEnumerable<TEntity> Get<TEntity>()
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                return this.Set<TEntity>().Where(x => !x.Deleted);
            }
            else
            {
                return this.Set<TEntity>();
            }
        }

        /// <summary>
        /// Returns the query of deleted entities based on the type provided.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The resulting query of deleted entities.</returns>
        public override IEnumerable<TEntity> GetDeleted<TEntity>()
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                return this.Set<TEntity>().Where(x => x.Deleted);
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
        public override TEntity GetByUniqueId<TEntity>(string uniqueId)
        {
            return this.Get<TEntity>().Where(x => x.UniqueId == uniqueId).FirstOrDefault();
        }

        /// <summary>
        /// Returns the deleted entity based on the type provided and a GUID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>The resulting deleted entity.</returns>
        public override TEntity GetDeletedByUniqueId<TEntity>(string uniqueId)
        {
            return this.GetDeleted<TEntity>().Where(x => x.UniqueId == uniqueId).FirstOrDefault();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public override void Add<TEntity>(TEntity entity)
        {
            this.Set<TEntity>().Add(entity);
            this.SetEntityStubFields(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public override void Update<TEntity>(TEntity entity)
        {
            this.Entry<TEntity>(entity).State = EntityState.Modified;
            this.SetEntityStubFields(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public override void Delete<TEntity>(TEntity entity)
        {
            if (this.CustomConfiguration.UseLogicalDeletes)
            {
                this.Update(entity);
            }
            else
            {
                this.Set<TEntity>().Remove(entity);
            }
        }

        /// <summary>
        /// Sets the entity stub fields.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public override void SetEntityStubFields<TEntity>(TEntity entity)
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
        public override IEnumerable<IEntity> GetEntitiesWithEvents()
        {
            return this.ChangeTracker.Entries<IEntity>().Select(po => po.Entity).Where(po => po.Events.Any()).ToList();
        }

        /// <summary>
        /// Configures the entity for use in the EF context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="modelBuilder">The model builder.</param>
        public void ConfigureEntity<TEntity>(DbModelBuilder modelBuilder)
            where TEntity : class, IEntity
        {
            this.ConfigureEntity(modelBuilder.Entity<TEntity>());
        }

        /// <summary>
        /// Configures the entity for use in the EF context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="configuration">The configuration.</param>
        public void ConfigureEntity<TEntity>(EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class, IEntity
        {
            if (this.CustomConfiguration.MapUniqueIdAsPrimaryKey)
            {
                configuration.HasKey(x => x.UniqueId);
            }
            else if (!this.CustomConfiguration.MapUniqueId)
            {
                configuration.Ignore(x => x.UniqueId);
            }

            if (!this.CustomConfiguration.UseLogicalDeletes)
            {
                configuration.Ignore(x => x.Deleted);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Wording is pulled from base class.")]
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.Configure(modelBuilder);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configures the context.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private void ConfigureContext(ContextConfiguration<TContext> configuration)
        {
            this.CustomConfiguration = configuration;
            Database.SetInitializer(configuration.DatabaseInitializationStrategy);
            this.Configuration.AutoDetectChangesEnabled = configuration.AutoDetectChangesEnabled;
            this.Configuration.LazyLoadingEnabled = configuration.LazyLoadingEnabled;
            this.Configuration.ProxyCreationEnabled = configuration.ProxyCreationEnabled || configuration.LazyLoadingEnabled; ////Lazy loading requires proxy creation to be on
        }

        #endregion
    }
}
