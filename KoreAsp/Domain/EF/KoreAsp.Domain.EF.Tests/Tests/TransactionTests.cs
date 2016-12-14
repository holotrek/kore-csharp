// ***********************************************************************
// <copyright file="TransactionTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Data.Entity;
using System.Linq;
using KoreAsp.Domain.EF.Context;
using KoreAsp.Domain.EF.Tests.Data;
using KoreAsp.Domain.EF.Tests.Domain.Orders;
using KoreAsp.Domain.EF.Tests.Domain.Orders.Models;
using KoreAsp.Domain.EF.Tests.Domain.Products;
using KoreAsp.Domain.EF.Tests.Domain.Products.Models;
using KoreAsp.Domain.EF.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreAsp.Domain.EF.Tests.Tests
{
    /// <summary>
    /// Tests that the EF Unit of Work and Repository handles transactions properly.
    /// </summary>
    [TestClass]
    public class TransactionTests : BaseEFTest
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
        /// Tests that a product can be updated and when an order is attempted to be inserted with no product, it rolls back the product update
        /// </summary>
        [TestMethod]
        public void EF_OrderInsertErrorRollsBackProductChange()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);
                
                Product prod = prodRepo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);
                
                prod.UpdateDescription(prodRepo, "Test Update");

                Assert.AreEqual(3, orderRepo.Get<Order>().Count());
                new Order(orderRepo, null, DateTime.Now);

                bool exceptionThrown = false;
                try
                {
                    uow.Commit();
                }
                catch
                {
                    Assert.IsTrue(this.Logger.Logs.Any());
                    Assert.AreEqual("Column 'ProductId' cannot be null. Error code: GenericError", this.Logger.Logs.First().Message);

                    Assert.AreEqual(3, orderRepo.Get<Order>().Count());

                    prodRepo.Entry(prod).Reload();
                    Assert.AreEqual("Ivory Bar Soap", prod.Description);

                    exceptionThrown = true;
                }
                finally
                {
                    if (!exceptionThrown)
                    {
                        Assert.Fail("Exception should've been thrown.");
                    }
                }
            }
        }

        /// <summary>
        /// Tests that a transaction and the changes can be rolled back by Unit of Work even after one repository saves changes.
        /// </summary>
        [TestMethod]
        public void EF_DisposingUnitOfWorkRollsBackTransaction()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                Product prod = prodRepo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);

                prod.UpdateDescription(prodRepo, "Test Update");
                prodRepo.SaveChanges();

                Assert.AreEqual(3, orderRepo.Get<Order>().Count());

                var orderProd = orderRepo.Get<OrderProduct>().Where(x => x.Name == prod.Name).FirstOrDefault();
                new Order(orderRepo, orderProd, DateTime.Now);
                orderRepo.SaveChanges();

                // Proves that changes were saved to database
                prodRepo.Entry(prod).Reload();
                Assert.AreEqual("Test Update", prod.Description);
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());

                // No commit executed
            }

            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                // Original values exist in database
                Product prod = prodRepo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);
                Assert.AreEqual(3, orderRepo.Get<Order>().Count());
            }
        }

        /// <summary>
        /// Tests that a transaction and the changes will be committed correctly.
        /// </summary>
        [TestMethod]
        public void EF_CommitingUnitOfWorkCommitsTransaction()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                Product prod = prodRepo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Ivory Bar Soap", prod.Description);

                prod.UpdateDescription(prodRepo, "Test Update");
                prodRepo.SaveChanges();

                Assert.AreEqual(3, orderRepo.Get<Order>().Count());

                var orderProd = orderRepo.Get<OrderProduct>().Where(x => x.Name == prod.Name).FirstOrDefault();
                new Order(orderRepo, orderProd, DateTime.Now);
                orderRepo.SaveChanges();

                // Proves that changes were saved to database
                prodRepo.Entry(prod).Reload();
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());

                try
                {
                    uow.Commit();
                }
                catch
                {
                    Assert.Fail("Commit failed unexpectedly.");
                }
            }

            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                var prodRepo = new ProductRepository(uow);
                var orderRepo = new OrderRepository(uow);

                // New values exist in database
                Product prod = prodRepo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Ivory").FirstOrDefault();
                Assert.AreEqual("Test Update", prod.Description);
                Assert.AreEqual(4, orderRepo.Get<Order>().Count());
            }
        }
    }
}
