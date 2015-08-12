using System;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class NonAcyclicGraphException
        : QuickGraphException
    {
        public NonAcyclicGraphException() { }
        public NonAcyclicGraphException(string message) : base( message ) { }
        public NonAcyclicGraphException(string message, System.Exception inner) : base( message, inner ) { }
#if !SILVERLIGHT
        protected NonAcyclicGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
#endif
    }
}


