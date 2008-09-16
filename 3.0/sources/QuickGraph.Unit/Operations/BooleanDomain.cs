using System;

namespace QuickGraph.Operations
{
    public sealed class BooleanDomain : DomainBase
    {
        private bool[] values = new bool[] { true, false };
        public BooleanDomain()
        {}

        public override int Count
        {
            get { return 2; }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public override Object this[int index]
        {
            get 
            {
                switch (index)
                {
                    case 0: return this.values[0];
                    case 1: return this.values[1];
                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
    }
}
