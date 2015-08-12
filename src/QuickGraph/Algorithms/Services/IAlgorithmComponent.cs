using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms.Services
{
    public interface IAlgorithmComponent
    {
        IAlgorithmServices Services { get; }
        T GetService<T>() where T : IService;
        bool TryGetService<T>(out T service) where T : IService;
    }
}
