// ***********************************************************************
// <copyright file="TextSharpPdfManipulationProvider.cs" company="Holotrek">
//     Copyright (c) Holotrek. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KoreAsp.Providers.Pdf
{
    /// <summary>
    /// A provider for rendering an SSRS RDLC report.
    /// </summary>
    public class TextSharpPdfManipulationProvider : IPdfManipulationProvider
    {
        /// <summary>
        /// Merges the PDFS.
        /// </summary>
        /// <param name="pdfsToMerge">The PDFS to merge.</param>
        /// <returns>The merged PDF's resulting bytes.</returns>
        public byte[] MergePdfs(IEnumerable<byte[]> pdfsToMerge)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    using (PdfSmartCopy copy = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();

                        // Loop through each byte array
                        foreach (byte[] p in pdfsToMerge)
                        {
                            // Create a PdfReader bound to that byte array
                            using (PdfReader reader = new PdfReader(p))
                            {
                                // Add the entire document instead of page-by-page
                                copy.AddDocument(reader);
                            }
                        }

                        doc.Close();
                    }
                }

                // Return just before disposing
                return ms.ToArray();
            }
        }
    }
}
