using System;
using System.Collections.Generic;
using System.Web;

using QuickGraph.Predicates;

namespace QuickGraph.Algorithms.Exploration
{
    public sealed class TransitionFactoryImplicitGraph<Vertex,Edge> :
        IImplicitGraph<Vertex,Edge>
        where Vertex : ICloneable
        where Edge : IEdge<Vertex>
    {
        private IList<ITransitionFactory<Vertex, Edge>> transitionFactories
            = new List<ITransitionFactory<Vertex, Edge>>();
        private VertexPredicate<Vertex> successorVertexPredicate
            = new AnyVertexPredicate<Vertex>().Test;
        private EdgePredicate<Vertex, Edge> successorEdgePredicate
            = new AnyEdgePredicate<Vertex, Edge>().Test;

        public TransitionFactoryImplicitGraph()
        {}

        public IList<ITransitionFactory<Vertex, Edge>> TransitionFactories
        {
            get { return this.transitionFactories; }
        }

        public VertexPredicate<Vertex> SuccessorVertexPredicate
        {
            get { return this.successorVertexPredicate; }
            set { this.successorVertexPredicate = value; }
        }

        public EdgePredicate<Vertex, Edge> SuccessorEdgePredicate
        {
            get { return this.successorEdgePredicate; }
            set { this.successorEdgePredicate = value; }
        }

        public bool IsOutEdgesEmpty(Vertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(Vertex v)
        {
            int i = 0;
            foreach(Edge edge in this.OutEdges(v))
                i++;
            return i;
        }

        public IEnumerable<Edge> OutEdges(Vertex v)
        {
            foreach (ITransitionFactory<Vertex, Edge> transitionFactory
                in this.TransitionFactories)
            {
                if (!transitionFactory.IsValid(v))
                    continue;

                foreach (Edge edge in transitionFactory.Apply(v))
                {
                    if (this.SuccessorVertexPredicate(edge.Target) &&
                        this.SuccessorEdgePredicate(edge))
                        yield return edge;
                }
            }
        }

        public Edge OutEdge(Vertex v, int index)
        {
            int i = 0;
            foreach (Edge e in this.OutEdges(v))
                if (i++ == index)
                    return e;
            throw new ArgumentOutOfRangeException("index");
        }

        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return true; }
        }
    }
}
