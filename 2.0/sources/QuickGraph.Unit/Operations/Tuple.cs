using System;
using System.Collections;
using System.IO;

namespace QuickGraph.Operations
{
    public sealed class Tuple : CollectionBase, 
        ITuple, 
        IEquatable<ITuple>,
        IComparable<ITuple>
    {
        public Tuple()
        {}

        public object this[int index]
        {
            get
            {
                return this.List[index];
            }
        }

        public void Add(Object o)
        {
            this.InnerList.Add(o);
        }

        public void Concat(ITuple tuple)
        {
            foreach (Object o in tuple)
                this.Add(o);
        }

        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            foreach (object item in this)
                sw.Write("{0}, ", item);
            return sw.ToString().TrimEnd(',',' ');
        }

        // override object.Equals
        public bool Equals(ITuple tuple)
        {
            if (tuple == null)
                return false;
            return this.CompareTo(tuple) == 0;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (object o in this)
            {
                if (o != null)
                    hash += o.GetHashCode();
            }
            return hash;
        }

        public int CompareTo(object tuple)
        {
            return this.CompareTo(tuple as ITuple);
        }

        public int CompareTo(ITuple tuple)
        {
            if (((object)tuple) == null)
                return -1;
            if (this.Count < tuple.Count)
                return -1;
            else if (this.Count > tuple.Count)
                return 1;
            for (int i = 0; i < this.Count; ++i)
            {
                int c = Comparer.Default.Compare(this[i], tuple[i]);
                if (c != 0)
                    return c;
            }
            return 0;
        }

        public Object[] ToObjectArray()
        {
            object[] objs = new object[this.Count];
            this.List.CopyTo(objs, 0);
            return objs;
        }
    }
}

