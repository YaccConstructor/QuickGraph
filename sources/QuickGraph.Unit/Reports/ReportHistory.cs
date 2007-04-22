using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace QuickGraph.Unit.Reports
{
    public sealed class ReportHistory
    {
        private string reportOutputPath;
        private string testAssemblyName;

        public ReportHistory(string reportOutputPath, string testAssemblyName)
        {
            this.reportOutputPath = reportOutputPath;
            this.testAssemblyName = testAssemblyName;
        }

        public string ReportOutputPath
        {
            get { return this.reportOutputPath; }
        }

        public string TestAssemblyName
        {
            get { return this.testAssemblyName; }
        }

        public string GetLatestXmlReport()
        {
            List<string> reports = new List<string>(GetPreviousXmlReports());
            if (reports.Count == 0)
                return null;
            reports.Sort(new CreationTimeComparer());
            return reports[reports.Count - 1];
        }

        public IEnumerable<string> GetPreviousXmlReports()
        {
            string searchPattern = String.Format("{0}.html.xml", ReportPath.EscapeAssemblyName(TestAssemblyName));
            searchPattern = ReportPath.EscapeFileName(searchPattern);
            // look for the report file
            foreach (string xmlreport in Directory.GetFiles(
                    this.ReportOutputPath,
                    searchPattern,
                    SearchOption.AllDirectories
                    ))
            {
                yield return xmlreport;
            }
        }

        public XmlDocument LoadReportHistory()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("MergedBatch");
            document.AppendChild(root);
            XmlElement testBatches = document.CreateElement("TestBatches");
            root.AppendChild(testBatches);

            foreach (string previousResult in this.GetPreviousXmlReports())
            {
                try
                {
                    using (StreamReader reader = new StreamReader(previousResult))
                    {
                        XmlDocument previousDocument = new XmlDocument();
                        previousDocument.Load(reader);
                        string path = Path.GetFullPath(previousResult);
                        path = path.Substring(0, path.Length - ".xml".Length);
                        previousDocument.DocumentElement.SetAttribute("Path", path);
                        testBatches.AppendChild(
                            document.ImportNode(previousDocument.DocumentElement, true)
                            );
                    }
                }
                catch (XmlException ex)
                {
                    Assert.Logger.LogWarning("Error while loading {0}: {1}",
                        previousResult,
                        ex.Message);
                }
            }

            return document;
        }

        private sealed class CreationTimeComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                FileInfo fx = new FileInfo(x);
                FileInfo fy = new FileInfo(y);
                return fx.CreationTime.CompareTo(fy.CreationTime);
            }
        }
    }
}
