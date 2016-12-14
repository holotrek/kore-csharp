// ***********************************************************************
// <copyright file="MockLog.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace KoreAsp.Providers.Logging
{
    /// <summary>
    /// A test log object for the <see cref="MockLoggingProvider" />.
    /// </summary>
    public class MockLog
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public Severity Level { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        #endregion
    }
}
