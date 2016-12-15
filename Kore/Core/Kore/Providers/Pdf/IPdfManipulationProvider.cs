// ***********************************************************************
// <copyright file="IPdfManipulationProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;

namespace Kore.Providers.Pdf
{
    /// <summary>
    /// A basic interface for manipulating a PDF.
    /// </summary>
    public interface IPdfManipulationProvider
    {
        /// <summary>
        /// Merges the PDFS.
        /// </summary>
        /// <param name="pdfsToMerge">The PDFS to merge.</param>
        /// <returns>The merged PDF.</returns>
        byte[] MergePdfs(IEnumerable<byte[]> pdfsToMerge);
    }
}
