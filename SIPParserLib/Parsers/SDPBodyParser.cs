using LogLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using StreamReader = System.IO.StreamReader;

namespace SIPParserLib.Parsers
{
	public class SDPBodyParser : StreamParser<SDP>, IParser<SDP>
	{
		private IClassStringParser<SDPField> sdpFieldParser;
		
		public SDPBodyParser(ILogger Logger,IClassStringParser<SDPField> SDPFieldParser) : base(Logger)
		{
			AssertParameterNotNull(SDPFieldParser, nameof(SDPFieldParser), out sdpFieldParser);

		}
		public SDPBodyParser(ILogger Logger) : this(Logger,new SDPFieldParser(Logger))
		{
		}

		private string? ReadLine(StreamReader Reader)
		{
			string? line;

			line = null;

			if (!Try(() => Reader.ReadLine()).Then(result => line = result).OrWarn("Failed to read line from stream")) return null;
			if (line == null) return "";// Log(LogLevels.Warning, "Failed to read line from stream");

			return line;
		}

		public override SDP? Parse(Stream Stream)
		{
			string? line;
			StreamReader reader;
			SDPField? field;
			List<SDPField> fields;

			LogEnter();

			AssertParameterNotNull(Stream,nameof(Stream),LogLevels.Fatal,true);

			line = null;

			fields = new List<SDPField>();

			Log(LogLevels.Information, "Starting to parse stream");
			reader = new StreamReader(Stream);
			while (!reader.EndOfStream)
			{
				line = ReadLine(reader);
				if (line == null) return null;


				Log(LogLevels.Debug, $"Parsing SDP field: {line}");
				if (!sdpFieldParser.Parse(line, out field, true) || (field== null))
				{
					Log(LogLevels.Error, $"Invalid field: {line}");
					return null;
				}
				fields.Add(field);
			}

			return new SDP(fields.ToArray());
		}


	}
}
