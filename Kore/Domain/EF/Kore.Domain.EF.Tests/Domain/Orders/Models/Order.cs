// ***********************************************************************
// <copyright file="Order.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using Kore.Domain.Context;

namespace Kore.Domain.EF.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
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
        public string ProductId { get; private set; }

        /// <summary>
        /// Gets the product.
        /// </summary>
        public OrderProduct Product { get; private set; }

        /// <summary>
        /// Gets the order date.
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the completion date.
        /// </summary>
        public DateTime? CompletionDate { get; private set; }

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
