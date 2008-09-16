using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
    internal sealed class BoolArgumentParser : ArgumentParserBase
    {
        public BoolArgumentParser(
            IMember member,
            ArgumentAttribute argument,
            bool isMultiple)
            :base(member,argument,isMultiple)
        { }

        public override bool Parse(object instance, string arg)
        {
            if (IsFlag(arg[0]))
                return false;

            bool value = true;
            int index = -1;
            switch (arg[arg.Length - 1])
            {
                case '+':
                    value = true;
                    index = arg.Length - 1;
                    break;
                case '-':
                    value = false;
                    index = arg.Length - 1;
                    break;
                default:
                    index = arg.Length;
                    break;
            }

            // check name is correct
            string name = arg.Substring(1, index - 1);
            if (name != this.Argument.LongName &&
                name != this.Argument.ShortName)
                return false;

            // assign value
            this.AssignValue(instance, value);
            return true;
        }

        protected override string GetValidValues()
        {
            return "[+-]";
        }
    }
}
