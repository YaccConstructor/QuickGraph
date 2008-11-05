using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("{ID}")]
    public class IdentifiableVertex 
        : IIdentifiable
    {
        private readonly string id;

        public IdentifiableVertex(string id)
        {
            CodeContract.Requires(id!=null);

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
}
