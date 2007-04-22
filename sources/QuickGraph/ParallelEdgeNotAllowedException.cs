using System;

namespace QuickGraph
{
    [System.Serializable]
    public sealed class ParallelEdgeNotAllowedException : System.ApplicationException
    {
        public ParallelEdgeNotAllowedException() { }
        public ParallelEdgeNotAllowedException(string message) : base( message ) { }
        public ParallelEdgeNotAllowedException(string message, System.Exception inner) : base( message, inner ) { }
        public ParallelEdgeNotAllowedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
