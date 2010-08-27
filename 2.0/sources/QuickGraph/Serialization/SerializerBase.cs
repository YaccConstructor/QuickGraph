using System;
using System.Xml;

namespace QuickGraph.Serialization
{
    public abstract class SerializerBase<TVertex,TEdge>
        where TEdge :IEdge<TVertex>
    {
        private bool emitDocumentDeclaration = true;

        public bool EmitDocumentDeclaration
        {
            get { return this.emitDocumentDeclaration; }
            set { this.emitDocumentDeclaration = value; }
        }
    }
}
