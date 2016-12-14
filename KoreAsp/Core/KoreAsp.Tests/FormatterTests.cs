// ***********************************************************************
// <copyright file="FormatterTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using KoreAsp.Composites;
using KoreAsp.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreAsp.Tests
{
    /// <summary>
    /// Tests that the formatters behave correctly.
    /// </summary>
    [TestClass]
    public class FormatterTests
    {
        /// <summary>
        /// Tests that an object will contain the property with the correctly formatted name.
        /// </summary>
        [TestMethod]
        public void NameFormatsCorrectly()
        {
            var tc = new TestClass
            {
                Name = new FullName("Doe", "John", "Test")
            };

            FormatterStrategy strategy = new FormatterStrategy();

            strategy.SetFormatter(typeof(IFullNameFormatter), new FirstLeadingWithFullMiddle());
            strategy.Format(tc);
            Assert.AreEqual("John Test Doe", tc.FullName);

            strategy.SetFormatter(typeof(IFullNameFormatter), new FirstLeadingWithInitializedMiddle());
            strategy.Format(tc);
            Assert.AreEqual("John T. Doe", tc.FullName);

            strategy.SetFormatter(typeof(IFullNameFormatter), new LastLeadingWithFullMiddle());
            strategy.Format(tc);
            Assert.AreEqual("Doe, John Test", tc.FullName);

            strategy.SetFormatter(typeof(IFullNameFormatter), new LastLeadingWithInitializedMiddle());
            strategy.Format(tc);
            Assert.AreEqual("Doe, John T.", tc.FullName);
        }

        /// <summary>
        /// Tests that an object will contain the property with the correctly formatted date.
        /// </summary>
        [TestMethod]
        public void DateFormatsCorrectly()
        {
            var tc = new TestClass
            {
                Date = new DateTime(2016, 10, 31, 8, 30, 22)
            };

            FormatterStrategy strategy = new FormatterStrategy();
            strategy.SetFormatter(typeof(IDateFormatter), new DateFormatter());
            strategy.Format(tc);

            Assert.AreEqual("10/31/2016 8:30:22 AM", tc.FullDateTime);
            Assert.AreEqual("Monday, October 31, 2016", tc.LongDate);
            Assert.AreEqual("8:30:22 AM", tc.LongTime);
            Assert.AreEqual("10/31/2016", tc.ShortDate);
            Assert.AreEqual("8:30 AM", tc.ShortTime);
            Assert.AreEqual("2016-10-31T08:30:22", tc.SortableDateTime);
            Assert.AreEqual("636134994220000000", tc.Ticks);
        }

        /// <summary>
        /// Class TestClass.
        /// </summary>
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public FullName Name { get; set; }

            /// <summary>
            /// Gets or sets the full name.
            /// </summary>
            /// <value>The full name.</value>
            [FormattedFullName("Name")]
            public string FullName { get; set; }

            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            /// <value>The date.</value>
            public DateTime Date { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.FullDateTime)]
            public string FullDateTime { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.LongDate)]
            public string LongDate { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.LongTime)]
            public string LongTime { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.ShortDate)]
            public string ShortDate { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.ShortTime)]
            public string ShortTime { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.SortableDateTime)]
            public string SortableDateTime { get; set; }

            /// <summary>
            /// Gets or sets the date string.
            /// </summary>
            /// <value>The date string.</value>
            [FormattedDate("Date", DateStringFormat.Ticks)]
            public string Ticks { get; set; }
        }
    }
}
