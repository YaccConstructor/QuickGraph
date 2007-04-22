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
    public sealed class EdgeRecorderObserver<Vertex, Edge> :
        IObserver<Vertex,Edge,ITreeBuilderAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IList<Edge> edges;
        public EdgeRecorderObserver()
            :this(new List<Edge>())
        {}

        public EdgeRecorderObserver(IList<Edge> edges)
        {
            if (edges == null)
                throw new ArgumentNullException("edges");
            this.edges = edges;
        }

        public IList<Edge> Edges
        {
            get
            {
                return this.edges;
            }
        }

        public void Attach(ITreeBuilderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.TreeEdge +=new EdgeEventHandler<Vertex,Edge>(RecordEdge);
        }

        public void Detach(ITreeBuilderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.TreeEdge -=new EdgeEventHandler<Vertex,Edge>(RecordEdge);
        }

        public void RecordEdge(Object sender, EdgeEventArgs<Vertex,Edge> args)
        {
            this.Edges.Add(args.Edge);
        }
        public void RecordSource(Object sender, EdgeEdgeEventArgs<Vertex,Edge> args)
        {
            this.Edges.Add(args.Edge);
        }
        public void RecordTarget(Object sender, EdgeEdgeEventArgs<Vertex,Edge> args)
        {
            this.Edges.Add(args.TargetEdge);
        }

    }
}
