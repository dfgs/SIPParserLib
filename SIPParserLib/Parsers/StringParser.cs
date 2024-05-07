using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLib;
using ModuleLib;

namespace SIPParserLib.Parsers
{
	public abstract class StringParser<T> : Parser<T>,IStringParser<T>
	{
		public StringParser(ILogger Logger) : base(Logger)
		{
		}

		public abstract T? Parse(string Line);
	}
}
