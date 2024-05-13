using LogLib;
using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using StreamReader = System.IO.StreamReader;

namespace SIPParserLib.Parsers
{
	public class SIPMessageParser : StreamParser<SIPMessage>, IParser<SIPMessage>
	{
		private IClassStringParser<RequestLine> requestLineParser;
		private IClassStringParser<StatusLine> statusLineParser;
		private IClassStringParser<MessageHeader> messageHeaderParser;

		public SIPMessageParser(ILogger Logger,IClassStringParser<RequestLine> RequestLineParser, IClassStringParser<StatusLine> StatusLineParser, IClassStringParser<MessageHeader> MessageHeaderParser) : base(Logger)
		{
			AssertParameterNotNull(RequestLineParser, nameof(RequestLineParser), out requestLineParser);
			AssertParameterNotNull(StatusLineParser, nameof(StatusLineParser), out statusLineParser);
			AssertParameterNotNull(MessageHeaderParser, nameof(MessageHeaderParser), out messageHeaderParser);

		}
		public SIPMessageParser(ILogger Logger) : this(Logger,new RequestLineParser(Logger),new StatusLineParser(Logger),  new MessageHeaderParser(Logger) )
		{
		}

		private string? ReadLine(StreamReader Reader)
		{
			string? line;

			line = null;

			if (!Try(() => Reader.ReadLine()).Then(result => line = result).OrWarn("Failed to read line from stream")) return null;
			if (line == null) Log(LogLevels.Warning, "Failed to read line from stream");

			return line;
		}

		private Request? ParseRequest(StreamReader reader,string? line)
		{
			RequestLine? requestLine;
			List<MessageHeader> headers;
			MessageHeader? header;
			string body;

			LogEnter();

			headers = new List<MessageHeader>();

			Log(LogLevels.Information, $"Parsing SIP request: {line}");
			if (!requestLineParser.Parse(line, out requestLine, true) || (requestLine == null))
			{
				Log(LogLevels.Error, $"Invalid request line: {line}");
				return null;
			}

			Log(LogLevels.Information, "Parsing SIP headers");
			do
			{
				line = ReadLine(reader);
				if (line == null)
				{
					Log(LogLevels.Error, $"End of stream reached");
					return null;
				}

				// end of headers
				if (line == "") break;

				Log(LogLevels.Debug, $"Parsing header: {line}");
				if (!messageHeaderParser.Parse(line, out header, true) || (header == null))
				{
					Log(LogLevels.Error, $"Invalid header: {line}");
					return null;
				}
				headers.Add(header);
			} while (true);

			body = "";
			Log(LogLevels.Information, "Parsing body");
			do
			{
				line = ReadLine(reader);
				if (line == null)
				{
					Log(LogLevels.Error, $"End of stream reached");
					return null;
				}

				// end of body
				if (line == "") break;

				body += line + "\r\n";
			} while (true);

			return new Request(requestLine, headers.ToArray(), body);
		}
		private Response? ParseResponse(StreamReader reader, string? line)
		{
			StatusLine? statusLine;
			List<MessageHeader> headers;
			MessageHeader? header;
			string body;

			LogEnter();

			headers = new List<MessageHeader>();

			Log(LogLevels.Information, $"Parsing SIP response: {line}");
			if (!statusLineParser.Parse(line, out statusLine, true) || (statusLine == null))
			{
				Log(LogLevels.Error, $"Invalid status line: {line}");
				return null;
			}

			Log(LogLevels.Information, "Parsing SIP headers");
			do
			{
				line = ReadLine(reader);
				if (line == null)
				{
					Log(LogLevels.Error, $"End of stream reached");
					return null;
				}

				// end of headers
				if (line == "") break;

				Log(LogLevels.Debug, $"Parsing header: {line}");
				if (!messageHeaderParser.Parse(line, out header, true) || (header == null))
				{
					Log(LogLevels.Error, $"Invalid header: {line}");
					return null;
				}
				headers.Add(header);
			} while (true);

			body = "";
			Log(LogLevels.Information, "Parsing body");
			do
			{
				line = ReadLine(reader);
				if (line == null)
				{
					Log(LogLevels.Error, $"End of stream reached");
					return null;
				}

				// end of body
				if (line == "") break;

				body += line + "\r\n";
			} while (true);

			return new Response(statusLine, headers.ToArray(), body);
		}
		public override SIPMessage? Parse(Stream Stream)
		{
			string? line;
			StreamReader reader;

			LogEnter();

			AssertParameterNotNull(Stream,nameof(Stream),LogLevels.Fatal,true);

			line = null;

			Log(LogLevels.Information, "Starting to parse stream");
			reader = new StreamReader(Stream);
			while (!reader.EndOfStream)
			{
				line = ReadLine(reader);
				if (line == null) return null;

				if (line.StartsWith("SIP/2.0"))
				{
					return ParseResponse(reader, line);
				}
				else
				{
					return ParseRequest(reader, line);
				}
			}

			return null;
		}


	}
}
