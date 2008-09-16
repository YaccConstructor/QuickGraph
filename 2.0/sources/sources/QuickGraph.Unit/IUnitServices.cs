using System.Reflection;
using System.ComponentModel;

namespace QuickGraph.Unit
{
    public interface IUnitServices
    {
        ILoggerService GetLoggerService();
    }
}
