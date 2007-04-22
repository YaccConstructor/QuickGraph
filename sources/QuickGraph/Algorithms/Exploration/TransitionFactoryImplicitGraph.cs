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
        private IVertexPredicate<Vertex> successorVertexPredicate
            = new AnyVertexPredicate<Vertex>();
        private IEdgePredicate<Vertex, Edge> successorEdgePredicate
            = new AnyEdgePredicate<Vertex, Edge>();

        public TransitionFactoryImplicitGraph()
        {}

        public IList<ITransitionFactory<Vertex, Edge>> TransitionFactories
        {
            get { return this.transitionFactories; }
        }

        public IVertexPredicate<Vertex> SuccessorVertexPredicate
        {
            get { return this.successorVertexPredicate; }
            set { this.successorVertexPredicate = value; }
        }

        public IEdgePredicate<Vertex, Edge> SuccessorEdgePredicate
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
                    if (!this.SuccessorVertexPredicate.Test(edge.Target))
                        continue;
                    if (!this.SuccessorEdgePredicate.Test(edge))
                        continue;
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
