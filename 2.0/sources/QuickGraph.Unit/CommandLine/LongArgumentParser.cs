using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
    internal sealed class LongArgumentParser : ArgumentParserBase
    {
        public LongArgumentParser(
            IMember member,
            ArgumentAttribute argument,
            bool isMultiple)
            : base(member, argument, isMultiple)
        { }

        public override bool Parse(object instance, string arg)
        {
            string value = this.ExtractValue(arg);
            if (value == null)
                return false;
            this.AssignValue(instance, long.Parse(value));
            return true;
        }
    }
}
