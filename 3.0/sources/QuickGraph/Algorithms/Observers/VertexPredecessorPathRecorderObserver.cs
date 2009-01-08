using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
    [Serializable]
    public sealed class VertexPredecessorPathRecorderObserver<TVertex, TEdge> :
        IObserver<IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, TEdge> vertexPredecessors;
        private readonly List<TVertex> endPathVertices = new List<TVertex>();

        public VertexPredecessorPathRecorderObserver()
            :this(new Dictionary<TVertex,TEdge>())
        {}

        public VertexPredecessorPathRecorderObserver(
            IDictionary<TVertex, TEdge> vertexPredecessors)
        {
            if (vertexPredecessors == null)
                throw new ArgumentNullException("vertexPredecessors");
            this.vertexPredecessors = vertexPredecessors;
        }

        public IDictionary<TVertex, TEdge> VertexPredecessors
        {
            get { return this.vertexPredecessors; }
        }

        public ICollection<TVertex> EndPathVertices
        {
            get { return this.endPathVertices; }
        }

        public void Attach(IVertexPredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);
            
            algorithm.TreeEdge+=new EdgeAction<TVertex,TEdge>(TreeEdge);
            algorithm.FinishVertex+=new VertexAction<TVertex>(FinishVertex);
        }

        public void Detach(IVertexPredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge -= new EdgeAction<TVertex, TEdge>(TreeEdge);
            algorithm.FinishVertex -= new VertexAction<TVertex>(FinishVertex);
        }

        void TreeEdge(Object sender, TEdge e)
        {
            VertexPredecessors[e.Target] = e;
        }

        void FinishVertex(object sender, TVertex v)
        {
            foreach (var edge in this.VertexPredecessors.Values)
            {
                if (edge.Source.Equals(v))
                    return;
            }
            this.endPathVertices.Add(v);
        }

        public IEnumerable<IEnumerable<TEdge>> AllPaths()
        {
            List<IEnumerable<TEdge>> es = new List<IEnumerable<TEdge>>();
            foreach (var v in this.EndPathVertices)
            {
                IEnumerable<TEdge> path;
                if (EdgeExtensions.TryGetPath(this.vertexPredecessors, v, out path))
                    es.Add(path);
            }
            return es;
        }
    }
}
