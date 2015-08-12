using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Services
{
    /// <summary>
    /// Common services available to algorithm instances
    /// </summary>
    public interface IAlgorithmServices
    {
        ICancelManager CancelManager { get; }
    }

    class AlgorithmServices :
        IAlgorithmServices
    {
        readonly IAlgorithmComponent host;

        public AlgorithmServices(IAlgorithmComponent host)
        {
            Contract.Requires(host != null);

            this.host = host;
        }

        ICancelManager _cancelManager;
        public ICancelManager CancelManager
        {
            get 
            {
                if (this._cancelManager == null)
                    this._cancelManager = this.host.GetService<ICancelManager>();
                return this._cancelManager; 
            }
        }
    }
}
