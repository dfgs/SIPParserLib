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
		private IClassStringParser<RequestLine> requestLineParser;

		public SIPMessageParser(ILogger Logger,IClassStringParser<RequestLine> RequestLineParser) : base(Logger)
		{
			AssertParameterNotNull(RequestLineParser, nameof(RequestLineParser), out requestLineParser);

		}
		public SIPMessageParser(ILogger Logger) : this(Logger,new RequestLineParser(Logger))
		{
		}

		public override SIPMessage? Parse(Stream Stream)
		{
			StreamReader reader;
			string? line;
			RequestLine? requestLine;
			StatusLine? statusLine;
			
			LogEnter();

			AssertParameterNotNull(Stream,nameof(Stream),LogLevels.Fatal,true);

			Log(LogLevels.Information, "Starting to parse stream");
			reader = new StreamReader(Stream);
			while (!reader.EndOfStream)
			{
				if (!Try(() => reader.ReadLine()).Then(result => line = result).OrWarn("Failed to read line from stream")) return null;
								
				

			}

			return null;
		}


	}
}
