using LogLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class URIParser : ClassStringParser<URI>
	{
		private static Regex regex = new Regex(@"^(?<Protocol>sip:|tel:).*$");

		private IClassStringParser<SIPURL> sipURLParser;
		private IClassStringParser<TELURL> telURLParser;

		public URIParser(ILogger Logger, IClassStringParser<SIPURL> SIPURLParser, IClassStringParser<TELURL> TELURLParser) : base(Logger)
		{
			AssertParameterNotNull(SIPURLParser, nameof(SIPURLParser), out sipURLParser);
			AssertParameterNotNull(TELURLParser, nameof(TELURLParser), out telURLParser);
		}
		public URIParser(ILogger Logger) : this(Logger, new SIPURIParser(Logger),new TELURIParser(Logger))
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out URI? Value)
		{
			string? protocol;
			TELURL? telURL;
			SIPURL? sipURL;

			LogEnter();

			Value = null;

			protocol = Match.Groups["Protocol"].MatchedValue();
			switch(protocol)
			{
				case "tel:":
					if (!telURLParser.Parse(Match.Value, out telURL, true)) return false;
					Value = telURL;
					return true;
				case "sip:":
					if (!sipURLParser.Parse(Match.Value, out sipURL, true)) return false;
					Value = sipURL;
					return true;
				default:
					return false;
			}
			
			
		}
		

		
	}
}
