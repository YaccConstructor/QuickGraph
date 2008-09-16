using System;
using System.Collections;

namespace QuickGraph.Operations
{
    internal sealed class StringDomain : DomainBase
    {
        private string value;
        public StringDomain(string value)
        {
            this.value = value;
        }
        public override int Count
        {
            get { return 1; }
        }
        public override Object this[int i]
        {
            get 
            {
                if (i != 0)
                    throw new ArgumentOutOfRangeException("index out of range");
                return this.value;
            }
        }
        public override IEnumerator GetEnumerator()
        {
            ArrayList list = new ArrayList();
            list.Add(this.value);
            return list.GetEnumerator();
        }
    }
}
