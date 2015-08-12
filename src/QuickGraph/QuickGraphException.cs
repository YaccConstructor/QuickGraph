using System;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class QuickGraphException 
        : Exception
    {
        protected QuickGraphException() { }
        protected QuickGraphException(string message) : base(message) { }
        protected QuickGraphException(string message, Exception inner) : base(message, inner) { }
#if !SILVERLIGHT
        protected QuickGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }
}
