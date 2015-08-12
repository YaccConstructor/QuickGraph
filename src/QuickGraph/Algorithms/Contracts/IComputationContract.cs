using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Contracts
{
    [ContractClassFor(typeof(IComputation))]
    abstract class IComputationContract
        : IComputation
    {
        #region IComputation Members
        object IComputation.SyncRoot
        {
            get
            {
                Contract.Ensures(Contract.Result<object>() != null);
                return null;
            }
        }

        ComputationState IComputation.State
        {
            get 
            {
                Contract.Ensures(Enum.IsDefined(typeof(ComputationState), Contract.Result<ComputationState>()));

                return default(ComputationState);
            }
        }

        void IComputation.Compute()
        {
            // todo contracts on events
        }

        void IComputation.Abort()
        {
        }

        event EventHandler IComputation.StateChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler IComputation.Started
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler IComputation.Finished
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler IComputation.Aborted
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}
