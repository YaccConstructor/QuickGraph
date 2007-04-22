using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    [Serializable]
    public sealed class IntVertexFactory :IVertexFactory<int>
    {
        private int current;

        public int CreateVertex()
        {
            return current++;
        }
    }
}
