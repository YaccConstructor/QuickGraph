using GraphX.PCL.Common.Models;

namespace QuickGraph.GraphXAdapter
{
    public class GraphXEdge<TVertex> : EdgeBase<TVertex>
    {
        public GraphXEdge(TVertex source, TVertex target) : base(source, target)
        {
        }
    }
}
