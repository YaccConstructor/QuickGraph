using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace QuickGraph.Unit.Reports
{
    public abstract class XslReportBase : FileReportBase
    {
        private string outputXmlFileName;

        public XslReportBase(
            IContainer container, 
            string defaultFileExtension)
            :base(container,defaultFileExtension)
        {}

        public string OutputXmlFileName
        {
            get { return this.outputXmlFileName; }
        }

        public override void SetOutputFileName(string outputFileName)
        {
            base.SetOutputFileName(outputFileName);
            this.outputXmlFileName = this.OutputFileName + ".xml";
        }

        public sealed override void Generate(TextWriter writer)
        {
            using (StreamWriter xwriter = new StreamWriter(this.OutputXmlFileName))
            {
                using (XmlTextWriter xxwriter = new XmlTextWriter(xwriter))
                {
                    xxwriter.Formatting = Formatting.Indented;
                    GenerateXml(xxwriter);
                }
            }

            XPathDocument doc = new XPathDocument(this.OutputXmlFileName);
            using (XmlTextWriter xwriter = new XmlTextWriter(writer))
            {
                xwriter.Formatting = Formatting.Indented;
                TransformXml(doc,xwriter);
            }
        }

        protected abstract void GenerateXml(XmlTextWriter writer);
        protected abstract void TransformXml(XPathDocument document, XmlTextWriter writer);
    }
}
