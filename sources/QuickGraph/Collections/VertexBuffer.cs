namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class VertexBuffer<TVertex> : List<TVertex>
    {
        public TVertex Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Buffer is empty");
            return this[0];
        }

        public void Push(TVertex v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            this.Add(v);
        }

        public TVertex Pop()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Buffer is empty");
            TVertex v = this[0];
            this.RemoveAt(0);
            return v;
        }
    }
}
