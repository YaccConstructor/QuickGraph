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
    public sealed class VertexPredecessorRecorderObserver<TVertex, TEdge> :
        IObserver<IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, TEdge> vertexPredecessors;
        private readonly List<TVertex> endPathVertices = new List<TVertex>();
        private bool recordEndPath = true;

        public VertexPredecessorRecorderObserver()
            :this(new Dictionary<TVertex,TEdge>())
        {}

        public VertexPredecessorRecorderObserver(
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

        public bool RecordEndPath
        {
            get { return this.recordEndPath; }
            set { this.recordEndPath = value; }
        }

        public void Attach(IVertexPredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.StartVertex += new VertexEventHandler<TVertex>(StartVertex);
            algorithm.TreeEdge+=new EdgeEventHandler<TVertex,TEdge>(TreeEdge);
            algorithm.FinishVertex+=new VertexEventHandler<TVertex>(FinishVertex);
        }

        public void Detach(IVertexPredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.StartVertex -= new VertexEventHandler<TVertex>(StartVertex);
            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(TreeEdge);
            algorithm.FinishVertex -= new VertexEventHandler<TVertex>(FinishVertex);
        }

        void StartVertex(object sender, VertexEventArgs<TVertex> e)
        {
//            VertexPredecessors[e.Vertex] = default(Edge);
        }

        void TreeEdge(Object sender, EdgeEventArgs<TVertex, TEdge> e)
        {
            VertexPredecessors[e.Edge.Target] = e.Edge;
        }

        void FinishVertex(Object sender, VertexEventArgs<TVertex> e)
        {
            if (this.RecordEndPath)
            {
                foreach (var edge in this.VertexPredecessors.Values)
                {
                    if (edge.Source.Equals(e.Vertex))
                        return;
                }
                this.endPathVertices.Add(e.Vertex);
            }
        }

        public List<TEdge> Path(TVertex v)
        {
            List<TEdge> path = new List<TEdge>();

            TVertex vc = v;
            TEdge e;
            while (this.VertexPredecessors.TryGetValue(vc, out e))
            {
                path.Insert(0, e);
                vc = e.Source;
            }

            return path;
        }

        public IList<IList<TEdge>> AllPaths()
        {
            List<IList<TEdge>> es = new List<IList<TEdge>>();
            foreach (var v in this.EndPathVertices)
                es.Add(Path(v));

            return es;
        }
    }
}
