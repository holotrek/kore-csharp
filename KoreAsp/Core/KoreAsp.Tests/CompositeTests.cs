// ***********************************************************************
// <copyright file="CompositeTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using KoreAsp.Composites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreAsp.Tests
{
    /// <summary>
    /// Tests that the custom composites behave correctly.
    /// </summary>
    [TestClass]
    public class CompositeTests
    {
        /// <summary>
        /// Tests that the GPS model will calculate a destination correctly.
        /// </summary>
        [TestMethod]
        public void CacluatesDestinationGpsPointCorrectly()
        {
            GpsPoint gps = new GpsPoint(40.25, -76.83);

            // Go 100 miles in a NE bearing
            GpsPoint dest = gps.GetDestinationPoint(45, 100 * GpsPoint.METERSPERMILE);

            Assert.AreEqual(41.26549647088418, dest.Latitude, 14);
            Assert.AreEqual(-75.46848847257391, dest.Longitude, 14);
        }

        /// <summary>
        /// Tests that the GPS model will calculate a destination correctly.
        /// </summary>
        [TestMethod]
        public void CacluatesDistanceBetweenTwoGpsPointsCorrectly()
        {
            GpsPoint gps1 = new GpsPoint(40.25, -76.83);
            GpsPoint gps2 = new GpsPoint(41.26549647088418, -75.46848847257391);
            
            // Should be around 100 miles
            Assert.AreEqual(100 * GpsPoint.METERSPERMILE, 1, gps1.GetDistanceToPoint(gps2));
        }
    }
}
