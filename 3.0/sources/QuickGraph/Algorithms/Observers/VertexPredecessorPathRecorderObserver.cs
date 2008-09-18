using System;
using System.Collections.Generic;

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
        private bool recordEndPath = true;

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
            GraphContracts.AssumeNotNull(algorithm, "algorithm");
            algorithm.TreeEdge+=new EdgeEventHandler<TVertex,TEdge>(TreeEdge);
            algorithm.FinishVertex+=new VertexEventHandler<TVertex>(FinishVertex);
        }

        public void Detach(IVertexPredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            GraphContracts.AssumeNotNull(algorithm, "algorithm");
            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(TreeEdge);
            algorithm.FinishVertex -= new VertexEventHandler<TVertex>(FinishVertex);
        }

        void TreeEdge(Object sender, EdgeEventArgs<TVertex, TEdge> e)
        {
            VertexPredecessors[e.Edge.Target] = e.Edge;
        }

        void FinishVertex(Object sender, VertexEventArgs<TVertex> e)
        {
            foreach (var edge in this.VertexPredecessors.Values)
            {
                if (edge.Source.Equals(e.Vertex))
                    return;
            }
            this.endPathVertices.Add(e.Vertex);
        }

        public IEnumerable<IEnumerable<TEdge>> AllPaths()
        {
            List<IEnumerable<TEdge>> es = new List<IEnumerable<TEdge>>();
            foreach (var v in this.EndPathVertices)
                es.Add(this.vertexPredecessors.Path(v));
            return es;
        }
    }
}
