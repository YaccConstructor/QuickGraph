using System;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class ExceptionMessageDoesNotMatchException : Exception
    {
        public ExceptionMessageDoesNotMatchException(
            INameMatcher matcher,
            Exception innerException
            ) 
            :base(
                String.Format("Exception message {0} does not match {1}", innerException.Message, matcher),
                innerException)
        { }
    }
}
