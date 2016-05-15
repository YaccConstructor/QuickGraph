using QuickGraph.GraphXAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperForKruskalAndPrimVisualisation
{
    public class EdgeForVisualisation
    {
        public GraphXTaggedEdge<GraphXVertex, int> edge;
        public bool isConteins;
        public int number;
        public EdgeForVisualisation(GraphXTaggedEdge<GraphXVertex, int> a, bool b, int c)
        {
            edge = a;
            isConteins = b;
            number = c;
        }
    }
}
