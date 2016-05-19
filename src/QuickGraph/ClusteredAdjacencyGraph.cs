using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{

#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class ClusteredAdjacencyGraph<TVertex, TEdge>
        : IVertexAndEdgeListGraph<TVertex, TEdge>
        , IEdgeListAndIncidenceGraph<TVertex, TEdge>
        , IClusteredGraph
        where TEdge : IEdge<TVertex>
    {
        private ClusteredAdjacencyGraph<TVertex, TEdge> parent;
        private AdjacencyGraph<TVertex, TEdge> wrapped;
        private ArrayList clusters;
        private bool colapsed;

        public ClusteredAdjacencyGraph(AdjacencyGraph<TVertex, TEdge> wrapped)
        {
            if (wrapped == null)
                throw new ArgumentNullException("parent");
            this.parent = null;
            this.wrapped = wrapped;
            this.clusters = new ArrayList();
            this.colapsed = false;
        }


        public ClusteredAdjacencyGraph(ClusteredAdjacencyGraph<TVertex, TEdge> parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            this.parent = parent;
            this.wrapped = new AdjacencyGraph<TVertex, TEdge>(parent.AllowParallelEdges);
            this.clusters = new ArrayList();
        }


        public ClusteredAdjacencyGraph<TVertex, TEdge> Parent
        {
            get
            {
                return parent;
            }
        }


        public bool Colapsed
        {
            get
            {
                return colapsed;
            }
            set
            {
                colapsed = value;
            }
        }


        protected AdjacencyGraph<TVertex, TEdge> Wrapped
        {
            get
            {
                return wrapped;
            }
        }


        public bool IsDirected
        {
            get 
            { 
                return wrapped.IsDirected;
            }
        }

        public bool AllowParallelEdges
        {
            [Pure]
            get 
            {
                return wrapped.AllowParallelEdges;
            }
        }

        public int EdgeCapacity
        {
            get 
            {
                return wrapped.EdgeCapacity;
            }
            set 
            {
                wrapped.EdgeCapacity = value; 
            }
        }

        public static Type EdgeType
        {
            get 
            {
                return typeof(TEdge); 
            }
        }

        public bool IsVerticesEmpty
        {
            get 
            {
                return wrapped.IsVerticesEmpty; 
            }
        }

        public int VertexCount
        {
            get 
            { 
                return wrapped.VertexCount; 
            }
        }

        public virtual IEnumerable<TVertex> Vertices
        {
            get 
            {
                return wrapped.Vertices; 
            }
        }

        [Pure]
        public bool ContainsVertex(TVertex v)
        {
            return wrapped.ContainsVertex(v);
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            return wrapped.IsOutEdgesEmpty(v);
        }

        public int OutDegree(TVertex v)
        {
            return wrapped.OutDegree(v);
        }

        public virtual IEnumerable<TEdge> OutEdges(TVertex v)
        {
            return wrapped.OutEdges(v);
        }

        public virtual bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            return wrapped.TryGetOutEdges(v, out edges);
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            return wrapped.OutEdge(v, index);
        }


        public bool IsEdgesEmpty
        {
            [Pure]
            get 
            {
                return wrapped.IsEdgesEmpty;
            }
        }


        public int EdgeCount
        {
            get
            {
                return wrapped.EdgeCount;
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(wrapped.EdgeCount >= 0);
        }

        public virtual IEnumerable<TEdge> Edges
        {
            [Pure]
            get
            {
                return wrapped.Edges;
            }
        }

        [Pure]
        public bool ContainsEdge(TVertex source, TVertex target)
        {
            return wrapped.ContainsEdge(source, target);
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            return wrapped.ContainsEdge(edge);
        }

        [Pure]
        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            return wrapped.TryGetEdge(source, target, out edge);
        }

        [Pure]
        public virtual bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            return wrapped.TryGetEdges(source, target, out edges);
        }


        public IEnumerable Clusters
        {
            get
            {
                return clusters;
            }
        }


        public int ClustersCount
        {
            get
            {
                return clusters.Count;
            }
        }


        public ClusteredAdjacencyGraph<TVertex, TEdge> AddCluster()
        {
            ClusteredAdjacencyGraph<TVertex, TEdge> cluster = new ClusteredAdjacencyGraph<TVertex, TEdge>(this);
            clusters.Add(cluster);
            return cluster;
        }


        IClusteredGraph IClusteredGraph.AddCluster()
        {
            return this.AddCluster();
        }


        public void RemoveCluster(IClusteredGraph cluster)
        {
            if (cluster == null)
                throw new ArgumentNullException("cluster");
            clusters.Remove(cluster);
        }

        public virtual bool AddVertex(TVertex v)
        {
            if (!(parent == null || parent.ContainsVertex(v)))
            {
                parent.AddVertex(v);
                return wrapped.AddVertex(v);
            }
            else
                return wrapped.AddVertex(v);
        }

        public virtual int AddVertexRange(IEnumerable<TVertex> vertices)
        {
            int count = 0;
            foreach (var v in vertices)
                if (this.AddVertex(v))
                    count++;
            return count;
        }

        private void RemoveChildVertex(TVertex v)
        {
            foreach (ClusteredAdjacencyGraph<TVertex, TEdge> el in Clusters)
                if (el.ContainsVertex(v))
                {
                    el.Wrapped.RemoveVertex(v);
                    el.RemoveChildVertex(v);
                    break;
                }
        }
        public virtual bool RemoveVertex(TVertex v)
        {
            if (!wrapped.ContainsVertex(v))
                return false;
            RemoveChildVertex(v);
            wrapped.RemoveVertex(v);
            if (parent != null)
                parent.RemoveVertex(v);
            return true;
        }



        public int RemoveVertexIf(VertexPredicate<TVertex> predicate)
        {
            var vertices = new VertexList<TVertex>();
            foreach (var v in wrapped.Vertices)
                if (predicate(v))
                    vertices.Add(v);
            foreach (var v in vertices)
                this.RemoveVertex(v);
            return vertices.Count;
        }

        public virtual bool AddVerticesAndEdge(TEdge e)
        {
            this.AddVertex(e.Source);
            this.AddVertex(e.Target);
            return this.AddEdge(e);
        }


        public int AddVerticesAndEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddVerticesAndEdge(edge))
                    count++;
            return count;
        }

        public virtual bool AddEdge(TEdge e)
        {
            wrapped.AddEdge(e);
            if (parent != null && !parent.ContainsEdge(e))
                parent.AddEdge(e);
            return true;
        }

        public int AddEdgeRange(IEnumerable<TEdge> edges)
        {
            int count = 0;
            foreach (var edge in edges)
                if (this.AddEdge(edge))
                    count++;
            return count;
        }

        private void RemoveChildEdge(TEdge e)
        {
            foreach (ClusteredAdjacencyGraph<TVertex, TEdge> el in Clusters)
                if (el.ContainsEdge(e))
                {
                    el.Wrapped.RemoveEdge(e);
                    el.RemoveChildEdge(e);
                    break;
                }
        }

        public virtual bool RemoveEdge(TEdge e)
        {
            if (!wrapped.ContainsEdge(e))
                return false;
            RemoveChildEdge(e);
            wrapped.RemoveEdge(e);
            if (parent != null)
                parent.RemoveEdge(e);
            return true;
        }


        public int RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
        {
            var edges = new EdgeList<TVertex, TEdge>();
            foreach (var edge in wrapped.Edges)
                if (predicate(edge))
                    edges.Add(edge);
            foreach (var edge in edges)
                this.RemoveEdge(edge);
            return edges.Count;
        }

        public void ClearOutEdges(TVertex v)
        {
            wrapped.ClearOutEdges(v);
        }

        public int RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            int edgeToRemoveCount = wrapped.RemoveOutEdgeIf(v, predicate);
            if (parent != null)
                parent.RemoveOutEdgeIf(v, predicate);
            return edgeToRemoveCount;
        }


        public void Clear()
        {
            wrapped.Clear();
            this.clusters.Clear();
        }
    }
}
