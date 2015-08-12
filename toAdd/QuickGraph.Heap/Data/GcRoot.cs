using System;

namespace QuickGraph.Heap.Data
{
    public struct GcRoot
    {
        private readonly long address;
        private readonly string kind;

        public GcRoot(long address, string kind)
        {
            this.address = address;
            this.kind = kind;
        }

        public long Address
        {
            get { return this.address; }
        }

        public string Kind
        {
            get { return this.kind; }
        }

        public override string ToString()
        {
            return String.Format("{0}({1})", this.Address, this.Kind);
        }
    }
}
