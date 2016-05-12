using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph
{
    public interface IClusteredGraph
    {
        IEnumerable Clusters { get; }

        int ClustersCount
        { get; }

        bool Colapsed { get; set; }

        IClusteredGraph AddCluster();

        void RemoveCluster(IClusteredGraph g);
    }
}
