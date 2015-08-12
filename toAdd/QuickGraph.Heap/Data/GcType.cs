using System;
using System.Xml.Serialization;

namespace QuickGraph.Heap.Data
{
    public struct GcType
    {
        private readonly int id;
        private readonly string name;

        public GcType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Name, this.ID);
        }
    }
}
