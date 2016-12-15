// ***********************************************************************
// <copyright file="TransactionTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Linq;
using Kore.Domain.LiteDb.Context;
using Kore.Domain.LiteDb.Tests.Domain.Orders;
using Kore.Domain.LiteDb.Tests.Domain.Orders.Models;
using Kore.Domain.LiteDb.Tests.Domain.Products;
using Kore.Domain.LiteDb.Tests.Domain.Products.Models;
using Kore.Domain.LiteDb.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Domain.LiteDb.Tests.Tests
{
    /// <summary>
    /// Tests that the EF Unit of Work and Repository handles transactions properly.
    /// </summary>
    [TestClass]
    public class TransactionTests : BaseLiteDbTest
    {
        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
        }

        /// <summary>
        /// Tests that a transaction and the changes can be rolled back by Unit of Work even after one repository saves changes.
        /// </summary>
        [TestMethod]
        public void LiteDb_DisposingUnitOfWorkRollsBackTransaction()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                Product prod = prodRepo.Get<Product>().Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);

                prod.UpdateDescription(prodRepo, "Test Update");

                Assert.AreEqual(3, orderRepo.Get<Order>().Count());

                var orderProd = orderRepo.Get<OrderProduct>().Where(x => x.Name == prod.Name).FirstOrDefault();
                new Order(orderRepo, orderProd, DateTime.Now);

                // Proves that changes were saved to database
                Assert.AreEqual("Test Update", prod.Description);
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());

                // No commit executed
            }

            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                // Original values exist in database
                Product prod = prodRepo.Get<Product>().Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);
                Assert.AreEqual(3, orderRepo.Get<Order>().Count());
            }
        }

        /// <summary>
        /// Tests that a transaction and the changes will be committed correctly.
        /// </summary>
        [TestMethod]
        public void LiteDb_CommitingUnitOfWorkCommitsTransaction()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                Product prod = prodRepo.Get<Product>().Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);

                prod.UpdateDescription(prodRepo, "Test Update");

                Assert.AreEqual(3, orderRepo.Get<Order>().Count());

                var orderProd = orderRepo.Get<OrderProduct>().Where(x => x.Name == prod.Name).FirstOrDefault();
                new Order(orderRepo, orderProd, DateTime.Now);

                // Proves that changes were saved to database
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());

                try
                {
                    uow.Commit();
                }
                catch
                {
                    Assert.Fail("Commit failed unexpectedly.");
                }

                // New values exist in database
                prod = prodRepo.Get<Product>().Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Test Update", prod.Description);
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());
            }
        }
    }
}
