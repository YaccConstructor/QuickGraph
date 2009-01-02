using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    [global::System.Serializable]
    public class NegativeWeightException : ApplicationException
    {
        public NegativeWeightException() { }
        public NegativeWeightException(string message) : base(message) { }
        public NegativeWeightException(string message, Exception inner) : base(message, inner) { }
        protected NegativeWeightException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
