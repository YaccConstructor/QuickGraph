using System;
using System.Reflection;
using System.Xml.XPath;
using System.IO;

namespace QuickGraph.Unit.Core
{
    public sealed class DataTestCase  :MethodTestCase
    {
        public DataTestCase(
            string fixtureName,
            MethodInfo method,
            XPathNavigator node,
            string nodeName
            )
            : base(fixtureName, method)
        {
            this.Parameters.Add(new TestCaseParameter(nodeName, node));
        }
    }
}
