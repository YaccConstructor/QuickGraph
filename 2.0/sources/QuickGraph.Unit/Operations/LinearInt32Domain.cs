using System;
using System.Collections;

namespace QuickGraph.Operations
{
    public sealed class LinearInt32Domain : IDomain
    {
        private IDomain boundary = null;
        private string name = null;
        private int start;
        private int stepCount;
        private int step;

        public LinearInt32Domain(int start, int stepCount)
            :this(start,stepCount,1)
        {}
        public LinearInt32Domain(int start, int stepCount,int step)
        {
            this.start = start;
            this.step = step;
            this.stepCount = stepCount;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Count
        {
            get
            {
                return this.stepCount;
            }
        }

        public IDomain Boundary
        {
            get
            {
                if (this.boundary == null)
                {
                    this.boundary = new CollectionDomain(new int[] { this[0], this[this.stepCount - 1] });
                }
                return this.boundary;
            }
        }

        public int this[int index]
        {
            get
            {
                if (index >= this.stepCount)
                    throw new ArgumentOutOfRangeException("Index is greater or equal to count");
                return this.start + index * this.step;
            }
        }
        Object IDomain.this[int index]
        {
            get
            {
                return this[index];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new LinearIntEnumerator(this.stepCount, this.stepCount, this.step);
        }

        public class LinearIntEnumerator : IEnumerator
        {
            private int start;
            private int step;
            private int stepCount;
            private int index = -1;
            public LinearIntEnumerator(int start, int stepCount,int step)
            {
                this.start = start;
                this.step = step;
                this.stepCount = stepCount;
            }

            public void Reset()
            {
                this.index = -1;
            }

            public bool MoveNext()
            {
                this.index++;
                return this.index < this.stepCount;
            }

            public int Current
            {
                get
                {
                    if (this.index < 0)
                        throw new InvalidOperationException("MoveNext was not called");
                    if (this.index >= this.stepCount)
                        throw new InvalidOperationException("Enumeration out of range");
                    return this.start + this.index * this.step;
                }
            }

            Object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }
    }
}
