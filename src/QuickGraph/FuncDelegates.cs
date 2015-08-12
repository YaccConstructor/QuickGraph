using System;

namespace QuickGraph
{
#if NET20
    public delegate void Action<T>(T value);
    public delegate void Action<T1, T2>(T1 value1, T2 value2);
    public delegate TResult Func<TResult>();
    public delegate TResult Func<T, TResult>(T value);
    public delegate TResult Func<T1, T2, TResult>(T1 value1, T2 value2);
    public delegate TResult Func<T1, T2, T3, TResult>(T1 value1, T2 value2, T3 value3);
#endif
}
