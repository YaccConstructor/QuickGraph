using System;

namespace QuickGraph
{
#if NET20
    public delegate TResult Func<TResult>();
    public delegate TResult Func<T, TResult>(T value);
#endif
}
