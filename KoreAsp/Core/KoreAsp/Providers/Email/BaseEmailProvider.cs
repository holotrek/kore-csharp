// ***********************************************************************
// <copyright file="BaseEmailProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Net.Mail;

namespace KoreAsp.Providers.Email
{
    /// <summary>
    /// Base functionality for the email provider.
    /// </summary>
    /// <seealso cref="KoreAsp.Providers.Email.IEmailProvider" />
    public abstract class BaseEmailProvider : IEmailProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEmailProvider"/> class.
        /// </summary>
        public BaseEmailProvider()
        {
            this.CC = new List<MailAddress>();
            this.BCC = new List<MailAddress>();
            this.Attachments = new List<FileAttachment>();
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the address collection that contains the carbon copy (CC) recipients for this e-mail message.
        /// </summary>
        /// <value>The cc.</value>
        public virtual IEnumerable<MailAddress> CC { get; set; }

        /// <summary>
        /// Gets or sets the address collection that contains the blind carbon copy(BCC) recipients for this e-mail message.
        /// </summary>
        /// <value>The BCC.</value>
        public virtual IEnumerable<MailAddress> BCC { get; set; }

        /// <summary>
        /// Gets or sets the attachment collection used to store data attached to this e-mail message.
        /// </summary>
        /// <value>The attachments.</value>
        public virtual IEnumerable<FileAttachment> Attachments { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery. Use properties for optional
        /// inputs such as CC, BCC and Attachments.
        /// </summary>
        /// <param name="to">The mail to address.</param>
        /// <param name="subject">Subject line for this e-mail message.</param>
        /// <param name="body">The message body.</param>
        public abstract void Send(List<MailAddress> to, string subject, string body);

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery. Use properties for optional
        /// inputs such as CC, BCC and Attachments.
        /// </summary>
        /// <param name="to">The mail to address.</param>
        /// <param name="subject">Subject line for this e-mail message.</param>
        /// <param name="body">The message body.</param>      
        /// <param name="mailFrom">The email from address</param>
        public abstract void Send(List<MailAddress> to, string subject, string body, string mailFrom);

        /// <summary>
        /// Configures the server and encoding of the mail message and sends it immediately.
        /// </summary>
        /// <param name="configuration">Server and encoding configuration</param>
        /// <param name="to">Collection that contains the recipients of this e-mail message</param>
        /// <param name="subject">Subject line for this e-mail message.</param>
        /// <param name="body">The message body.</param>
        protected virtual void SendEmail(EmailConfiguration configuration, List<MailAddress> to, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.BodyEncoding = configuration.BodyEncoding;
                mail.SubjectEncoding = configuration.SubjectEncoding;
                mail.From = configuration.MailFrom;
                mail.Sender = configuration.Sender;
                mail.IsBodyHtml = configuration.IsHTMLMessage;
                mail.Priority = configuration.Priority;

                foreach (MailAddress address in to)
                {
                    mail.To.Add(address);
                }

                if (this.CC != null)
                {
                    foreach (MailAddress address in this.CC)
                    {
                        mail.CC.Add(address);
                    }
                }

                if (this.BCC != null)
                {
                    foreach (MailAddress address in this.BCC)
                    {
                        mail.Bcc.Add(address);
                    }
                }

                mail.Body = body;
                mail.Subject = subject;

                if (Attachments != null)
                {
                    foreach (FileAttachment ma in this.Attachments)
                    {
                        mail.Attachments.Add(ma.File);
                    }
                }
                
                using (SmtpClient smtp = new SmtpClient())
                {
                    if (configuration.UseCredentials)
                    {
                        smtp.Credentials = configuration.SMTPCredentials;
                    }

                    smtp.Host = configuration.SMTPHost;
                    smtp.Port = configuration.SMTPPort;
                    smtp.EnableSsl = configuration.EnableSsl;
                    smtp.Send(mail);
                }
            }
        }

        #endregion
    }
}