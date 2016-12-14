// ***********************************************************************
// <copyright file="OrderRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity;
using KoreAsp.Domain.Context;
using KoreAsp.Domain.EF.Context;
using KoreAsp.Domain.EF.Tests.Domain.Orders.Models;

namespace KoreAsp.Domain.EF.Tests.Domain.Orders
{
    /// <summary>
    /// The repository for the Product Domain in the Core.EF Testing Suite.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.EF.Context.EFRepository{KoreAsp.Domain.EF.Tests.Domain.Products.ProductRepository}" />
    /// <seealso cref="KoreAsp.Domain.Context.IRepository" />
    public class OrderRepository : EFRepository<OrderRepository>, IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public OrderRepository(EFUnitOfWork unitOfWork, ContextConfiguration<OrderRepository> configuration)
            : base(unitOfWork, configuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OrderRepository(EFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Configures the repository using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public override void Configure(DbModelBuilder modelBuilder)
        {
            this.ConfigureEntity<Order>(modelBuilder);
            modelBuilder.Configurations.Add(new OrderCategoryConfiguration(this));
            modelBuilder.Configurations.Add(new OrderProductConfiguration(this));
        }
    }
}
