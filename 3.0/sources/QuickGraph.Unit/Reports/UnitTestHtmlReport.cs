using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections.Generic;
using System.ComponentModel;
using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Listeners;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Reports
{
    public sealed class UnitTestHtmlReport : XslReportBase
    {
        private XmlTestListener testListener = new XmlTestListener();
        private string entryAssemblyName;
        private bool generateFixtureInSeparateFile = false;
        private bool showFixturesSummary = true;

        public UnitTestHtmlReport(IContainer container)
            :base(container,"html")
        {}

        public bool GenerateFixtureInSeparateFile
        {
            get { return this.generateFixtureInSeparateFile; }
            set { this.generateFixtureInSeparateFile = value; }
        }

        public bool ShowFixturesSummary
        {
            get { return this.showFixturesSummary; }
            set { this.showFixturesSummary = value; }
        }

        public string EntryAssemblyName
        {
            get { return this.entryAssemblyName; }
            set { this.entryAssemblyName = value; }
        }

        public XmlTestListener TestListener
        {
            get { return this.testListener; }
            set { this.testListener = value; }
        }

        protected override void GenerateXml(XmlTextWriter writer)
        {
            QuickGraph.Operations.OperationsResourceManager.DumpResources(Path.GetDirectoryName(this.OutputFileName));
            UnitResourceManager.DumpResources(Path.GetDirectoryName(this.OutputFileName));
            UnitSerializer.TestBatchSerializer.Serialize(
                writer,
                this.TestListener.TestBatch);
        }

        protected override void TransformXml(XPathDocument document, XmlTextWriter writer)
        {
			XsltArgumentList args = new XsltArgumentList();
			args.AddParam("separate-fixtures", "", (this.GenerateFixtureInSeparateFile) ? 1 : 0);
            args.AddParam("show-fixtures-summary", "", (this.ShowFixturesSummary) ? 1 : 0);
            args.AddParam("creation-time", "", this.CreationTime.ToString("u"));

			if (this.GenerateFixtureInSeparateFile)
				this.GenerateFixtureReports(document, args);

            UnitResourceManager.HtmlReport.Transform(document, args, writer);
            writer.Close();

        }

		private void GenerateFixtureReports(XPathDocument document, XsltArgumentList args)
		{
			Console.WriteLine("Generating fixture reports");
			XPathNavigator navi = document.CreateNavigator();
			foreach (XPathNavigator node in navi.Select("//Fixture"))
			{
				Console.Write('.');
				string fileName = string.Format(
					"{0}_{1}.html",
					this.EntryAssemblyName.Replace('.', '_'),
					node.GetAttribute("Name", "").Replace('.', '_')
					);
				fileName = ReportPath.EscapeFileName(fileName);
				fileName = Path.Combine(this.OutputFolderName, fileName);

                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
				{
					using (StreamWriter fixtureWriter = new StreamWriter(fileName))
					{
						UnitResourceManager.HtmlFixtureReport.Transform(node, args, fixtureWriter);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed generating {0}", fileName);
					Console.WriteLine(ex.Message);
				}
			}

			Console.WriteLine();
		}
    }
}
