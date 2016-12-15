// ***********************************************************************
// <copyright file="EmailConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Net;
using System.Net.Mail;
using System.Text;

namespace Kore.Providers.Email
{
    /// <summary>
    /// Provides for the ability to configure the email being sent.
    /// </summary>
    public class EmailConfiguration
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfiguration"/> class.
        /// </summary>
        public EmailConfiguration()
        {
            this.SMTPCredentials = new NetworkCredential();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the SMTP host endpoint
        /// </summary>
        /// <value>The SMTP host.</value>
        public string SMTPHost { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the SMTP host endpoint port
        /// </summary>
        /// <value>The SMTP port.</value>
        public int SMTPPort { get; set; } = 25;

        /// <summary>
        /// Gets or sets the from address for this e-mail message.
        /// </summary>
        /// <value>The mail from.</value>
        public MailAddress MailFrom { get; set; }

        /// <summary>
        /// Gets or sets the sender's address for this e-mail message.
        /// </summary>
        /// <value>The sender.</value>
        public MailAddress Sender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mail message body is in Html.
        /// </summary>
        /// <value><c>true</c> if this instance is HTML message; otherwise, <c>false</c>.</value>
        public bool IsHTMLMessage { get; set; } = true;

        /// <summary>
        /// Gets or sets the encoding used for the subject content for this e-mail message.
        /// </summary>
        /// <value>The subject encoding.</value>
        public Encoding SubjectEncoding { get; set; } = Encoding.ASCII;

        /// <summary>
        /// Gets or sets the encoding used to encode the message body.
        /// </summary>
        /// <value>The body encoding.</value>
        public Encoding BodyEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Gets or sets the priority of this e-mail message
        /// </summary>
        /// <value>The priority.</value>
        public MailPriority Priority { get; set; } = MailPriority.Normal;

        /// <summary>
        /// Gets or sets a value indicating whether or not credentials need to be passed to the host
        /// </summary>
        /// <value><c>true</c> if [use credentials]; otherwise, <c>false</c>.</value>
        public bool UseCredentials { get; set; }

        /// <summary>
        /// Gets or sets network credentials to pass to the SMTP host
        /// </summary>
        /// <value>The SMTP credentials.</value>
        public NetworkCredential SMTPCredentials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the SMTP Client uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        /// <value><c>true</c> if [enable SSL]; otherwise, <c>false</c>.</value>
        public bool EnableSsl { get; set; }

        #endregion
    }
}