namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class VertexBuffer<Vertex> : List<Vertex>
    {
        public Vertex Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Buffer is empty");
            return this[0];
        }

        public void Push(Vertex v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            this.Add(v);
        }

        public Vertex Pop()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Buffer is empty");
            Vertex v = this[0];
            this.RemoveAt(0);
            return v;
        }
    }
}
