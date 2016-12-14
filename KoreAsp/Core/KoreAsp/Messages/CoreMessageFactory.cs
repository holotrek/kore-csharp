// ***********************************************************************
// <copyright file="CoreMessageFactory.cs" company="CAI">
//     Copyright © CAI 2016
// </copyright>
// ***********************************************************************

using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CAI.Core.Messages
{
    /// <summary>
    /// Gets the appropriate message based on the provided key and culture.
    /// </summary>
    public static class CoreMessageFactory
    {
        /// <summary>
        /// Gets the message by the specified resource key for the default culture.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <returns>The message retrieved from the resource file.</returns>
        public static string GetMessage(string messageKey)
        {
            return CoreMessageFactory.GetMessage(messageKey, string.Empty);
        }

        /// <summary>
        /// Gets the message by the specified resource key for the specified culture.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns>The message retrieved from the resource file.</returns>
        public static string GetMessage(string messageKey, string cultureInfo)
        {
            CultureInfo ci = null;
            if (!string.IsNullOrWhiteSpace(cultureInfo))
            {
                ci = new CultureInfo(cultureInfo);
            }

            return CoreMessageFactory.GetMessage(messageKey, ci);
        }

        /// <summary>
        /// Gets the message by the specified resource key for the specified culture.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns>The message retrieved from the resource file.</returns>
        public static string GetMessage(string messageKey, CultureInfo cultureInfo)
        {
            Assembly assembly = typeof(CoreMessageFactory).Assembly;
            ResourceManager rm = new ResourceManager(assembly.GetName().Name + ".Messages.Resource.CoreMessages", assembly);
            return rm.GetString(messageKey, cultureInfo);
        }
    }
}
