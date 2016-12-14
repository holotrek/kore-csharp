// ***********************************************************************
// <copyright file="FormattedDateAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Reflection;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// Indicates that the property is a string that will receive the formatted date with the data from
    /// another property of type <see cref="DateTime"/> or <see cref="DateTime?"/>.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class FormattedDateAttribute : BaseFormatAttribute, IFormatAttribute<IDateFormatter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedDateAttribute" /> class.
        /// </summary>
        /// <param name="dateProperty">The date property.</param>
        /// <param name="formatType">Type of the format.</param>
        public FormattedDateAttribute(string dateProperty, DateStringFormat formatType)
        {
            this.DateProperty = dateProperty;
            this.FormatType = formatType;
        }

        /// <summary>
        /// Gets the specific formatter for this attribute.
        /// </summary>
        public IDateFormatter SpecificFormatter
        {
            get
            {
                return (IDateFormatter)this.Formatter;
            }
        }

        /// <summary>
        /// Gets or sets the date property.
        /// </summary>
        /// <value>
        /// The date property.
        /// </value>
        public string DateProperty { get; set; }

        /// <summary>
        /// Gets or sets the type of the format.
        /// </summary>
        /// <value>
        /// The type of the format.
        /// </value>
        public DateStringFormat FormatType { get; set; }

        /// <summary>
        /// Formats the specified date within the model and sets the annotated property.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="annotatedPropertyName">Name of the annotated property.</param>
        public override void Format<T>(T model, string annotatedPropertyName)
        {
            Type containerType = typeof(T);
            PropertyInfo dateProp = containerType.GetProperty(this.DateProperty);
            PropertyInfo stringProp = containerType.GetProperty(annotatedPropertyName);

            if (dateProp != null && stringProp != null)
            {
                DateTime? toParse = null;
                DateTime d;
                if (DateTime.TryParse(dateProp.GetValue(model).ToString(), out d))
                {
                    toParse = d;
                }

                string formatted = string.Empty;
                switch (this.FormatType)
                {
                    case DateStringFormat.FullDateTime:
                        formatted = this.SpecificFormatter.ToDateAndTime(toParse);
                        break;
                    case DateStringFormat.ShortDate:
                        formatted = this.SpecificFormatter.ToShortDate(toParse);
                        break;
                    case DateStringFormat.LongDate:
                        formatted = this.SpecificFormatter.ToLongDate(toParse);
                        break;
                    case DateStringFormat.ShortTime:
                        formatted = this.SpecificFormatter.ToShortTime(toParse);
                        break;
                    case DateStringFormat.LongTime:
                        formatted = this.SpecificFormatter.ToLongTime(toParse);
                        break;
                    case DateStringFormat.SortableDateTime:
                        formatted = this.SpecificFormatter.ToSortable(toParse);
                        break;
                    case DateStringFormat.Ticks:
                        formatted = this.SpecificFormatter.ToTicks(toParse);
                        break;
                }

                stringProp.SetValue(model, formatted);
            }
        }
    }

    /// <summary>
    /// Indicates how to format the date
    /// </summary>
    public enum DateStringFormat
    {
        /// <summary>
        /// The full date time
        /// </summary>
        FullDateTime,

        /// <summary>
        /// The short date
        /// </summary>
        ShortDate,

        /// <summary>
        /// The long date
        /// </summary>
        LongDate,

        /// <summary>
        /// The short time
        /// </summary>
        ShortTime,

        /// <summary>
        /// The long time
        /// </summary>
        LongTime,

        /// <summary>
        /// The sortable date time
        /// </summary>
        SortableDateTime,

        /// <summary>
        /// The ticks
        /// </summary>
        Ticks
    }
}
