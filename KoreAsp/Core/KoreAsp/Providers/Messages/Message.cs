// ***********************************************************************
// <copyright file="Message.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Providers.Messages
{
    /// <summary>
    /// Represents a message of some sort sent to the application that is consuming the service layer.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        public Message()
        {
            this.ReferenceId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type.</value>
        public MessageType Type { get; set; }

        /// <summary>
        /// Gets or sets the text of the message.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the (optional) property that the message is related to.
        /// </summary>
        /// <value>The property.</value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the (optional) record identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the (optional) unique string identifier of the record.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier so that this message can be retrieved again, i.e. from logs.
        /// </summary>
        /// <value>The reference identifier.</value>
        public Guid ReferenceId { get; set; }
    }

    /// <summary>
    /// The type of message indicating to consuming application as to how to show it.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The message type used when the service layer request needs to provide additional information.
        /// </summary>
        Info,

        /// <summary>
        /// A message that is typically returned every time some sort of service layer request is successful.
        /// </summary>
        Success,

        /// <summary>
        /// A message that is typically returned when a service layer request is successful, but there's a warning.
        /// </summary>
        Warning,

        /// <summary>
        /// A message indicating some sort of error occurred and the service layer request was not successful.
        /// </summary>
        Error
    }
}
