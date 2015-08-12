using System;
using System.Collections.Generic;

namespace QuickGraph.Heap.Data
{
    public sealed class AddressList : List<long>
    {
        public AddressList()
            : base(0)
        { }
    }

    public sealed class GcObject
    {
        private readonly long address;
        private readonly int typeID;
        private readonly int size;
        private AddressList members;

        public GcObject(long address, int typeID, int size)
        {
            this.address = address;
            this.typeID = typeID;
            this.size = size;
        }

        public long Address
        {
            get { return this.address; }
        }

        public int TypeID
        {
            get { return this.typeID; }
        }

        public int Size
        {
            get { return this.size; }
        }

        public AddressList Members
        {
            get
            {
                if (this.members == null)
                    this.members = new AddressList();
                return this.members;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}({1})", this.address, this.TypeID);
        }
    }
}
