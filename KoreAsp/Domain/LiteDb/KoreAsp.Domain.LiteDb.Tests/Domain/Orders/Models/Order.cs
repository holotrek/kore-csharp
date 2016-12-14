// ***********************************************************************
// <copyright file="Order.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.LiteDb.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public class Order : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="product">The product.</param>
        /// <param name="orderDate">The order date.</param>
        public Order(IRepository repository, OrderProduct product, DateTime orderDate)
        {
            this.Product = product;
            this.OrderDate = orderDate;
            this.EntityState = DomainState.Added;
            repository.Add(this);
        }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>The order identifier.</value>
        public string OrderId
        {
            get
            {
                return this.UniqueId;
            }

            set
            {
                this.UniqueId = value;
            }
        }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public OrderProduct Product { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>The order date.</value>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the completion date.
        /// </summary>
        /// <value>The completion date.</value>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void Update(IRepository repository)
        {
            this.EntityState = DomainState.Modified;
            repository.Update(this);
        }

        /// <summary>
        /// Updates the completion date.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="completionDate">The completion date.</param>
        public void UpdateCompletionDate(IRepository repository, DateTime completionDate)
        {
            this.CompletionDate = completionDate;
            this.Update(repository);
        }
    }
}
