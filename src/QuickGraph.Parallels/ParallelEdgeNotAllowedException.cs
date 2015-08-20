using System;

namespace QuickGraph
{
    [System.Serializable]
    public class ParallelEdgeNotAllowedException : System.ApplicationException
    {
        public ParallelEdgeNotAllowedException() { }
        public ParallelEdgeNotAllowedException(string message) : base( message ) { }
        public ParallelEdgeNotAllowedException(string message, System.Exception inner) : base( message, inner ) { }
        protected ParallelEdgeNotAllowedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
