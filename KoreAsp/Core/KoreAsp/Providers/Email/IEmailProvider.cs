// ***********************************************************************
// <copyright file="IEmailProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Net.Mail;

namespace KoreAsp.Providers.Email
{
    /// <summary>
    /// A contract for sending an email.
    /// </summary>
    public interface IEmailProvider
    {
        /// <summary>
        /// Gets or sets the address collection that contains the carbon copy (CC) recipients for this e-mail message.
        /// </summary>
        /// <value>The cc.</value>
        IEnumerable<MailAddress> CC { get; set; }

        /// <summary>
        /// Gets or sets the address collection that contains the blind carbon copy(BCC) recipients for this e-mail message.
        /// </summary>
        /// <value>The BCC.</value>
        IEnumerable<MailAddress> BCC { get; set; }

        /// <summary>
        /// Gets or sets the attachment collection used to store data attached to this e-mail message.
        /// </summary>
        /// <value>The attachments.</value>
        IEnumerable<FileAttachment> Attachments { get; set; }

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery. Use properties for optional
        /// inputs such as CC, BCC and Attachments.
        /// </summary>
        /// <param name="to">The mail to address.</param>
        /// <param name="subject">Subject line for this e-mail message.</param>
        /// <param name="body">The message body.</param>
        void Send(List<MailAddress> to, string subject, string body);

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery. Use properties for optional
        /// inputs such as CC, BCC and Attachments.
        /// </summary>
        /// <param name="to">The mail to address.</param>
        /// <param name="subject">Subject line for this e-mail message.</param>
        /// <param name="body">The message body.</param>
        /// <param name="mailFrom">The mail from email address</param>
        void Send(List<MailAddress> to, string subject, string body, string mailFrom);
    }
}