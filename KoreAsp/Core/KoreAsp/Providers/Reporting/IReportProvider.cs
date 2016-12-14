// ***********************************************************************
// <copyright file="IReportProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace KoreAsp.Providers.Reporting
{
    /// <summary>
    /// A basic interface for rendering a report.
    /// </summary>
    public interface IReportProvider
    {
        /// <summary>
        /// Gets the report folder.
        /// </summary>
        string ReportFolder { get; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        string Encoding { get; }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the streams.
        /// </summary>
        IEnumerable<string> Streams { get; }

        /// <summary>
        /// Gets the warnings.
        /// </summary>
        IEnumerable<string> Warnings { get; }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <returns>The report contents.</returns>
        byte[] RenderReport(string reportName, string reportFormat, IDictionary<string, IEnumerable> dataSources);

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The report contents.</returns>
        byte[] RenderReport(string reportName, string reportFormat, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters);

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportDefinition">The report definition.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, IEnumerable> dataSources);

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportDefinition">The report definition.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters);

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportDefinition">The report definition.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="subreportDefinitions">The sub-report definitions.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, Stream> subreportDefinitions, IDictionary<string, IEnumerable> dataSources);

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportDefinition">The report definition.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="subreportDefinitions">The sub-report definitions.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, Stream> subreportDefinitions, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters);
    }

    /// <summary>
    /// The list of report formats that a report can be rendered to
    /// </summary>
    public static class ReportFormats
    {
        /// <summary>The PDF Report format.</summary>
        public static readonly string PDF = "PDF";
    }
}
