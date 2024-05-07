using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class SIPMessageParser : StreamParser<SIPMessage>, IParser<SIPMessage>
	{
		public SIPMessageParser(ILogger Logger) : base(Logger)
		{
		}

		public override SIPMessage? Parse(Stream Stream)
		{
			AssertParameterNotNull(Stream,nameof(Stream),LogLevels.Fatal,true);

			return null;
		}


	}
}
