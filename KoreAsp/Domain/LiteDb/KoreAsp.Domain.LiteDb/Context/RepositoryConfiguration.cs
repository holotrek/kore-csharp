// ***********************************************************************
// <copyright file="RepositoryConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.LiteDb.Context
{
    /// <summary>
    /// Configuration class defining EF context options to use for initialization.
    /// </summary>
    public class RepositoryConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether [use logical deletes].
        /// </summary>
        /// <value><c>true</c> if [use logical deletes]; otherwise, <c>false</c>.</value>
        public bool UseLogicalDeletes { get; set; }

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
