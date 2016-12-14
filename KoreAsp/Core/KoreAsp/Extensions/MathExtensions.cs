// ***********************************************************************
// <copyright file="MathExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// Provides common extension methods for mathematical operations
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Converts an angle in degrees to radians.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static double ToRadians(this double degrees)
        {
            return degrees / 180 * Math.PI;
        }

        /// <summary>
        /// Converts an angle in radians to degrees.
        /// </summary>
        /// <param name="radians">The radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static double ToDegrees(this double radians)
        {
            return radians / Math.PI * 180;
        }
    }
}
