// ***********************************************************************
// <copyright file="OrderProduct.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.EF.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Product model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public class OrderProduct : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProduct"/> class.
        /// </summary>
        public OrderProduct()
        {
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public string ProductId
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
        /// Gets the category identifier.
        /// </summary>
        public string CategoryId { get; private set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        public virtual OrderCategory Category { get; private set; }

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
