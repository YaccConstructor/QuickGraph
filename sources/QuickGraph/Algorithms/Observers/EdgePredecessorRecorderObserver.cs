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
    public sealed class EdgePredecessorRecorderObserver<Vertex, Edge> :
        IObserver<Vertex, Edge, IEdgePredecessorRecorderAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Edge,Edge> edgePredecessors;
        private IList<Edge> endPathEdges;

        public EdgePredecessorRecorderObserver()
            :this(new Dictionary<Edge,Edge>(), new List<Edge>())
        {}

        public EdgePredecessorRecorderObserver(
            IDictionary<Edge,Edge> edgePredecessors,
            IList<Edge> endPathEdges
            )
        {
            if (edgePredecessors == null)
                throw new ArgumentNullException("edgePredecessors");
            if (endPathEdges == null)
                throw new ArgumentNullException("endPathEdges");

            this.edgePredecessors = edgePredecessors;
            this.endPathEdges = endPathEdges;
        }

        public IDictionary<Edge,Edge> EdgePredecessors
        {
            get
            {
                return edgePredecessors;
            }
        }

        public IList<Edge> EndPathEdges
        {
            get
            {
                return endPathEdges;
            }
        }

        public void Attach(IEdgePredecessorRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverTreeEdge +=new EdgeEdgeEventHandler<Vertex,Edge>(this.DiscoverTreeEdge);
            algorithm.FinishEdge +=new EdgeEventHandler<Vertex,Edge>(this.FinishEdge);
        }

        public void Detach(IEdgePredecessorRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverTreeEdge -= new EdgeEdgeEventHandler<Vertex, Edge>(this.DiscoverTreeEdge);
            algorithm.FinishEdge -= new EdgeEventHandler<Vertex, Edge>(this.FinishEdge);
        }

        public ICollection<Edge> Path(Edge se)
        {
            List<Edge> path = new List<Edge>();

            Edge ec = se;
            path.Insert(0, ec);
            while (EdgePredecessors.ContainsKey(ec))
            {
                Edge e = EdgePredecessors[ec];
                path.Insert(0, e);
                ec = e;
            }
            return path;
        }

        public ICollection<ICollection<Edge>> AllPaths()
        {
            IList<ICollection<Edge>> es = new List<ICollection<Edge>>();

            foreach (Edge e in EndPathEdges)
                es.Add(Path(e));

            return es;
        }

        public ICollection<Edge> MergedPath(Edge se, IDictionary<Edge,GraphColor> colors)
        {
            List<Edge> path = new List<Edge>();

            Edge ec = se;
            GraphColor c = colors[ec];
            if (c != GraphColor.White)
                return path;
            else
                colors[ec] = GraphColor.Black;

            path.Insert(0, ec);
            while (EdgePredecessors.ContainsKey(ec))
            {
                Edge e = EdgePredecessors[ec];
                c = colors[e];
                if (c != GraphColor.White)
                    return path;
                else
                    colors[e] = GraphColor.Black;

                path.Insert(0, e);
                ec = e;
            }
            return path;
        }

        public ICollection<ICollection<Edge>> AllMergedPaths()
        {
            List<ICollection<Edge>> es = new List<ICollection<Edge>>(EndPathEdges.Count);
            IDictionary<Edge,GraphColor> colors = new Dictionary<Edge,GraphColor>();

            foreach (KeyValuePair<Edge,Edge> de in EdgePredecessors)
            {
                colors[de.Key] = GraphColor.White;
                colors[de.Value] = GraphColor.White;
            }

            for (int i = 0; i < EndPathEdges.Count; ++i)
                es.Add(MergedPath(EndPathEdges[i], colors));

            return es;
        }

        private void DiscoverTreeEdge(Object sender, EdgeEdgeEventArgs<Vertex,Edge> args)
        {
            if (!args.Edge.Equals(args.TargetEdge))
                EdgePredecessors[args.TargetEdge] = args.Edge;
        }

        private void FinishEdge(Object sender, EdgeEventArgs<Vertex,Edge> args)
        {
            foreach (Edge edge in this.EdgePredecessors.Values)
                if (edge.Equals(args.Edge))
                    return;

            this.EndPathEdges.Add(args.Edge);
        }
    }
}
