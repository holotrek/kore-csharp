// ***********************************************************************
// <copyright file="FormatterStrategy.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// Concrete implementation of a collection of different formatters in order to provide consistency for the application.
    /// </summary>
    public class FormatterStrategy : IFormatterStrategy
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatterStrategy"/> class.
        /// </summary>
        public FormatterStrategy()
        {
            this.Formatters = new Dictionary<Type, IFormatter>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the formatters.
        /// </summary>
        public Dictionary<Type, IFormatter> Formatters { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the formatter for the specified interface type.
        /// </summary>
        /// <param name="formatterInterface">The formatter interface type, for example <see cref="IDateFormatter" />.</param>
        /// <param name="formatter">The formatter.</param>
        public void SetFormatter(Type formatterInterface, IFormatter formatter)
        {
            this.Formatters[formatterInterface] = formatter;
        }

        /// <summary>
        /// Formats the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="obj">The object.</param>
        public void Format<T>(T obj)
        {
            PropertyInfo[] infos = typeof(T).GetProperties();
            foreach (PropertyInfo pi in infos)
            {
                IEnumerable<IFormatAttribute> formatters = pi.GetCustomAttributes<BaseFormatAttribute>();
                foreach (IFormatAttribute attr in formatters)
                {
                    Type t = attr.GetType();
                    if (typeof(IFormatAttribute).IsAssignableFrom(t))
                    {
                        Type innerType = t.GetInterface("IFormatAttribute`1").GetGenericArguments()[0];
                        if (innerType != null && this.Formatters.ContainsKey(innerType))
                        {
                            attr.Formatter = this.Formatters[innerType];
                            attr.Format(obj, pi.Name);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
