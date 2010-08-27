using System;

namespace QuickGraph
{
    public interface IPredicate<T>
    {
        bool Test(T t);
    }
}
