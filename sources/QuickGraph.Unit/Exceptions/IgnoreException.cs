using System;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class IgnoreException : UnitException
    {
        //
        // For guidelines regarding the creationg of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public IgnoreException() { }
        public IgnoreException(string message) : base(message) { }
        public IgnoreException(string message, Exception inner) : base(message, inner) { }
    }
}
