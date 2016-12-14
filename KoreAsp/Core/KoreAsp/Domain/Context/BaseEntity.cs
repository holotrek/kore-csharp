// ***********************************************************************
// <copyright file="BaseEntity.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KoreAsp.Domain.Events;

namespace KoreAsp.Domain.Context
{
    /// <summary>
    /// A base implementation of the entity object that provides some default functionality for the <see cref="KoreAsp.Domain.Context.IEntity"/>.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public abstract class BaseEntity : IEntity, IEquatable<BaseEntity>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            this.Events = new List<IDomainEvent>();
            this.UniqueId = Guid.NewGuid().ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the collection of domain events raised by this entity.
        /// </summary>
        [NotMapped]
        public virtual ICollection<IDomainEvent> Events { get; private set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [NotMapped]
        public virtual DomainState EntityState { get; protected set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public virtual string UniqueId { get; protected set; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        /// <value>
        /// The created by user.
        /// </value>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public virtual DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the updated by user.
        /// </summary>
        /// <value>
        /// The updated by user.
        /// </value>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>
        /// The updated date time.
        /// </value>
        public virtual DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEntity" /> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Deleted
        {
            get
            {
                return this.EntityState == DomainState.Deleted;
            }

            protected set
            {
                if (value)
                {
                    this.EntityState = DomainState.Deleted;
                }
                else if (this.EntityState == DomainState.Deleted)
                {
                    this.EntityState = DomainState.Modified;
                }
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        #endregion

        #region Public Methods
    
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as BaseEntity);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
            {
                return false;
            }

            return this.UniqueId == other.UniqueId;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.UniqueId.GetHashCode();
        }

        #endregion
    }
}
