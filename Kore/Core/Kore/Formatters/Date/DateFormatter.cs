// ***********************************************************************
// <copyright file="DateFormatter.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Globalization;

namespace Kore.Formatters
{
    /// <summary>
    /// Concrete implementation of a formatter that takes a <see cref="DateTime?"/> and formats it based on the current culture
    /// </summary>
    public class DateFormatter : IDateFormatter
    {
        /// <summary>
        /// The culture info
        /// </summary>
        private CultureInfo _culture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFormatter"/> class.
        /// </summary>
        public DateFormatter()
            : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFormatter"/> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public DateFormatter(CultureInfo culture)
        {
            this._culture = culture;
        }

        /// <summary>
        /// Gets a date and time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted date and time.
        /// </returns>
        public string ToDateAndTime(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString(this._culture.DateTimeFormat) : string.Empty;
        }

        /// <summary>
        /// Gets a short date based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted short date.
        /// </returns>
        public string ToShortDate(DateTime? date)
        {
            return this.ToString(date, this._culture.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Gets a long date based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted long date.
        /// </returns>
        public string ToLongDate(DateTime? date)
        {
            return this.ToString(date, this._culture.DateTimeFormat.LongDatePattern);
        }

        /// <summary>
        /// Gets a short time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted short time.
        /// </returns>
        public string ToShortTime(DateTime? date)
        {
            return this.ToString(date, this._culture.DateTimeFormat.ShortTimePattern);
        }

        /// <summary>
        /// Gets a long time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted long time.
        /// </returns>
        public string ToLongTime(DateTime? date)
        {
            return this.ToString(date, this._culture.DateTimeFormat.LongTimePattern);
        }

        /// <summary>
        /// Gets a sortable date and time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The formatted sortable date and time.
        /// </returns>
        public string ToSortable(DateTime? date)
        {
            return this.ToString(date, this._culture.DateTimeFormat.SortableDateTimePattern);
        }

        /// <summary>
        /// Gets the date and time as ticks based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted sortable date and time.</returns>
        public string ToTicks(DateTime? date)
        {
            return date.HasValue ? date.Value.Ticks.ToString() : "0";
        }

        /// <summary>
        /// Gets a date and time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The formatted date and time.
        /// </returns>
        private string ToString(DateTime? date, string format)
        {
            return date.HasValue ? date.Value.ToString(format) : string.Empty;
        }
    }
}
