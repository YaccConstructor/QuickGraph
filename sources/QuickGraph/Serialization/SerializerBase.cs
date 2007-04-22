using System;
using System.Xml;

namespace QuickGraph.Serialization
{
    public abstract class SerializerBase<Vertex,Edge>
        where Edge :IEdge<Vertex>
    {
        private bool emitDocumentDeclaration = true;

        public bool EmitDocumentDeclaration
        {
            get { return this.emitDocumentDeclaration; }
            set { this.emitDocumentDeclaration = value; }
        }
    }
}
