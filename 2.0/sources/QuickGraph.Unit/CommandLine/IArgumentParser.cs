using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuickGraph.CommandLine
{
    internal interface IArgumentParser
    {
        IMember Member { get;}
        ArgumentAttribute Argument { get;}
        bool IsMultiple { get;}

        bool Parse(object instance, string arg);
        void ShowHelp(TextWriter writer);
        void ShowData(object instance, TextWriter writer);
    }
}
