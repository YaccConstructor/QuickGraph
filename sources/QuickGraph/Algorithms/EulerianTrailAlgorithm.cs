using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class EulerianTrailAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex,IMutableVertexAndEdgeListGraph<Vertex, Edge>>,
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private List<Edge> circuit;
        private List<Edge> temporaryCircuit;
        private Vertex currentVertex;
        private List<Edge> temporaryEdges;

        /// <summary>
        /// Construct an eulerian trail builder
        /// </summary>
        /// <param name="g"></param>
        public EulerianTrailAlgorithm(IMutableVertexAndEdgeListGraph<Vertex, Edge> visitedGraph)
            :base(visitedGraph)
        {
            this.circuit = new List<Edge>();
            this.temporaryCircuit = new List<Edge>();
            this.currentVertex = default(Vertex);
            this.temporaryEdges = new List<Edge>();
        }

        public List<Edge> Circuit
        {
            get
            {
                return circuit;
            }
        }

        private bool NotInCircuit(Edge edge)
        {
            return !this.circuit.Contains(edge) 
                && !this.temporaryCircuit.Contains(edge);
        }

        private IEnumerable<Edge> SelectOutEdgesNotInCircuit(Vertex v)
        {
            foreach (Edge edge in VisitedGraph.OutEdges(v))
                if (this.NotInCircuit(edge))
                    yield return edge;
        }

        private Edge SelectSingleOutEdgeNotInCircuit(Vertex v)
        {
            IEnumerable<Edge> en = this.SelectOutEdgesNotInCircuit(v);
            IEnumerator<Edge> eor = en.GetEnumerator();
            if (!eor.MoveNext())
                return default(Edge);
            else
                return eor.Current;
        }

        public event EdgeEventHandler<Vertex,Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event EdgeEventHandler<Vertex,Edge> CircuitEdge;
        private void OnCircuitEdge(Edge e)
        {
            if (CircuitEdge != null)
                CircuitEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event EdgeEventHandler<Vertex,Edge> VisitEdge;
        private void OnVisitEdge(Edge e)
        {
            if (VisitEdge != null)
                VisitEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        private bool Search(Vertex u)
        {
            foreach (Edge e in SelectOutEdgesNotInCircuit(u))
            {
                OnTreeEdge(e);
                Vertex v = e.Target;
                // add edge to temporary path
                this.temporaryCircuit.Add(e);
                // e.Target should be equal to CurrentVertex.
                if (e.Target.Equals(this.currentVertex))
                    return true;

                // continue search
                if (Search(v))
                    return true;
                else
                    // remove edge
                    this.temporaryCircuit.Remove(e);
            }

            // it's a dead end.
            return false;
        }


        /// <summary>
        /// Looks for a new path to add to the current vertex.
        /// </summary>
        /// <returns>true if found a new path, false otherwize</returns>
        private bool Visit()
        {
            // find a vertex that needs to be visited
            foreach (Edge e in Circuit)
            {
                Edge fe = SelectSingleOutEdgeNotInCircuit(e.Source);
                if (fe != null)
                {
                    OnVisitEdge(fe);
                    this.currentVertex = e.Source;
                    if (Search(currentVertex))
                        return true;
                }
            }

            // Could not augment circuit
            return false;
        }

        /// <summary>
        /// Computes the number of eulerian trail in the graph.
        /// </summary>
        /// <param name="g"></param>
        /// <returns>number of eulerian trails</returns>
        public static int ComputeEulerianPathCount(IVertexAndEdgeListGraph<Vertex,Edge> g)
        {
            if (g == null)
                throw new ArgumentNullException("g");

            if (g.EdgeCount < g.VertexCount)
                return 0;

            int odd = AlgoUtility.OddVertices(g).Count;
            if (odd == 0)
                return 1;
            else if (odd % 2 != 0)
                return 0;
            else
                return odd / 2;
        }

        /// <summary>
        /// Merges the temporary circuit with the current circuit
        /// </summary>
        /// <returns>true if all the graph edges are in the circuit</returns>
        private bool CircuitAugmentation()
        {
            List<Edge> newC = new List<Edge>(this.circuit.Count + this.temporaryCircuit.Count);
            int i, j;

            // follow C until w is found
            for (i = 0; i < Circuit.Count; ++i)
            {
                Edge e = Circuit[i];
                if (e.Source.Equals(currentVertex))
                    break;
                newC.Add(e);
            }

            // follow D until w is found again
            for (j = 0; j < this.temporaryCircuit.Count; ++j)
            {
                Edge e = this.temporaryCircuit[j];
                newC.Add(e);
                OnCircuitEdge(e);
                if (e.Target.Equals(this.currentVertex))
                    break;
            }
            this.temporaryCircuit.Clear();

            // continue C
            for (; i < Circuit.Count; ++i)
            {
                Edge e = this.Circuit[i];
                newC.Add(e);
            }

            // set as new circuit
            circuit = newC;

            // check if contains all edges
            if (this.circuit.Count == this.VisitedGraph.EdgeCount)
                return true;

            return false;
        }

        protected override void InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;
            if (this.RootVertex == null)
                this.RootVertex = TraversalHelper.GetFirstVertex<Vertex, Edge>(this.VisitedGraph);

            this.currentVertex = this.RootVertex;
            // start search
            Search(this.currentVertex);
            if (CircuitAugmentation())
                return; // circuit is found

            do
            {
                if (!Visit())
                    break; // visit edges and build path
                if (CircuitAugmentation())
                    break; // circuit is found
            } while (true);
        }

        /// <summary>
        /// Adds temporary edges to the graph to make all vertex even.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public List<Edge> AddTemporaryEdges(IEdgeFactory<Vertex,Edge> edgeFactory)
        {
            // first gather odd edges.
            List<Vertex> oddVertices = AlgoUtility.OddVertices(this.VisitedGraph);

            // check that there are an even number of them
            if (oddVertices.Count % 2 != 0)
                throw new Exception("number of odd vertices in not even!");

            // add temporary edges to create even edges:
            this.temporaryEdges = new List<Edge>();

            bool found, foundbe, foundadjacent;
            while (oddVertices.Count > 0)
            {
                Vertex u = oddVertices[0];
                // find adjacent odd vertex.
                found = false;
                foundadjacent = false;
                foreach (Edge e in this.VisitedGraph.OutEdges(u))
                {
                    Vertex v = e.Target;
                    if (!v.Equals(u) && oddVertices.Contains(v))
                    {
                        foundadjacent = true;
                        // check that v does not have an out-edge towards u
                        foundbe = false;
                        foreach (Edge be in this.VisitedGraph.OutEdges(v))
                        {
                            if (be.Target.Equals(u))
                            {
                                foundbe = true;
                                break;
                            }
                        }
                        if (foundbe)
                            continue;
                        // add temporary edge
                        Edge tempEdge = edgeFactory.CreateEdge(v, u);
                        if (!this.VisitedGraph.AddEdge(tempEdge))
                            throw new InvalidOperationException();
                        // add to collection
                        temporaryEdges.Add(tempEdge);
                        // remove u,v from oddVertices
                        oddVertices.Remove(u);
                        oddVertices.Remove(v);
                        // set u to null
                        found = true;
                        break;
                    }
                }

                if (!foundadjacent)
                {
                    // pick another vertex
                    if (oddVertices.Count < 2)
                        throw new Exception("Eulerian trail failure");
                    Vertex v = oddVertices[1];
                    Edge tempEdge = edgeFactory.CreateEdge(u, v);
                    if (!this.VisitedGraph.AddEdge(tempEdge))
                        throw new InvalidOperationException();
                    // add to collection
                    temporaryEdges.Add(tempEdge);
                    // remove u,v from oddVertices
                    oddVertices.Remove(u);
                    oddVertices.Remove(v);
                    // set u to null
                    found = true;

                }

                if (!found)
                {
                    oddVertices.Remove(u);
                    oddVertices.Add(u);
                }
            }
            return this.temporaryEdges;
        }

        /// <summary>
        /// Removes temporary edges
        /// </summary>
        /// <param name="g"></param>
        public void RemoveTemporaryEdges()
        {
            // remove from graph
            foreach (Edge e in temporaryEdges)
                this.VisitedGraph.RemoveEdge(e);
            this.temporaryEdges.Clear();
        }

        /// <summary>
        /// Computes the set of eulerian trails that traverse the edge set.
        /// </summary>
        /// <remarks>
        /// This method returns a set of disjoint eulerian trails. This set
        /// of trails spans the entire set of edges.
        /// </remarks>
        /// <returns>Eulerian trail set</returns>
        public ICollection<ICollection<Edge>> Trails()
        {
            List<ICollection<Edge>> trails = new List<ICollection<Edge>>();

            List<Edge> trail = new List<Edge>();
            foreach (Edge e in this.Circuit)
            {
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    trail = new List<Edge>();
                }
                else
                    trail.Add(e);
            }
            if (trail.Count != 0)
                trails.Add(trail);

            return trails;
        }

        /// <summary>
        /// Computes a set of eulerian trail, starting at <paramref name="s"/>
        /// that spans the entire graph.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method computes a set of eulerian trail starting at <paramref name="s"/>
        /// that spans the entire graph.The algorithm outline is as follows:
        /// </para>
        /// <para>
        /// The algorithms iterates throught the Eulerian circuit of the augmented
        /// graph (the augmented graph is the graph with additional edges to make
        /// the number of odd vertices even).
        /// </para>
        /// <para>
        /// If the current edge is not temporary, it is added to the current trail.
        /// </para>
        /// <para>
        /// If the current edge is temporary, the current trail is finished and
        /// added to the trail collection. The shortest path between the 
        /// start vertex <paramref name="s"/> and the target vertex of the
        /// temporary edge is then used to start the new trail. This shortest
        /// path is computed using the <see cref="BreadthFirstSearchAlgorithm"/>.
        /// </para>
        /// </remarks>
        /// <param name="s">start vertex</param>
        /// <returns>eulerian trail set, all starting at s</returns>
        /// <exception cref="ArgumentNullException">s is a null reference.</exception>
        /// <exception cref="Exception">Eulerian trail not computed yet.</exception>
        public ICollection<ICollection<Edge>> Trails(Vertex s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (this.Circuit.Count == 0)
                throw new Exception("Circuit is empty");

            // find the first edge in the circuit.
            int i = 0;
            for (i = 0; i < this.Circuit.Count; ++i)
            {
                Edge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                    continue;
                if (e.Source.Equals(s))
                    break;
            }
            if (i == this.Circuit.Count)
                throw new Exception("Did not find vertex in eulerian trail?");

            // create collections
            List<ICollection<Edge>> trails = new List<ICollection<Edge>>();
            List<Edge> trail = new List<Edge>();
            BreadthFirstSearchAlgorithm<Vertex,Edge> bfs =
                new BreadthFirstSearchAlgorithm<Vertex,Edge>(VisitedGraph);
            VertexPredecessorRecorderObserver<Vertex,Edge> vis = 
                new VertexPredecessorRecorderObserver<Vertex,Edge>();
            vis.Attach(bfs);
            bfs.Compute(s);

            // go throught the edges and build the predecessor table.
            int start = i;
            for (; i < this.Circuit.Count; ++i)
            {
                Edge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    // take the shortest path from the start vertex to
                    // the target vertex
                    trail = vis.Path(e.Target);
                }
                else
                    trail.Add(e);
            }

            // starting again on the circuit
            for (i = 0; i < start; ++i)
            {
                Edge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    // take the shortest path from the start vertex to
                    // the target vertex
                    trail = vis.Path(e.Target);
                }
                else
                    trail.Add(e);
            }

            // adding the last element
            if (trail.Count != 0)
                trails.Add(trail);

            return trails;
        }
    }
}
