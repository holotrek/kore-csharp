// ***********************************************************************
// <copyright file="OrderRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Kore.Domain.Context;
using Kore.Domain.LiteDb.Context;
using Kore.Domain.LiteDb.Tests.Domain.Orders.Models;

namespace Kore.Domain.LiteDb.Tests.Domain.Orders
{
    /// <summary>
    /// The repository for the Product Domain in the Core.LiteDb Testing Suite.
    /// </summary>
    /// <seealso cref="Kore.Domain.LiteDb.Context.EFRepository{Kore.Domain.LiteDb.Tests.Domain.Products.ProductRepository}" />
    /// <seealso cref="Kore.Domain.Context.IRepository" />
    public class OrderRepository : LiteDbRepository, IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public OrderRepository(LiteDbUnitOfWork unitOfWork, RepositoryConfiguration configuration)
            : base(unitOfWork, configuration)
        {
            this.SeedData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OrderRepository(LiteDbUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.SeedData();
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        private void SeedData()
        {
            var categories = new List<OrderCategory>
            {
                new OrderCategory
                {
                    Name = "Soap",
                    Description = "Household Soaps",
                    CreatedBy = "80c00013-61b1-4c41-a4a9-906588ad5261",
                    CreatedDateTime = new DateTime(2016, 10, 2, 9, 57, 53)
                },
                new OrderCategory
                {
                    Name = "Towels",
                    Description = "Cloth Towels",
                    CreatedBy = "b07febfd-6df5-467c-bdff-ae812290608b",
                    CreatedDateTime = new DateTime(2016, 2, 19, 21, 23, 56)
                }
            };

            var products = new List<OrderProduct>
            {
                new OrderProduct
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Ivory",
                    Description = "Ivory Bar Soap",
                    CreatedBy = "80c00013-61b1-4c41-a4a9-906588ad5261",
                    CreatedDateTime = new DateTime(2016, 10, 2, 9, 57, 53)
                },
                new OrderProduct
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Dove",
                    Description = "Dove Bar Soap",
                    CreatedBy = "7510876c-e28c-4776-b86e-2a30707753ad",
                    CreatedDateTime = new DateTime(2016, 11, 1, 12, 32, 12)
                },
                new OrderProduct
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Dove Lotion",
                    Description = "Dove Hand Lotion",
                    CreatedBy = "1924b76e-a08b-4ed6-80f4-47bccb6c2eca",
                    CreatedDateTime = new DateTime(2016, 10, 12, 15, 27, 25)
                },
                new OrderProduct
                {
                    CategoryId = categories[1].CategoryId,
                    Category = categories[1],
                    Name = "Hand Towel",
                    Description = "Hand Towel",
                    CreatedBy = "a7a776a7-9c24-4135-bbd2-137f8123780f",
                    CreatedDateTime = new DateTime(2016, 9, 23, 19, 17, 12)
                },
                new OrderProduct
                {
                    CategoryId = categories[1].CategoryId,
                    Category = categories[1],
                    Name = "Bath Towel",
                    Description = "Bath Towel",
                    CreatedBy = "b07febfd-6df5-467c-bdff-ae812290608b",
                    CreatedDateTime = new DateTime(2016, 2, 19, 21, 23, 56)
                },
            };

            var orders = new List<Order>
            {
                new Order
                {
                    ProductId = products[0].ProductId,
                    Product = products[0],
                    OrderDate = new DateTime(2016, 10, 2),
                    CreatedBy = "80c00013-61b1-4c41-a4a9-906588ad5261",
                    CreatedDateTime = new DateTime(2016, 10, 2, 9, 57, 53)
                },
                new Order
                {
                    ProductId = products[2].ProductId,
                    Product = products[2],
                    OrderDate = new DateTime(2016, 10, 2),
                    CreatedBy = "1924b76e-a08b-4ed6-80f4-47bccb6c2eca",
                    CreatedDateTime = new DateTime(2016, 10, 12, 15, 27, 25)
                },
                new Order
                {
                    ProductId = products[4].ProductId,
                    Product = products[4],
                    OrderDate = new DateTime(2016, 10, 2),
                    CreatedBy = "b07febfd-6df5-467c-bdff-ae812290608b",
                    CreatedDateTime = new DateTime(2016, 2, 19, 21, 23, 56)
                }
            };

            this.SeedData(categories);
            this.SeedData(products);
            this.SeedData(orders);
        }
    }
}
