// ***********************************************************************
// <copyright file="ProductRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using KoreAsp.Domain.Context;
using KoreAsp.Domain.LiteDb.Context;
using KoreAsp.Domain.LiteDb.Tests.Domain.Products.Models;

namespace KoreAsp.Domain.LiteDb.Tests.Domain.Products
{
    /// <summary>
    /// The repository for the Product Domain in the Core.EF Testing Suite.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.LiteDb.Context.EFRepository{KoreAsp.Domain.LiteDb.Tests.Domain.Products.ProductRepository}" />
    /// <seealso cref="KoreAsp.Domain.Context.IRepository" />
    public class ProductRepository : LiteDbRepository, IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public ProductRepository(LiteDbUnitOfWork unitOfWork, RepositoryConfiguration configuration)
            : base(unitOfWork, configuration)
        {
            this.SeedData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProductRepository(LiteDbUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.SeedData();
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        private void SeedData()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Soap",
                    Description = "Household Soaps",
                    DisplayOrder = 1,
                    CreatedBy = "80c00013-61b1-4c41-a4a9-906588ad5261",
                    CreatedDateTime = new DateTime(2016, 10, 2, 9, 57, 53)
                },
                new Category
                {
                    Name = "Towels",
                    Description = "Cloth Towels",
                    DisplayOrder = 2,
                    CreatedBy = "b07febfd-6df5-467c-bdff-ae812290608b",
                    CreatedDateTime = new DateTime(2016, 2, 19, 21, 23, 56)
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Ivory",
                    Description = "Ivory Bar Soap",
                    DisplayOrder = 1,
                    CreatedBy = "80c00013-61b1-4c41-a4a9-906588ad5261",
                    CreatedDateTime = new DateTime(2016, 10, 2, 9, 57, 53)
                },
                new Product
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Dove",
                    Description = "Dove Bar Soap",
                    DisplayOrder = 2,
                    CreatedBy = "7510876c-e28c-4776-b86e-2a30707753ad",
                    CreatedDateTime = new DateTime(2016, 11, 1, 12, 32, 12)
                },
                new Product
                {
                    CategoryId = categories[0].CategoryId,
                    Category = categories[0],
                    Name = "Dove Lotion",
                    Description = "Dove Hand Lotion",
                    DisplayOrder = 3,
                    CreatedBy = "1924b76e-a08b-4ed6-80f4-47bccb6c2eca",
                    CreatedDateTime = new DateTime(2016, 10, 12, 15, 27, 25)
                },
                new Product
                {
                    CategoryId = categories[1].CategoryId,
                    Category = categories[1],
                    Name = "Hand Towel",
                    Description = "Hand Towel",
                    DisplayOrder = 4,
                    CreatedBy = "a7a776a7-9c24-4135-bbd2-137f8123780f",
                    CreatedDateTime = new DateTime(2016, 9, 23, 19, 17, 12)
                },
                new Product
                {
                    CategoryId = categories[1].CategoryId,
                    Category = categories[1],
                    Name = "Bath Towel",
                    Description = "Bath Towel",
                    DisplayOrder = 5,
                    CreatedBy = "b07febfd-6df5-467c-bdff-ae812290608b",
                    CreatedDateTime = new DateTime(2016, 2, 19, 21, 23, 56)
                },
            };

            this.SeedData(categories);
            this.SeedData(products);
        }
    }
}
