using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLib;
using ModuleLib;

namespace SIPParserLib.Parsers
{
	public abstract class StreamParser<T> : Parser<T>,IStreamParser<T>
	{
		public StreamParser(ILogger Logger) : base(Logger)
		{
		}

		public abstract T? Parse(Stream Stream);
	}
}
