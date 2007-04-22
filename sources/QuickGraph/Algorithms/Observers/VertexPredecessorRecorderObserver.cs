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
    public sealed class VertexPredecessorRecorderObserver<Vertex, Edge> :
        IObserver<Vertex,Edge,IVertexPredecessorRecorderAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, Edge> vertexPredecessors;
        private bool recordEndPath = true;
        private IList<Vertex> endPathVertices = new List<Vertex>();

        public VertexPredecessorRecorderObserver()
            :this(new Dictionary<Vertex,Edge>())
        {}

        public VertexPredecessorRecorderObserver(
            IDictionary<Vertex, Edge> vertexPredecessors)
        {
            if (vertexPredecessors == null)
                throw new ArgumentNullException("vertexPredecessors");
            this.vertexPredecessors = vertexPredecessors;
        }

        public IDictionary<Vertex, Edge> VertexPredecessors
        {
            get { return this.vertexPredecessors; }
        }

        public ICollection<Vertex> EndPathVertices
        {
            get { return this.endPathVertices; }
        }

        public bool RecordEndPath
        {
            get { return this.recordEndPath; }
            set { this.recordEndPath = value; }
        }

        public void Attach(IVertexPredecessorRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.StartVertex += new VertexEventHandler<Vertex>(StartVertex);
            algorithm.TreeEdge+=new EdgeEventHandler<Vertex,Edge>(TreeEdge);
            algorithm.FinishVertex+=new VertexEventHandler<Vertex>(FinishVertex);
        }

        public void Detach(IVertexPredecessorRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.StartVertex -= new VertexEventHandler<Vertex>(StartVertex);
            algorithm.TreeEdge -= new EdgeEventHandler<Vertex, Edge>(TreeEdge);
            algorithm.FinishVertex -= new VertexEventHandler<Vertex>(FinishVertex);
        }

        void StartVertex(object sender, VertexEventArgs<Vertex> e)
        {
            VertexPredecessors[e.Vertex] = default(Edge);
        }

        void TreeEdge(Object sender, EdgeEventArgs<Vertex, Edge> e)
        {
            VertexPredecessors[e.Edge.Target] = e.Edge;
        }

        void FinishVertex(Object sender, VertexEventArgs<Vertex> e)
        {
            if (this.RecordEndPath)
            {
                foreach (Edge edge in this.VertexPredecessors.Values)
                {
                    if (edge!=null && edge.Source.Equals(e.Vertex))
                        return;
                }
                this.endPathVertices.Add(e.Vertex);
            }
        }

        public List<Edge> Path(Vertex v)
        {
            List<Edge> path = new List<Edge>();

            Vertex vc = v;
            Edge e;
            while (this.VertexPredecessors.TryGetValue(vc, out e))
            {
                if (e == null)
                    break;
                path.Insert(0, e);
                vc = e.Source;
            }

            return path;
        }

        public IList<IList<Edge>> AllPaths()
        {
            List<IList<Edge>> es = new List<IList<Edge>>();
            foreach (Vertex v in this.EndPathVertices)
                es.Add(Path(v));

            return es;
        }
    }
}
