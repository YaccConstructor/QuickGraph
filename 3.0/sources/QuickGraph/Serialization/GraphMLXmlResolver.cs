using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics.Contracts;
using System.Net;

namespace QuickGraph.Serialization
{
    /// <summary>
    /// A resolver that loads graphml DTD and XSD schemas 
    /// from embedded resources.
    /// </summary>
    public sealed class GraphMLXmlResolver 
        : XmlUrlResolver
    {
        public const string GraphMLNamespace = "http://graphml.graphdrawing.org/xmlns";

#if !SILVERLIGHT
        ICredentials _credentials;
        public override ICredentials Credentials
        {
            set
            {
                this._credentials = value;
            }
        }
#endif
 
        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            if (absoluteUri.AbsoluteUri == "http://www.graphdrawing.org/dtds/graphml.dtd")
                return typeof(GraphMLExtensions).Assembly.GetManifestResourceStream(typeof(GraphMLExtensions), "graphml.dtd");
            else if (absoluteUri.AbsoluteUri == "http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd")
                return typeof(GraphMLExtensions).Assembly.GetManifestResourceStream(typeof(GraphMLExtensions), "graphml.xsd");
            else if (absoluteUri.AbsoluteUri == "http://graphml.graphdrawing.org/xmlns/1.0/graphml-structure.xsd")
                return typeof(GraphExtensions).Assembly.GetManifestResourceStream(typeof(GraphMLExtensions), "graphml-structure.xsd");

            return base.GetEntity(absoluteUri, role, ofObjectToReturn);
        }
    }
}
