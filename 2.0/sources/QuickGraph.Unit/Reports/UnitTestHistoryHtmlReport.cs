using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.ComponentModel;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Reports
{
    public sealed class UnitTestHistoryHtmlReport : XslReportBase
    {
        private string entryAssemblyName;

        public UnitTestHistoryHtmlReport(IContainer container)
            :base(container,"html")
        {}

        public string EntryAssemblyName
        {
            get { return this.entryAssemblyName; }
            set { this.entryAssemblyName = value; }
        }

        protected override void GenerateXml(XmlTextWriter writer)
        {
            QuickGraph.Operations.OperationsResourceManager.DumpResources(
                Path.GetDirectoryName(this.OutputFileName)
                );
            UnitResourceManager.DumpResources(
                Path.GetDirectoryName(this.OutputFileName)
                );
            ReportHistory history = new ReportHistory(
                this.OutputFolderName, this.EntryAssemblyName);
            XmlDocument doc = history.LoadReportHistory();
            doc.Save(writer);
        }

        protected override void TransformXml(XPathDocument document, XmlTextWriter writer)
        {
            XsltArgumentList args = new XsltArgumentList();

            UnitResourceManager.HtmlHistoryReport.Transform(document, args, writer);
            writer.Close();
        }
    }
}
