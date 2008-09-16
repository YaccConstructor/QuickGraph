using System;
using System.Collections;

namespace QuickGraph.Operations
{
    public abstract class DomainBase : IDomain
    {
        private string name = null;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public abstract int Count { get;}
        public abstract IEnumerator GetEnumerator();
        public abstract Object this[int index] { get;}
    }
}
