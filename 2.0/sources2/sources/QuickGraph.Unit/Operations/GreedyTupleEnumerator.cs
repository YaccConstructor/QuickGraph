using System;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    internal sealed class GreedyTupleEnumerable : IEnumerable<ITuple>
    {
        private IEnumerable<ITuple> enumerable;
        public GreedyTupleEnumerable(IEnumerable<ITuple> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            this.enumerable = enumerable;
        }

        public IEnumerator<ITuple> GetEnumerator()
        {
            return new GreedyTupleEnumerator(this.enumerable.GetEnumerator());
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class GreedyTupleEnumerator : IEnumerator<ITuple>
        {
            private Dictionary<ITuple,ITuple> tuples = new Dictionary<ITuple,ITuple>();
            private IEnumerator<ITuple> enumerator;

            public GreedyTupleEnumerator(IEnumerator<ITuple> enumerator)
            {
                this.enumerator = enumerator;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public bool MoveNext()
            {
                while (this.enumerator.MoveNext())
                {
                    if (this.tuples.ContainsKey(this.Current))
                        continue;
                    this.tuples.Add(this.Current, null);
                    return true;
                }
                return false;
            }

            public ITuple Current
            {
                get
                {
                    return this.enumerator.Current;
                }
            }

            Object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
                if (this.enumerator != null)
                {
                    this.enumerator.Dispose();
                    this.enumerator = null;
                }
                this.tuples = null;
            }
        }
    }
}
