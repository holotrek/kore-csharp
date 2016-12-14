// ***********************************************************************
// <copyright file="IEntity.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using KoreAsp.Domain.Events;

namespace KoreAsp.Domain.Context
{
    /// <summary>
    /// Identifies the state of the entity.
    /// </summary>
    public enum DomainState
    {
        /// <summary>
        /// Entity has not been changed
        /// </summary>
        Unchanged,

        /// <summary>
        /// Entity has been added
        /// </summary>
        Added,

        /// <summary>
        /// Entity has been modified
        /// </summary>
        Modified,

        /// <summary>
        /// Entity has been deleted
        /// </summary>
        Deleted,

        /// <summary>
        /// Entity has been detached or was never attached to the set
        /// </summary>
        Detached
    }

    /// <summary>
    /// Contract for how all entities in the domain contexts should be configured.
    /// </summary>
    public interface IEntity
    {
        #region Properties

        /// <summary>
        /// Gets the collection of domain events raised by this entity.
        /// </summary>
        ICollection<IDomainEvent> Events { get; }

        /// <summary>
        /// Gets the state of the entity.
        /// </summary>
        DomainState EntityState { get; }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        string UniqueId { get; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        /// <value>
        /// The created by user.
        /// </value>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the updated by user.
        /// </summary>
        /// <value>
        /// The updated by user.
        /// </value>
        string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>
        /// The updated date time.
        /// </value>
        DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IEntity" /> is deleted.
        /// </summary>
        bool Deleted { get; }

        #endregion
    }
}
