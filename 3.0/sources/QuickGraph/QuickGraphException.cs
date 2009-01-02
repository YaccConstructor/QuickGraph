using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    [global::System.Serializable]
    public abstract class QuickGraphException : ApplicationException
    {
        protected QuickGraphException() { }
        protected QuickGraphException(string message) : base(message) { }
        protected QuickGraphException(string message, Exception inner) : base(message, inner) { }
        protected QuickGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
