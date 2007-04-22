namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class VertexBuffer<Vertex> : List<Vertex>
    {
        public Vertex Peek()
        {
            return this[0];
        }

        public void Push(Vertex v)
        {
            this.Add(v);
        }

        public Vertex Pop()
        {
            Vertex v = this[0];
            this.RemoveAt(0);
            return v;
        }
    }
}
