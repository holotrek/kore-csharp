// ***********************************************************************
// <copyright file="ComparerTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using Kore.Comparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Tests
{
    /// <summary>
    /// Tests that the custom comparers in the Core work consistently.
    /// </summary>
    [TestClass]
    public class ComparerTests
    {
        /// <summary>
        /// Tests that the compare function testing objects with equal properties results in true result.
        /// </summary>
        [TestMethod]
        public void CompareFunctionOfEqualPropertiesResultsInTrueResult()
        {
            var t1 = new CompareTest { Prop = "Test" };
            var t2 = new CompareTest { Prop = "Test" };
            var compareFunc = new Func<CompareTest, CompareTest, bool>((x, y) => x.Prop == y.Prop);
            var comparer = new LambdaEqualityComparer<CompareTest>(compareFunc);
            Assert.IsTrue(comparer.Equals(t1, t2));
            Assert.IsTrue(object.Equals(t1, t2));
        }

        /// <summary>
        /// Tests that the compare function testing objects with non-equal properties results in false result.
        /// </summary>
        [TestMethod]
        public void CompareFunctionOfNonEqualPropertiesResultsInFalseResult()
        {
            var t1 = new CompareTest { Prop = "Test" };
            var t2 = new CompareTest { Prop = "ASDF" };
            var compareFunc = new Func<CompareTest, CompareTest, bool>((x, y) => x.Prop == y.Prop);
            var comparer = new LambdaEqualityComparer<CompareTest>(compareFunc);
            Assert.IsFalse(comparer.Equals(t1, t2));
            Assert.IsFalse(object.Equals(t1, t2));
        }

        /// <summary>
        /// A model for testing compare functions
        /// </summary>
        private struct CompareTest
        {
            /// <summary>
            /// Gets or sets the property.
            /// </summary>
            /// <value>The property.</value>
            public string Prop { get; set; }
        }
    }
}
