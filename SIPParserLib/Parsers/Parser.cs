using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLib;
using ModuleLib;

namespace SIPParserLib.Parsers
{
	public abstract class Parser<T> : Module,IParser<T>
	{
		public Parser(ILogger Logger) : base(Logger)
		{
		}


	}
}
