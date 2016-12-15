// ***********************************************************************
// <copyright file="GpsPoint.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using Kore.Domain.Context;
using Kore.Extensions;

namespace Kore.Composites
{
    /// <summary>
    /// Represents a point on Earth in the global positioning system
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.ValueObject{Kore.Composites.GpsPoint}" />
    public class GpsPoint : ValueObject<GpsPoint>
    {
        #region Public Constants

        /// <summary>
        /// The earth radius in meters
        /// </summary>
        public const int EARTHRADIUS = 6371000;

        /// <summary>
        /// The conversion for meters per mile
        /// </summary>
        public const double METERSPERMILE = 1609.344;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GpsPoint"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public GpsPoint(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a destination GPS point based on the current GPS point with a course (angle) and distance.
        /// <c>http://www.movable-type.co.uk/scripts/latlong.html</c> (under "Destination point given distance and bearing from start point")
        /// </summary>
        /// <param name="angle">The angle in degrees (E = 0, N = 90, W = 180, S = 270).</param>
        /// <param name="distance">The distance in meters.</param>
        /// <returns>The destination GPS point.</returns>
        public GpsPoint GetDestinationPoint(double angle, double distance)
        {
            distance = distance / GpsPoint.EARTHRADIUS;
            angle = angle.ToRadians();

            double lat = this.Latitude.ToRadians();
            double lng = this.Longitude.ToRadians();

            double newLat = Math.Asin((Math.Sin(lat) * Math.Cos(distance)) + (Math.Cos(lat) * Math.Sin(distance) * Math.Cos(angle)));
            double newLng = lng + Math.Atan2(Math.Sin(angle) * Math.Sin(distance) * Math.Cos(lat), Math.Cos(distance) - (Math.Sin(lat) * Math.Sin(newLat)));

            return new GpsPoint(newLat.ToDegrees(), newLng.ToDegrees());
        }

        /// <summary>
        /// Gets the distance to another GPS point from this one.
        /// <c>https://en.wikipedia.org/wiki/Haversine_formula</c>
        /// </summary>
        /// <param name="otherPoint">The other point.</param>
        /// <returns>The distance in meters to the other point.</returns>
        public double GetDistanceToPoint(GpsPoint otherPoint)
        {
            double latDist = (otherPoint.Latitude - this.Latitude).ToRadians();
            double lngDist = (otherPoint.Longitude - this.Longitude).ToRadians();
            double a = (Math.Sin(latDist / 2) * Math.Sin(latDist / 2)) + (Math.Cos(this.Latitude.ToRadians()) * Math.Cos(otherPoint.Latitude.ToRadians()) * Math.Sin(lngDist / 2) * Math.Sin(lngDist / 2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return GpsPoint.EARTHRADIUS * c;
        }

        #endregion
    }
}
