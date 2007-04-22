using System;
namespace QuickGraph
{
    [System.Serializable]
    public sealed class NotStronglyConnectedGraphException : System.ApplicationException
    {
        public NotStronglyConnectedGraphException() { }
        public NotStronglyConnectedGraphException(string message) : base( message ) { }
        public NotStronglyConnectedGraphException(string message, System.Exception inner) : base( message, inner ) { }
        public NotStronglyConnectedGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
