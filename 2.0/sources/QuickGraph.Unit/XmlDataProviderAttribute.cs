using System;
using System.Xml.XPath;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public sealed class XmlDataProviderAttribute : DataProviderAttributeBase
    {
        private string fileName;
        private XPathDocument data;

        public XmlDataProviderAttribute(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");
            this.fileName = fileName;
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public override IXPathNavigable GetData()
        {
            if (this.data == null)
                this.data = new XPathDocument(this.FileName);
            return this.data;
        }

        public override string Name
        {
            get { return System.IO.Path.GetFileName(this.FileName); }
        }
    }
}
