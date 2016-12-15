// ***********************************************************************
// <copyright file="MessageAndLoggingTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Linq;
using System.Resources;
using Kore.Providers.Messages.Resource.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Providers.Messages.Resource.Tests
{
    /// <summary>
    /// Tests the Message Provider and Logging Provider
    /// </summary>
    [TestClass]
    public class ResourceMessageTests
    {
        /// <summary>
        /// Tests that using the Resource Message Provider will allow messages to be shown differently based on a culture.
        /// </summary>
        [TestMethod]
        public void MessageShowsInCorrectCultureWithResourceMessageProvider()
        {
            ResourceManager rm = new ResourceManager(typeof(MessageResources));
            var englishMessages = new ResourceMessageProvider("en", rm);
            var spanishMessages = new ResourceMessageProvider("es", rm);
            englishMessages.AddMessage(MessageType.Info, "TestMessage");
            spanishMessages.AddMessage(MessageType.Info, "TestMessage");
            Assert.IsTrue(englishMessages.Messages.Any());
            Assert.IsTrue(spanishMessages.Messages.Any());
            Assert.AreEqual("This is a test message", englishMessages.Messages.First().Text);
            Assert.AreEqual("Este es un mensaje de prueba", spanishMessages.Messages.First().Text);
        }
    }
}
