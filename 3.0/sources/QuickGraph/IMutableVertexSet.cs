using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableVertexSet<TVertex>
        : IVertexSet<TVertex>
    {
        event VertexEventHandler<TVertex> VertexAdded;
        void AddVertex(TVertex v);
        void AddVertexRange(IEnumerable<TVertex> vertices);

        event VertexEventHandler<TVertex> VertexRemoved;
        bool RemoveVertex(TVertex v);
        int RemoveVertexIf(VertexPredicate<TVertex> pred);
    }
}
