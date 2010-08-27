using System;
using System.Collections;

namespace QuickGraph.Operations
{
    public sealed class ArrayDomain : DomainBase
    {
        private Array array;

        public ArrayDomain(Array array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            this.array = array;
        }

        public override int Count
        {
            get { return array.Length; }
        }

        public override IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }

        public override Object this[int index]
        {
            get { return this.array.GetValue(index); }
        }
    }

    public sealed class ArrayDomain<T> : DomainBase
    {
        private T[] array;

        public ArrayDomain(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            this.array = array;
        }

        public override int Count
        {
            get { return array.Length; }
        }

        public override IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }

        public override Object this[int index]
        {
            get { return this.array.GetValue(index); }
        }
    }
}
