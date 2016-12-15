// ***********************************************************************
// <copyright file="OrderCategory.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;

namespace Kore.Domain.EF.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public class OrderCategory : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCategory"/> class.
        /// </summary>
        public OrderCategory()
        {
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public string CategoryId
        {
            get
            {
                return this.UniqueId;
            }

            private set
            {
                this.UniqueId = value;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }
    }
}
