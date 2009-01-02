using System;
namespace QuickGraph
{
    /// <summary>
    /// Exception raised when an algorithm detects a non-strongly connected graph.
    /// </summary>
    [System.Serializable]
    public class NonStronglyConnectedGraphException 
        : QuickGraphException
    {
        public NonStronglyConnectedGraphException() { }
        public NonStronglyConnectedGraphException(string message) : base( message ) { }
        public NonStronglyConnectedGraphException(string message, System.Exception inner) : base( message, inner ) { }
        protected NonStronglyConnectedGraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base( info, context ) { }
    }
}
