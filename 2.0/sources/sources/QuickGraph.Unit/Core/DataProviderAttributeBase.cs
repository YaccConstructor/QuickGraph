using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace QuickGraph.Unit.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public abstract class DataProviderAttributeBase : Attribute,
        IDataProvider
    {
        public abstract IXPathNavigable GetData();
        public abstract string Name { get;}
    }
}
