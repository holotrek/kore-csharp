// ***********************************************************************
// <copyright file="ExtensionTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KoreAsp.Comparers;
using KoreAsp.Extensions;
using KoreAsp.Providers.Serialization.Newtonsoft;
using KoreAsp.Service.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace KoreAsp.Tests
{
    /// <summary>
    /// Tests that the extensions behave correctly.
    /// </summary>
    [TestClass]
    public class ExtensionTests
    {
        /// <summary>
        /// Tests that an object can be converted into an enumerable of itself.
        /// </summary>
        [TestMethod]
        public void ObjectCanBeConvertedToEnumerableOfItself()
        {
            IEnumerable<string> collection = "Test".ToEnumerable();
            Assert.AreEqual(1, collection.Count());
            Assert.AreEqual("Test", collection.First());
        }

        /// <summary>
        /// Tests that the enum description is obtained correctly.
        /// </summary>
        [TestMethod]
        public void EnumDescriptionsAreObtainedCorrectly()
        {
            Assert.AreEqual("Item 1", TestEnum.Item1.GetEnumDescription());
            Dictionary<TestEnum, string> descriptions = TestEnum.Item1.GetEnumDescriptions();
            Assert.AreEqual(2, descriptions.Count());
            Assert.AreEqual("Item Two", descriptions[TestEnum.Item2]);
        }

        /// <summary>
        /// Tests that the method that gets the inner most exception gets the correct one.
        /// </summary>
        [TestMethod]
        public void InnerMostExceptionCanBeObtained()
        {
            var test1 = new Exception("Test Single Level Exception");
            var test2 = new Exception("Test Multi-level Exception", new Exception("This is the message it should get."));
            Assert.AreEqual("Test Single Level Exception", test1.GetMostInner().Message);
            Assert.AreEqual("This is the message it should get.", test2.GetMostInner().Message);
        }

        /// <summary>
        /// Tests that the property name can be retrieved from an expression.
        /// </summary>
        [TestMethod]
        public void CanGetPropertyNameOfExpression()
        {
            var parameter = Expression.Parameter(typeof(TestClass), "x");
            var member = Expression.Property(parameter, "TestProperty");
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(TestClass), typeof(string));
            var lambda = Expression.Lambda(delegateType, member, parameter);
            Assert.AreEqual("TestProperty", lambda.GetPropertyName());
        }

        /// <summary>
        /// Tests that the LINQ extensions for set operations that take a lambda equality comparer work.
        /// </summary>
        [TestMethod]
        public void PerformsSetOperationsWithLambdaComparer()
        {
            var comparer = new LambdaEqualityComparer<TestClass>((x, y) => x.TestProperty == y.TestProperty);

            IEnumerable<TestClass> set1 = new TestClass { TestProperty = "Test1" }.ToEnumerable();
            IEnumerable<TestClass> set2 = new TestClass { TestProperty = "Test2" }.ToEnumerable();
            IEnumerable<TestClass> set3 = new TestClass { TestProperty = "Test3" }.ToEnumerable();

            // Create a Union of [Test1] and [Test2], which should result in [Test1, Test2]
            IEnumerable<TestClass> union1 = set1.Union(set2, comparer);
            Assert.AreEqual(2, union1.Count());

            // Create a Union of [Test1, Test2] and [Test2], which should still result in [Test1, Test2]
            IEnumerable<TestClass> union2 = union1.Union(set2, comparer);
            Assert.AreEqual(2, union2.Count());

            // Create a Subtraction of [Test1, Test2] and [Test1], which should result in [Test2]
            IEnumerable<TestClass> except1 = union2.Except(set1, comparer);
            Assert.AreEqual(1, except1.Count());
            Assert.AreEqual("Test2", except1.First().TestProperty);

            // Create an intersection of [Test1, Test2] and [Test2, Test3], which should result in [Test2]
            IEnumerable<TestClass> union3 = set2.Union(set3, comparer);
            IEnumerable<TestClass> intersect1 = union2.Intersect(union3, comparer);
            Assert.AreEqual(1, intersect1.Count());
            Assert.AreEqual("Test2", intersect1.First().TestProperty);

            // Do a distinct on [Test1, Test2, Test2] which should result in [Test1, Test2]
            IEnumerable<TestClass> concat1 = union2.Concat(set2);
            IEnumerable<TestClass> distinct1 = concat1.Distinct(comparer);
            Assert.AreEqual(2, distinct1.Count());
            Assert.IsTrue(distinct1.Where(x => x.TestProperty == "Test1").Any());
            Assert.IsTrue(distinct1.Where(x => x.TestProperty == "Test2").Any());
        }

        /// <summary>
        /// Tests that the math operations ToRadians and ToDegrees work correctly.
        /// </summary>
        [TestMethod]
        public void MathOperationsBetweenRadiansAndDegreesAreCorrect()
        {
            double deg = 90;
            double rad = 0.5 * Math.PI;
            Assert.AreEqual(rad, deg.ToRadians());
            Assert.AreEqual(deg, rad.ToDegrees());
        }

        /// <summary>
        /// Tests that a collection of items will be paged and sorted correctly based on the provided inputs.
        /// </summary>
        [TestMethod]
        public void CollectionPagesAndSortsWithCorrectResults()
        {
            var randomList = new List<TestClass>();
            int i = 0;
            var usedNumbers = new List<int>();
            while (i < 50)
            {
                int num = this.GetNextUnusedNumber(usedNumbers);
                usedNumbers.Add(num);
                randomList.Add(new TestClass
                {
                    TestOrder = num,
                    TestProperty = num.ToString()
                });
                i++;
            }

            var sortedAndPagedList = randomList.PageAndSort(new GetCollectionRequest
            {
                Page = 3,
                PageSize = 10,
                Sort = "TestOrder".ToEnumerable()
            });

            Assert.AreEqual(10, sortedAndPagedList.Count());
            int lowestOnPage = sortedAndPagedList.First().TestOrder;
            int highestOnPage = sortedAndPagedList.Last().TestOrder;
            Assert.IsTrue(randomList.OrderBy(x => x.TestOrder).Take(20).All(x => x.TestOrder < lowestOnPage), "First page and sort gets different lower results.");
            Assert.IsTrue(randomList.OrderByDescending(x => x.TestOrder).Take(20).All(x => x.TestOrder > highestOnPage), "First page and sort gets different higher results.");
            
            var sortedAndPagedList2 = randomList.PageAndSort(new GetCollectionRequest
            {
                Skip = 5,
                Take = 20,
                Sort = "TestOrder".ToEnumerable()
            });

            Assert.AreEqual(20, sortedAndPagedList2.Count());
            int lowestOnPage2 = sortedAndPagedList2.First().TestOrder;
            int highestOnPage2 = sortedAndPagedList2.Last().TestOrder;
            Assert.IsTrue(randomList.OrderBy(x => x.TestOrder).Take(5).All(x => x.TestOrder < lowestOnPage2), "Second page and sort gets different lower results.");
            Assert.IsTrue(randomList.OrderByDescending(x => x.TestOrder).Take(25).All(x => x.TestOrder > highestOnPage2), "Second page and sort gets different higher results.");
        }

        /// <summary>
        /// Tests that a collection of items will be filtered correctly based on the provided inputs.
        /// </summary>
        [TestMethod]
        public void CollectionFiltersWithCorrectResults()
        {
            var testList = new List<TestClass>
            {
                new TestClass
                {
                    ID = 1,
                    TestOrder = 1,
                    TestProperty = "Test1"
                },
                new TestClass
                {
                    ID = 2,
                    TestOrder = 2,
                    TestProperty = "Test1"
                },
                new TestClass
                {
                    ID = 3,
                    TestOrder = 2,
                    TestProperty = "Test2"
                },
                new TestClass
                {
                    ID = 4,
                    TestOrder = 3,
                    TestProperty = "Test3"
                }
            };

            // Simple filter
            var filteredList1 = testList.Filter(
                new GetCollectionRequest
                {
                    Filter = JsonConvert.SerializeObject(new FilterModel
                    {
                        Field = "TestProperty",
                        Operator = FilterOperator.EndsWith,
                        Value = "1"
                    })
                }, 
                new NewtonsoftSerializationProvider());
            Assert.AreEqual(2, filteredList1.Count(), "Filtered List 2 should contain 2 items.");
            Assert.IsTrue(filteredList1.Where(x => x.ID == 1).Any(), "Filtered List 1 should contain ID 1.");
            Assert.IsTrue(filteredList1.Where(x => x.ID == 2).Any(), "Filtered List 1 should contain ID 2.");

            // Complex AND filter
            var filteredList2 = testList.Filter(
                new GetCollectionRequest
                {
                    Filter = JsonConvert.SerializeObject(new FilterModel
                    {
                        Logic = FilterLogic.And,
                        ChildFilters = new List<FilterModel>
                        {
                            new FilterModel
                            {
                                Field = "TestProperty",
                                Operator = FilterOperator.Equals,
                                Value = "Test1"
                            },
                            new FilterModel
                            {
                                Field = "TestOrder",
                                Operator = FilterOperator.GreaterThan,
                                Value = 1,
                                ValueType = "int"
                            },
                        }
                    })
                }, 
                new NewtonsoftSerializationProvider());
            Assert.AreEqual(1, filteredList2.Count(), "Filtered List 2 should contain 1 item.");
            Assert.AreEqual(2, filteredList2.First().ID, "Filtered List 2 should contain ID 2.");

            // Complex OR filter
            var filteredList3 = testList.Filter(
                new GetCollectionRequest
                {
                    Filter = JsonConvert.SerializeObject(new FilterModel
                    {
                        Logic = FilterLogic.Or,
                        ChildFilters = new List<FilterModel>
                        {
                            new FilterModel
                            {
                                Field = "TestProperty",
                                Operator = FilterOperator.Equals,
                                Value = "Test3"
                            },
                            new FilterModel
                            {
                                Field = "TestOrder",
                                Operator = FilterOperator.LessThanOrEqualTo,
                                Value = 3,
                                ValueType = "int"
                            },
                        }
                    })
                }, 
                new NewtonsoftSerializationProvider());
            Assert.AreEqual(4, filteredList3.Count(), "Filtered List 3 should contain all 4 items.");
        }

        /// <summary>
        /// Tests that a string can be found within a list containing that string.
        /// </summary>
        [TestMethod]
        public void StringWithinListContainingString()
        {
            Assert.IsTrue("Test".In(new string[] { "asdf", "Test" }));
            Assert.IsFalse("Test".In(new string[] { "asdf", "qwerty" }));
            Assert.IsFalse("Test".In(new string[] { }));
        }

        /// <summary>
        /// Tests that getting the default value of a type will result in the default value of the type... how very meta.
        /// </summary>
        [TestMethod]
        public void DefaultValueFromType()
        {
            Assert.AreEqual(default(object), typeof(object).GetDefaultTypeValue());
            Assert.AreEqual(default(string), typeof(string).GetDefaultTypeValue());
            Assert.AreEqual(default(int), typeof(int).GetDefaultTypeValue());
            Assert.AreEqual(default(int?), typeof(int?).GetDefaultTypeValue());
            Assert.AreEqual(default(bool), typeof(bool).GetDefaultTypeValue());
            Assert.AreEqual(default(bool?), typeof(bool?).GetDefaultTypeValue());
            Assert.AreEqual(default(decimal), typeof(decimal).GetDefaultTypeValue());
            Assert.AreEqual(default(decimal?), typeof(decimal?).GetDefaultTypeValue());
            Assert.AreEqual(default(TestClass), typeof(TestClass).GetDefaultTypeValue());
        }

        /// <summary>
        /// Gets the next unused number.
        /// </summary>
        /// <param name="usedNumbers">The used numbers.</param>
        /// <returns>Next unused random number.</returns>
        private int GetNextUnusedNumber(List<int> usedNumbers)
        {
            var rand = new Random();
            int num;
            do
            {
                num = rand.Next(1, 10000);
            }
            while (usedNumbers.Contains(num));
            return num;
        }

        /// <summary>
        /// Class TestClass.
        /// </summary>
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public int ID { get; set; }

            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>The test property.</value>
            public string TestProperty { get; set; }

            /// <summary>
            /// Gets or sets the test order.
            /// </summary>
            /// <value>The test order.</value>
            public int TestOrder { get; set; }
        }

        /// <summary>
        /// Enum TestEnum
        /// </summary>
        private enum TestEnum
        {
            /// <summary>
            /// The item1
            /// </summary>
            [System.ComponentModel.Description("Item 1")]
            Item1,

            /// <summary>
            /// The item2
            /// </summary>
            [System.ComponentModel.Description("Item Two")]
            Item2
        }
    }
}
