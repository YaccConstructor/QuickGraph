using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IEdgeListGraph<,>))]
    abstract class IEdgeListGraphContract<TVertex, TEdge>
        : IEdgeListGraph<TVertex, TEdge>
      where TEdge : IEdge<TVertex>  
    {
        #region IGraph<TVertex,TEdge> Members
        bool IGraph<TVertex, TEdge>.IsDirected
        {
            get { throw new NotImplementedException(); }
        }

        bool IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEdgeSet<TVertex,TEdge> Members

        bool IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            get { throw new NotImplementedException(); }
        }

        int IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            get { throw new NotImplementedException(); }
        }

        System.Collections.Generic.IEnumerable<TEdge> IEdgeSet<TVertex, TEdge>.Edges
        {
            get { throw new NotImplementedException(); }
        }

        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IVertexSet<TVertex> Members

        public bool IsVerticesEmpty {
          get { throw new NotImplementedException(); }
        }

        public int VertexCount {
          get { throw new NotImplementedException(); }
        }

        public System.Collections.Generic.IEnumerable<TVertex> Vertices {
          get { throw new NotImplementedException(); }
        }

        #endregion

        #region IImplicitVertexSet<TVertex> Members

        public bool ContainsVertex(TVertex vertex) {
          throw new NotImplementedException();
        }

        #endregion
    }
}
