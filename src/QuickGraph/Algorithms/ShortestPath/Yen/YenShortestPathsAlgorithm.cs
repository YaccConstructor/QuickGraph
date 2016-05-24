using System.Collections.Generic;
using System.Management.Instrumentation;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
    public class YenShortestPathsAlgorithm<TVertex>
    {
        private readonly TVertex _sourceVertix;
        private readonly TVertex _targetVertix;
        // limit for amount of paths
        private int k;
        private AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> _graph;
        private readonly List<TaggedEquatableEdge<TVertex, double>> _removedEdges = new List<TaggedEquatableEdge<TVertex, double>>();

        /*
         * for access from visualisation code
         */
        public IEnumerable<TaggedEquatableEdge<TVertex, double>> RemovedEdges
          => _removedEdges;

        /*
          double type of tag comes from Dijkstra’s algorithm,
          which is used to get one shortest path.
        */

        public YenShortestPathsAlgorithm(AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph, TVertex s,
          TVertex t, int k)
        {
            _sourceVertix = s;
            _targetVertix = t;
            this.k = k;
            this._graph = graph;
        }

        public IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>> Execute()
        {
            var listShortestWays = new List<IEnumerable<TaggedEquatableEdge<TVertex, double>>>();
            // find the first shortest way
            var shortestWay = GetShortestPathInGraph(_graph);
            listShortestWays.Add(shortestWay);

            /*
             * in case of Dijkstra’s algorithm couldn't find any ways
             */
            if (shortestWay == null)
            {
                throw new InstanceNotFoundException();
            }

            for (var i = 0; i < k - 1; i++)
            {
                var minDistance = double.MaxValue;
                IEnumerable<TaggedEquatableEdge<TVertex, double>> pathSlot = null;
                // slot for graph state without some edge
                AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graphSlot = null;
                TaggedEquatableEdge<TVertex, double> removedEdge = null;
                foreach (var edge in shortestWay)
                {
                    // get new state without the edge
                    var newGraph = RemoveEdge(_graph, edge);

                    //find shortest way in the new graph
                    var newPath = GetShortestPathInGraph(newGraph);
                    if (newPath == null)
                    {
                        continue;
                    }
                    var pathWeight = GetPathDistance(newPath);
                    if (pathWeight >= minDistance)
                    {
                        continue;
                    }
                    minDistance = pathWeight;
                    pathSlot = newPath;
                    graphSlot = newGraph;
                    removedEdge = edge;
                }
                if (pathSlot == null)
                {
                    break;
                }
                listShortestWays.Add(pathSlot);
                _removedEdges.Add(removedEdge);
                shortestWay = pathSlot;
                _graph = graphSlot;
            }
            return listShortestWays;
        }

        private double GetPathDistance(IEnumerable<TaggedEquatableEdge<TVertex, double>> edges)
        {
            var pathSum = 0.0;
            foreach (var edge in edges)
            {
                pathSum += edge.Tag;
            }
            return pathSum;
        }

        private IEnumerable<TaggedEquatableEdge<TVertex, double>> GetShortestPathInGraph(
          AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph)
        {
            // calc distances beetween the start vertex and other
            var dij = new DijkstraShortestPathAlgorithm<TVertex, TaggedEquatableEdge<TVertex, double>>(graph, e => e.Tag);
            var vis = new VertexPredecessorRecorderObserver<TVertex, TaggedEquatableEdge<TVertex, double>>();
            using (vis.Attach(dij))
                dij.Compute(_sourceVertix);

            // get shortest path from start (source) vertex to target
            IEnumerable<TaggedEquatableEdge<TVertex, double>> path;

            return vis.TryGetPath(_targetVertix, out path) ? path : null;
        }

        private AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> RemoveEdge(
          AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> old,
          TaggedEquatableEdge<TVertex, double> edgeRemoving)
        {
            // get copy of the grapth using Serialization and Deserialization
            var copyGraph = old.Clone();

            // remove the edge
            foreach (var edge in copyGraph.Edges)
            {
                if (edge == edgeRemoving)
                {
                    copyGraph.RemoveEdge(edge);
                    break;
                }
            }

            return copyGraph;
        }
    }
}