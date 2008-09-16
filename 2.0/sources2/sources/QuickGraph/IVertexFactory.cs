using System;

namespace QuickGraph
{
    public interface IVertexFactory<TVertex>
    {
        TVertex CreateVertex();
    }
}
