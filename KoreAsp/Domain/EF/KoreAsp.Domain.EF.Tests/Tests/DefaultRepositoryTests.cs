// ***********************************************************************
// <copyright file="DefaultRepositoryTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KoreAsp.Domain.EF.Context;
using KoreAsp.Domain.EF.Tests.Data;
using KoreAsp.Domain.EF.Tests.Domain.Products;
using KoreAsp.Domain.EF.Tests.Domain.Products.Models;
using KoreAsp.Domain.EF.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreAsp.Domain.EF.Tests.Tests
{
    /// <summary>
    /// Tests the EF Unit of Work and Repository with all the default configuration settings.
    /// </summary>
    [TestClass]
    public class DefaultRepositoryTests : BaseEFTest
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
        /// Tests that a repository can be obtained from the Unit of Work.
        /// </summary>
        [TestMethod]
        public void EF_CanGetRepositoryBackFromUnitOfWork()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);
                var repoAgain = uow.GetRepository<ProductRepository>();
                Assert.IsNotNull(repoAgain);
                Assert.AreEqual(typeof(ProductRepository), repoAgain.GetType());
            }
        }

        /// <summary>
        /// Tests that a category can be inserted into the fake context and then queried
        /// </summary>
        [TestMethod]
        public void EF_CategoryCanBeInsertedAndQueried()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);

                string userId = uow.AuthenticationProvider.CurrentUser.UniqueId;

                // 2 existing records
                Assert.AreEqual(2, repo.Get<Category>().Count(), "Initial Count");

                // Adds the new category simply by instantiating it
                new Category(repo, "Dishes", "Plates, Bowels, etc");

                // Hasn't been commited yet, so still 2 existing records
                Assert.AreEqual(2, repo.Get<Category>().Count(), "After Insert, but not committed");

                uow.Commit();

                // Now there should be 3 records, 2 existing, plus one new one
                Assert.AreEqual(3, repo.Get<Category>().Count(), "After Commit");

                Category c = repo.Get<Category>().Where(x => x.Name == "Dishes").FirstOrDefault();
                Assert.IsNotNull(c);
                Assert.AreEqual("Plates, Bowels, etc", c.Description);

                // Test user has created it and no one has updated it
                Assert.AreEqual(userId, c.CreatedBy);
                Assert.IsNull(c.UpdatedBy);
            }
        }

        /// <summary>
        /// Tests that a product can be updated in the fake context and then re-queried with updated data
        /// </summary>
        [TestMethod]
        public void EF_ProductCanBeUpdated()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);

                string userId = uow.AuthenticationProvider.CurrentUser.UniqueId;

                Product initial = repo.Get<Product>().AsQueryable().Include(x => x.Category).Where(x => x.Name == "Dove").FirstOrDefault();
                string createdBy = initial.CreatedBy;
                Assert.IsNotNull(initial);
                Assert.AreEqual("Dove Bar Soap", initial.Description);

                initial.UpdateDescription(repo, "Dove Hand Soap");
                uow.Commit();

                // Now it should have new value
                Product p = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();
                Assert.IsNotNull(p);
                Assert.AreEqual("Dove Hand Soap", p.Description);

                // Test user has updated it and initial created by has not changed
                Assert.AreEqual(createdBy, p.CreatedBy);
                Assert.AreEqual(userId, p.UpdatedBy);
            }
        }

        /// <summary>
        /// Tests that a product can be queried and then load the associated category navigation property
        /// </summary>
        [TestMethod]
        public void EF_ProductsAssociatedCategoryCanBeEagerlyLoaded()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);

                Product p = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();

                Assert.IsNull(p.Category);

                repo.Entry(p).Reference(x => x.Category).Load();

                Assert.IsNotNull(p.Category);
                Assert.AreEqual("Soap", p.Category.Name);
            }
        }

        /// <summary>
        /// Tests that a set of products can be queried by a specific category and then have the category navigation property set
        /// </summary>
        [TestMethod]
        public void EF_ProductsCanBeFetchedForCategory()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);

                List<Product> prods = (from p in repo.Get<Product>()
                                       join c in repo.Get<Category>() on p.CategoryId equals c.CategoryId
                                       where c.Name == "Soap"
                                       select p).ToList();

                Assert.AreEqual(3, prods.Count());

                // Even without lazy loading a join will result in the navigation property being set
                Assert.IsNotNull(prods[0].Category);
            }
        }

        /// <summary>
        /// Tests that a set of products can be queried by a specific category and then have the category navigation property set
        /// </summary>
        [TestMethod]
        public void EF_ProductsCanBeFetchedWithCategoryNavigationSetUsingExplicitLoad()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow);

                List<Product> prods = (from p in repo.Get<Product>().AsQueryable().Include(x => x.Category)
                                       select p).ToList();

                Assert.AreEqual(5, prods.Count());
                Assert.IsNotNull(prods[0].Category);
            }
        }
    }
}
