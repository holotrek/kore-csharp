// ***********************************************************************
// <copyright file="FileAttachment.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.IO;
using System.Net.Mail;

namespace Kore.Providers.Email
{
    /// <summary>
    /// Represents an in memory file to attach to an email.
    /// </summary>
    public class FileAttachment
    {
        #region Fields

        /// <summary>
        /// The filename
        /// </summary>
        private string filename;

        /// <summary>
        /// The data array
        /// </summary>
        private byte[] dataArray;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the data stream for this attachment as a MemoryStream
        /// </summary>
        /// <value>The data.</value>
        public Stream Data
        {
            get
            {
                return new MemoryStream(dataArray);
            }
        }

        /// <summary>
        /// Gets the data stream for this attachment as a ByteArray
        /// </summary>
        /// <value>The data array.</value>
        public byte[] DataArray
        {
            get
            {
                return dataArray;
            }
        }

        /// <summary>
        /// Gets the original filename for this attachment
        /// </summary>
        /// <value>The filename.</value>
        public string Filename
        {
            get
            {
                return filename;
            }
        }

        /// <summary>
        /// Gets the file for this attachment (as a new attachment)
        /// </summary>
        /// <value>The file.</value>
        public Attachment File
        {
            get
            {
                return new Attachment(Data, Filename);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachment"/> class. Construct a mail attachment form a byte array.
        /// </summary>
        /// <param name="data">Bytes to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public FileAttachment(byte[] data, string filename)
        {
            this.dataArray = data;
            this.filename = filename;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachment"/> class. Construct a mail attachment from a string.
        /// </summary>
        /// <param name="data">String to attach as a file</param>
        /// <param name="filename">Logical filename for attachment</param>
        public FileAttachment(string data, string filename)
        {
            this.dataArray = System.Text.Encoding.ASCII.GetBytes(data);
            this.filename = filename;
        }

        #endregion
    }
}