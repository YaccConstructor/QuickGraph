using System.Collections;
using System;

namespace QuickGraph.Operations
{
    public sealed class EmptyDomain : IDomain
    {
        private string name = null;
        public EmptyDomain()
        {}

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name=value;
            }
        }

        public IDomain Boundary
        {
            get
            {
                return this;
            }
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public object this[int i]
        {
            get
            {
                throw new InvalidOperationException("Empty domain");
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new ArrayList().GetEnumerator();
        }
    }
}
