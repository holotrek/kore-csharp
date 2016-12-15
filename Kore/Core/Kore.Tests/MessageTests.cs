// ***********************************************************************
// <copyright file="MessageTests.cs" company="CAI">
//     Copyright © CAI 2016
// </copyright>
// ***********************************************************************

using CAI.Core.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace CAI.Core.Tests
{
    /// <summary>
    /// Tests the Messages part of the core to make sure that the appropriate message is retrieved based on the culture provided.
    /// </summary>
    [TestClass]
    public class MessageTests
    {
        /// <summary>
        /// Tests that messages for default culture gets message from default resource.
        /// </summary>
        [TestMethod]
        public void Messages_GetsEnglishMessageByDefault()
        {
            string messageResult = CoreMessageFactory.GetMessage(CoreMessageKeys.NoRecordsSaved);
            Assert.AreEqual("The request passed, but no changes were detected to be saved.", messageResult);
        }

        /// <summary>
        /// Tests that messages for French culture gets message from French resource.
        /// </summary>
        [TestMethod]
        public void Messages_GetsFrenchMessage()
        {
            string messageResult = CoreMessageFactory.GetMessage(CoreMessageKeys.NoRecordsSaved, new CultureInfo("fr"));
            Assert.AreEqual("La demande passé, mais aucun changement n'a été détecté pour être sauvé.", messageResult);
        }
    }
}
