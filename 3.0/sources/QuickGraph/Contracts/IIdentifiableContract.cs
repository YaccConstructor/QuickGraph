#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IIdentifiable))]
    sealed class IIdentifiableContract
        : IIdentifiable
    {
        string IIdentifiable.ID
        {
            get 
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return Contract.Result<string>();
            }
        }
    }
}
#endif