using System;
using System.Xml.XPath;

namespace QuickGraph.Unit
{
    public interface IDataProvider
    {
        string Name { get;}
        IXPathNavigable GetData();
    }
}
