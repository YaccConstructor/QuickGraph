using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class FixtureTimedOutException : ApplicationException
    {
        public FixtureTimedOutException(string message)
            : base(message)
        { }
    }
}
