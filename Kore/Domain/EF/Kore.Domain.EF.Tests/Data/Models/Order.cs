// ***********************************************************************
// <copyright file="Order.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using Kore.Domain.Context;

namespace Kore.Domain.EF.Tests.Data.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public class Order : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        public string OrderId
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
        /// Gets the product identifier.
        /// </summary>
        [Required]
        public string ProductId { get; private set; }

        /// <summary>
        /// Gets the product.
        /// </summary>
        [Required]
        public Product Product { get; private set; }

        /// <summary>
        /// Gets the order date.
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the completion date.
        /// </summary>
        public DateTime? CompletionDate { get; private set; }
    }
}
