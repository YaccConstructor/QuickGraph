using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public class AssumptionFailureException : ApplicationException
    {
        public AssumptionFailureException() { }
        public AssumptionFailureException(string message) : base(message) { }
        public AssumptionFailureException(string message, Exception inner) : base(message, inner) { }
        protected AssumptionFailureException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
