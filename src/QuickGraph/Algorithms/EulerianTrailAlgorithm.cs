using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Algorithms
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class EulerianTrailAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IMutableVertexAndEdgeListGraph<TVertex, TEdge>>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private List<TEdge> circuit;
        private List<TEdge> temporaryCircuit;
        private TVertex currentVertex;
        private List<TEdge> temporaryEdges;

        public EulerianTrailAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

        /// <summary>
        /// Construct an eulerian trail builder
        /// </summary>
        /// <param name="host"></param>
        /// <param name="visitedGraph"></param>
        public EulerianTrailAlgorithm(
            IAlgorithmComponent host,
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            :base(host, visitedGraph)
        {
            this.circuit = new List<TEdge>();
            this.temporaryCircuit = new List<TEdge>();
            this.currentVertex = default(TVertex);
            this.temporaryEdges = new List<TEdge>();
        }

        public List<TEdge> Circuit
        {
            get
            {
                return circuit;
            }
        }

        private bool NotInCircuit(TEdge edge)
        {
            return !this.circuit.Contains(edge) 
                && !this.temporaryCircuit.Contains(edge);
        }

        private IEnumerable<TEdge> SelectOutEdgesNotInCircuit(TVertex v)
        {
            foreach (var edge in VisitedGraph.OutEdges(v))
                if (this.NotInCircuit(edge))
                    yield return edge;
        }

        private TEdge SelectSingleOutEdgeNotInCircuit(TVertex v)
        {
            IEnumerable<TEdge> en = this.SelectOutEdgesNotInCircuit(v);
            IEnumerator<TEdge> eor = en.GetEnumerator();
            if (!eor.MoveNext())
                return default(TEdge);
            else
                return eor.Current;
        }

        public event EdgeAction<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            Contract.Requires(e != null);
            var eh = this.TreeEdge;
            if (eh != null)
                eh(e);
        }

        public event EdgeAction<TVertex,TEdge> CircuitEdge;
        private void OnCircuitEdge(TEdge e)
        {
            Contract.Requires(e != null);

            var eh = this.CircuitEdge;
            if (eh != null)
                eh(e);
        }

        public event EdgeAction<TVertex,TEdge> VisitEdge;
        private void OnVisitEdge(TEdge e)
        {
            Contract.Requires(e != null);

            var eh = this.VisitEdge;
            if (eh != null)
                eh(e);
        }

        private bool Search(TVertex u)
        {
            Contract.Requires(u != null);

            foreach (var e in SelectOutEdgesNotInCircuit(u))
            {
                OnTreeEdge(e);
                TVertex v = e.Target;
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
            foreach (var e in Circuit)
            {
                TEdge fe = SelectSingleOutEdgeNotInCircuit(e.Source);
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
        public static int ComputeEulerianPathCount(IVertexAndEdgeListGraph<TVertex,TEdge> g)
        {
            Contract.Requires(g != null);

            if (g.EdgeCount < g.VertexCount)
                return 0;

            int odd = AlgorithmExtensions.OddVertices(g).Count;
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
            List<TEdge> newC = new List<TEdge>(this.circuit.Count + this.temporaryCircuit.Count);
            int i, j;

            // follow C until w is found
            for (i = 0; i < this.Circuit.Count; ++i)
            {
                TEdge e = this.Circuit[i];
                if (e.Source.Equals(currentVertex))
                    break;
                newC.Add(e);
            }

            // follow D until w is found again
            for (j = 0; j < this.temporaryCircuit.Count; ++j)
            {
                TEdge e = this.temporaryCircuit[j];
                newC.Add(e);
                OnCircuitEdge(e);
                if (e.Target.Equals(this.currentVertex))
                    break;
            }
            this.temporaryCircuit.Clear();

            // continue C
            for (; i < Circuit.Count; ++i)
            {
                TEdge e = this.Circuit[i];
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

            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                rootVertex = Enumerable.First(this.VisitedGraph.Vertices);

            this.currentVertex = rootVertex;
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
        /// <param name="edgeFactory"></param>
        /// <returns></returns>
        public List<TEdge> AddTemporaryEdges(EdgeFactory<TVertex,TEdge> edgeFactory)
        {
            // first gather odd edges.
            var oddVertices = AlgorithmExtensions.OddVertices(this.VisitedGraph);

            // check that there are an even number of them
            if (oddVertices.Count % 2 != 0)
                throw new Exception("number of odd vertices in not even!");

            // add temporary edges to create even edges:
            this.temporaryEdges = new List<TEdge>();

            bool found, foundbe, foundadjacent;
            while (oddVertices.Count > 0)
            {
                TVertex u = oddVertices[0];
                // find adjacent odd vertex.
                found = false;
                foundadjacent = false;
                foreach (var e in this.VisitedGraph.OutEdges(u))
                {
                    TVertex v = e.Target;
                    if (!v.Equals(u) && oddVertices.Contains(v))
                    {
                        foundadjacent = true;
                        // check that v does not have an out-edge towards u
                        foundbe = false;
                        foreach (var be in this.VisitedGraph.OutEdges(v))
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
                        TEdge tempEdge = edgeFactory(v, u);
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
                    TVertex v = oddVertices[1];
                    TEdge tempEdge = edgeFactory(u, v);
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
        public void RemoveTemporaryEdges()
        {
            // remove from graph
            foreach (var e in temporaryEdges)
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
        public ICollection<ICollection<TEdge>> Trails()
        {
            List<ICollection<TEdge>> trails = new List<ICollection<TEdge>>();

            List<TEdge> trail = new List<TEdge>();
            foreach (var e in this.Circuit)
            {
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    trail = new List<TEdge>();
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
        /// path is computed using the BreadthFirstSearchAlgorithm.
        /// </para>
        /// </remarks>
        /// <param name="s">start vertex</param>
        /// <returns>eulerian trail set, all starting at s</returns>
        /// <exception cref="ArgumentNullException">s is a null reference.</exception>
        /// <exception cref="Exception">Eulerian trail not computed yet.</exception>
        public ICollection<ICollection<TEdge>> Trails(TVertex s)
        {
            Contract.Requires(s != null);
            if (this.Circuit.Count == 0)
                throw new InvalidOperationException("Circuit is empty");

            // find the first edge in the circuit.
            int i = 0;
            for (i = 0; i < this.Circuit.Count; ++i)
            {
                TEdge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                    continue;
                if (e.Source.Equals(s))
                    break;
            }
            if (i == this.Circuit.Count)
                throw new Exception("Did not find vertex in eulerian trail?");

            // create collections
            List<ICollection<TEdge>> trails = new List<ICollection<TEdge>>();
            List<TEdge> trail = new List<TEdge>();
            BreadthFirstSearchAlgorithm<TVertex,TEdge> bfs =
                new BreadthFirstSearchAlgorithm<TVertex,TEdge>(VisitedGraph);
            VertexPredecessorRecorderObserver<TVertex,TEdge> vis = 
                new VertexPredecessorRecorderObserver<TVertex,TEdge>();
            vis.Attach(bfs);
            bfs.Compute(s);

            // go throught the edges and build the predecessor table.
            int start = i;
            for (; i < this.Circuit.Count; ++i)
            {
                TEdge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    // take the shortest path from the start vertex to
                    // the target vertex
                    IEnumerable<TEdge> path;
                    if (!vis.TryGetPath(e.Target, out path))
                        throw new InvalidOperationException();
                    trail = new List<TEdge>(path);
                }
                else
                    trail.Add(e);
            }

            // starting again on the circuit
            for (i = 0; i < start; ++i)
            {
                TEdge e = this.Circuit[i];
                if (this.temporaryEdges.Contains(e))
                {
                    // store previous trail and start new one.
                    if (trail.Count != 0)
                        trails.Add(trail);
                    // start new trail
                    // take the shortest path from the start vertex to
                    // the target vertex
                    IEnumerable<TEdge> path;
                    if (!vis.TryGetPath(e.Target, out path))
                        throw new InvalidOperationException();
                    trail = new List<TEdge>(path);
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
