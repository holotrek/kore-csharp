// ***********************************************************************
// <copyright file="SSRSReportProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.Reporting.WebForms;

namespace Kore.Providers.Reporting.SSRS
{
    /// <summary>
    /// A provider for rendering an SSRS RDLC report.
    /// </summary>
    public class SSRSReportProvider : IReportProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSRSReportProvider"/> class.
        /// </summary>
        public SSRSReportProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SSRSReportProvider"/> class.
        /// </summary>
        /// <param name="reportFolder">The report folder.</param>
        public SSRSReportProvider(string reportFolder)
            : this()
        {
            this.ReportFolder = reportFolder;
        }

        /// <summary>
        /// Gets the report folder.
        /// </summary>
        public string ReportFolder { get; private set; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        public string Encoding { get; private set; }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the streams.
        /// </summary>
        public IEnumerable<string> Streams { get; private set; }

        /// <summary>
        /// Gets the warnings.
        /// </summary>
        public IEnumerable<string> Warnings { get; private set; }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <returns>The report contents.</returns>
        public byte[] RenderReport(string reportName, string reportFormat, IDictionary<string, IEnumerable> dataSources)
        {
            return this.RenderReport(reportName, reportFormat, dataSources, null);
        }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The report contents.</returns>
        public byte[] RenderReport(string reportName, string reportFormat, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters)
        {
            using (LocalReport rpt = new LocalReport
            {
                ReportPath = Path.Combine(this.ReportFolder, reportName),
                EnableExternalImages = true
            })
            {
                return this.RenderReport(rpt, reportFormat, dataSources, parameters, null);
            }
        }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportDefinition">The report definition.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        public byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, IEnumerable> dataSources)
        {
            return this.RenderReport(reportDefinition, reportFormat, dataSources, null);
        }

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
        public byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters)
        {
            return this.RenderReport(reportDefinition, reportFormat, null, dataSources, parameters);
        }

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
        public byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, Stream> subreportDefinitions, IDictionary<string, IEnumerable> dataSources)
        {
            return this.RenderReport(reportDefinition, reportFormat, subreportDefinitions, dataSources, null);
        }

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
        public byte[] RenderReport(Stream reportDefinition, string reportFormat, IDictionary<string, Stream> subreportDefinitions, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters)
        {
            using (LocalReport rpt = new LocalReport
            {
                EnableExternalImages = true
            })
            {
                rpt.LoadReportDefinition(reportDefinition);
                return this.RenderReport(rpt, reportFormat, dataSources, parameters, subreportDefinitions);
            }
        }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="dataSources">The data sources.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="subreportDefinitions">The sub-report definitions.</param>
        /// <returns>
        /// The report contents.
        /// </returns>
        private byte[] RenderReport(LocalReport report, string reportFormat, IDictionary<string, IEnumerable> dataSources, IDictionary<string, string> parameters, IDictionary<string, Stream> subreportDefinitions)
        {
            if (dataSources != null)
            {
                foreach (KeyValuePair<string, IEnumerable> ds in dataSources)
                {
                    ReportDataSource rds = new ReportDataSource(ds.Key, ds.Value);
                    report.DataSources.Add(rds);
                }
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    report.SetParameters(new ReportParameter(param.Key, param.Value));
                }
            }

            if (subreportDefinitions != null)
            {
                foreach (KeyValuePair<string, Stream> sub in subreportDefinitions)
                {
                    report.LoadSubreportDefinition(sub.Key, sub.Value);
                }

                report.SubreportProcessing += Report_SubreportProcessing;
            }

            string mimeType, encoding, extension;
            string[] streams;
            Warning[] warnings;

            byte[] reportContents = report.Render(reportFormat, null, out mimeType, out encoding, out extension, out streams, out warnings);
            this.MimeType = mimeType;
            this.Encoding = encoding;
            this.Extension = extension;
            this.Streams = streams.ToList();
            this.Warnings = warnings.Select(x => x.Message).ToList();

            return reportContents;
        }

        /// <summary>
        /// Handles the SubreportProcessing event of the Report control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Wording is pulled from event delegate.")]
        private void Report_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            foreach (ReportDataSource rds in ((LocalReport)sender).DataSources)
            {
                e.DataSources.Add(rds);
            }
        }
    }
}
