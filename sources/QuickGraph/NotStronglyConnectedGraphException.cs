using System;
namespace QuickGraph
{
    [System.Serializable]
    public class NotStronglyConnectedGraphException : System.ApplicationException
    {
        public NotStronglyConnectedGraphException() { }
        public NotStronglyConnectedGraphException(string message) : base( message ) { }
        public NotStronglyConnectedGraphException(string message, System.Exception inner) : base( message, inner ) { }
        protected NotStronglyConnectedGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
