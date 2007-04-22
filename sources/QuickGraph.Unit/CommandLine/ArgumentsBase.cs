using System;

namespace QuickGraph.CommandLine
{
    public abstract class ArgumentsBase
    {
        [Argument(
            ShortName="h",
            LongName="help",
            Description="display this message")]
        public bool Help = false;

        [Argument(
            ShortName="sd",
            LongName="show-data",
            Description="displays the parsed data")]
        public bool ShowData = false;
    }
}
