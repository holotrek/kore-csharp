// ***********************************************************************
// <copyright file="MessageAndLoggingTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Linq;
using Kore.Providers.Logging;
using Kore.Providers.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Tests
{
    /// <summary>
    /// Tests the Message Provider and Logging Provider
    /// </summary>
    [TestClass]
    public class MessageAndLoggingTests
    {
        /// <summary>
        /// Tests that when a message is added to the Message Provider that has a corresponding Logging Provider, that the message is logged.
        /// </summary>
        [TestMethod]
        public void MessageAddedIsAlsoLoggedWhenLoggerExists()
        {
            var lg = new MockLoggingProvider();
            var mp = new BaseMessageProvider(lg);
            mp.AddMessage(MessageType.Error, "Test Error");
            Assert.IsTrue(lg.Logs.Any());
            Assert.AreEqual(Severity.Error, lg.Logs[0].Level);
            Assert.AreEqual("Test Error", lg.Logs[0].Message);
        }

        /// <summary>
        /// Tests that when an exception is added to the Message Provider that has a corresponding Logging Provider, that the message is logged.
        /// </summary>
        [TestMethod]
        public void ExceptionIsLoggedWhenLoggerExists()
        {
            var lg = new MockLoggingProvider();
            var mp = new BaseMessageProvider(lg);
            mp.AddException(new Exception("Test Exception"));
            Assert.IsTrue(lg.Logs.Any());
            Assert.AreEqual(Severity.Fatal, lg.Logs[0].Level);
            Assert.AreEqual("Test Exception", lg.Logs[0].Message);
        }

        /// <summary>
        /// Tests that a message added to a Message Provider that has no corresponding Logging Provider will still add the message successfully.
        /// </summary>
        [TestMethod]
        public void MessageIsAddedWithoutLogger()
        {
            var mp = new BaseMessageProvider();
            mp.AddMessage(MessageType.Error, "Test Error");
            Assert.IsNull(mp.Logger);
            Assert.IsTrue(mp.Messages.Any());
            Assert.AreEqual(MessageType.Error, mp.Messages.First().Type);
            Assert.AreEqual("Test Error", mp.Messages.First().Text);
        }
    }
}
