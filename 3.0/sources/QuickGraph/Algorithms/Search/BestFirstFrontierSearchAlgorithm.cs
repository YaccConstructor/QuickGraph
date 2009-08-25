using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// NOT FINISHED
    /// </summary>
    /// <remarks>
    /// Algorithm from Frontier Search, Korkf, Zhand, Thayer, Hohwald.
    /// </remarks>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    internal sealed class BestFirstFrontierSearchAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex, IImplicitGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private TVertex targetVertex;
        private bool hasTargetVertex;

        public bool TryGetTargetVertex(out TVertex targetVertex)
        {
            targetVertex = this.targetVertex;
            return this.hasTargetVertex;
        }

        public TVertex TargetVertex
        {
            set
            {
                this.targetVertex = value;
                this.hasTargetVertex = this.targetVertex != null;
            }
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();

            var cancelManager = this.Services.CancelManager;

        }
    }
}
