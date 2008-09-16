using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuickGraph
{
    public class IdentifiableVertex : IIdentifiable
    {
        private string id;

        public IdentifiableVertex(string id)
        {
            this.id = id;
        }

        public string ID
        {
            get { return this.id; }
        }

        public override string ToString()
        {
            return this.id;
        }
    }

    public sealed class IdentifiableVertexFactory : IIdentifiableVertexFactory<IdentifiableVertex>
    {
        public IdentifiableVertex CreateVertex(string id)
        {
            return new IdentifiableVertex(id);
        }
    }
}
