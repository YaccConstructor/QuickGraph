using System;

namespace QuickGraph.Unit.Exceptions
{
    [System.Serializable]
    public sealed class DebugFailureException : System.ApplicationException
    {
        public DebugFailureException() { }
        public DebugFailureException(string message) : base( message ) { }
        public DebugFailureException(string message, System.Exception inner) : base( message, inner ) { }
        public DebugFailureException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
