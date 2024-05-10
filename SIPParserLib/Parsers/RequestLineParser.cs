using LogLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class RequestLineParser : ClassStringParser<RequestLine>
	{
		private static Regex regex = new Regex(@"^(?<Method>INVITE|ACK|PRACK|OPTIONS|BYE|CANCEL|REGISTER|UPDATE|NOTIFY|REFER|MESSAGE|SUBSCRIBE) +(?<RequestURI>.*) +(?<SIPVersion>SIP\/2\.0)$");

		private IClassStringParser<SIPURL> requestURIParser;

		public RequestLineParser(ILogger Logger,IClassStringParser<SIPURL> RequestURIParser) : base(Logger)
		{
			AssertParameterNotNull(RequestURIParser, nameof(RequestURIParser), out requestURIParser);
		}
		public RequestLineParser(ILogger Logger) : this(Logger,new SIPURIParser(Logger))
		{
		}

		protected override Regex OnGetRegex() => regex;


		protected override bool OnParse(Match Match, out RequestLine? Result)
		{
			string method;
			SIPURL? requestURI;
			string version;

			LogEnter();

			Result = null;
			
			method = Match.Groups["Method"].Value;
			version = Match.Groups["SIPVersion"].Value;

			if (!requestURIParser.Parse(Match.Groups["RequestURI"], out requestURI,true)) return false;
			if (requestURI == null) return false;

			Result=new RequestLine(method, requestURI, version);

			return true;
		}


	
	}
}
