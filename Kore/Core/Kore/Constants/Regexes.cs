// ***********************************************************************
// <copyright file="Regexes.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Text.RegularExpressions;

namespace Kore.Constants
{
    /// <summary>
    /// A set of common regular expressions that are compiled to the assembly.
    /// </summary>
    public static class Regexes
    {
        #region Private Static Fields

        /// <summary>
        /// The regex for any amount of whitespace characters.
        /// </summary>
        private static Regex _whiteSpace = new Regex(@"\s+", RegexOptions.Compiled);

        /// <summary>
        /// The regex for any character that is not a valid character for a URL.
        /// </summary>
        private static Regex _notValidUrlCharacter = new Regex(@"[^a-zA-Z0-9_\.~-]+", RegexOptions.Compiled);

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the regex for any amount of whitespace characters.
        /// </summary>
        public static Regex WhiteSpace
        {
            get
            {
                return _whiteSpace;
            }
        }

        /// <summary>
        /// Gets the regex for any character that is not a valid character for a URL.
        /// </summary>
        public static Regex NotValidUrlCharacter
        {
            get
            {
                return _notValidUrlCharacter;
            }
        }

        #endregion
    }
}
