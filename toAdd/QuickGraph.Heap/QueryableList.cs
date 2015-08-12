using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QuickGraph.Heap
{
    public abstract class QueryableList<T> : List<T>
    {
        protected QueryableList(IEnumerable<T> items)
            : base(items)
        { }

        protected QueryableList(int capacity)
            : base(capacity)
        { }

        protected abstract QueryableList<T> Create(int capacity); 

        public QueryableList<T> Top(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("must be posivite", "count");

            QueryableList<T> list = this.Create(count);
            int len = Math.Min(count, this.Count);
            for (int i = 0; i < len; ++i)
                list.Add(this[i]);
            return list;
        }

        public QueryableList<T> Bottom(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("must be posivite", "count");

            QueryableList<T> list = this.Create(count);
            int len = Math.Max(this.Count - count, 0);
            for (int i = this.Count - 1; i >= len; --i)
                list.Add(this[i]);
            return list;
        }

        public override string ToString()
        {
            using (StringWriter writer = new StringWriter())
            {
                for(int i = this.Count -1;i>=0;--i)
                    writer.WriteLine(this[i]);
                return writer.ToString();
            }
        }
    }
}
