using System;
using System.Collections.Generic;

namespace QuickGraph.CommandLine
{
    internal class ArgumentParserCollection : List<IArgumentParser>
    {
        public IArgumentParser GetParserFromShortName(string name)
        {
            foreach (IArgumentParser parser in this)
            {
                if (parser.Argument.ShortName == name ||
                    parser.Argument.LongName == name)
                    return parser;
            }
            return null;
        }

        public IArgumentParser GetDefaultParser()
        {
            foreach (IArgumentParser parser in this)
            {
                if (parser.Argument.IsDefault)
                    return parser;
            }
            return null;
        }

        public bool VerifyParsers()
        {
            Dictionary<string, IArgumentParser> namedParsers = new Dictionary<string, IArgumentParser>();
            IArgumentParser defaultParser = null;
            int errorCount = 0;
            foreach (IArgumentParser parser in this)
            {
                if (defaultParser!=null && parser.Argument.IsDefault)
                {
                    Console.WriteLine("Duplicate default argument ({0}, {1})", 
                        defaultParser.Member.Name,
                        parser.Member.Name);
                    errorCount++;
                    continue;
                }

                if (parser.Argument.IsDefault)
                {
                    defaultParser = parser;
                    continue;
                }

                if (namedParsers.ContainsKey(parser.Argument.ShortName))
                {
                    Console.WriteLine("Short name {0} of {1} is a duplicate of {2}",
                        parser.Argument.ShortName,
                        parser.Member.Name,
                        namedParsers[parser.Argument.ShortName].Member.Name
                        );
                    errorCount++;
                    continue;
                }

                if (namedParsers.ContainsKey(parser.Argument.LongName))
                {
                    Console.WriteLine("Long name {0} of {1} is a duplicate of {2}",
                        parser.Argument.LongName,
                        parser.Member.Name,
                        namedParsers[parser.Argument.ShortName].Member.Name
                        );
                    errorCount++;
                    continue;
                }
            }

            if (errorCount != 0)
                Console.WriteLine("Found {0} errors", errorCount);
            return errorCount == 0;
        }
    }
}
