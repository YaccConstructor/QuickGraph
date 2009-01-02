using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{    
    [global::System.Serializable]
    public class NegativeCycleGraphException
        : QuickGraphException
    {
        public NegativeCycleGraphException() { }
        public NegativeCycleGraphException(string message) : base(message) { }
        public NegativeCycleGraphException(string message, Exception inner) : base(message, inner) { }
        protected NegativeCycleGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
