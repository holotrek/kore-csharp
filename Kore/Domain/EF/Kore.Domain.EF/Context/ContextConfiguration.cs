// ***********************************************************************
// <copyright file="ContextConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity;
using Kore.Domain.Context;

namespace Kore.Domain.EF.Context
{
    /// <summary>
    /// Configuration class defining EF context options to use for initialization.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class ContextConfiguration<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Gets or sets the database initialization strategy.
        /// </summary>
        /// <value>The database initialization strategy.</value>
        public IDatabaseInitializer<TContext> DatabaseInitializationStrategy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use logical deletes].
        /// </summary>
        /// <value><c>true</c> if [use logical deletes]; otherwise, <c>false</c>.</value>
        public bool UseLogicalDeletes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic detect changes enabled].
        /// </summary>
        /// <value><c>true</c> if [automatic detect changes enabled]; otherwise, <c>false</c>.</value>
        public bool AutoDetectChangesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lazy loading enabled].
        /// </summary>
        /// <value><c>true</c> if [lazy loading enabled]; otherwise, <c>false</c>.</value>
        public bool LazyLoadingEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [proxy creation enabled].
        /// </summary>
        /// <value><c>true</c> if [proxy creation enabled]; otherwise, <c>false</c>.</value>
        public bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to map the <see cref="Kore.Domain.Context.IEntity"/> string unique ID property.
        /// </summary>
        /// <value><c>true</c> if map unique ID property; otherwise, <c>false</c>.</value>
        public bool MapUniqueId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to map the Unique ID as the primary key.
        /// </summary>
        /// <value><c>true</c> if mapping the Unique ID as the primary key; otherwise, <c>false</c>.</value>
        public bool MapUniqueIdAsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the stub mode.
        /// </summary>
        /// <value>The stub mode.</value>
        public RecordStubMode StubMode { get; set; }
    }
}
