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
	public class AddressParser : StructStringParser<Address>
	{

		// If URI contains ? or , Address Parameters are present
		private static Regex regex = new Regex(@"^((?<DisplayName>([^< ""]+)|(""[^""]+"")) *)?<(?<URI>[^>?]+)(\?(?<AddressParameters1>[^>]+))?>(;(?<AddressParameters2>.+))?$");
		private IClassStringParser<URI> uriParser;
		private IStructStringParser<AddressParameter> addressParameterParser;

		public AddressParser(ILogger Logger, IClassStringParser<URI> URIParser, IStructStringParser<AddressParameter> AddressParameterParser) : base(Logger)
		{
			AssertParameterNotNull(URIParser, nameof(URIParser), out uriParser);
			AssertParameterNotNull(AddressParameterParser, nameof(AddressParameterParser), out addressParameterParser);
		}
		public AddressParser(ILogger Logger) : this(Logger,new URIParser(Logger),new AddressParameterParser(Logger) )
		{
		}


		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out Address? Value)
		{
			URI? uri;
			AddressParameter[]? parameters1;
			AddressParameter[]? parameters2;
			AddressParameter[]? joinedParameters;
			string? displayName;

			LogEnter();

			Value = null;

			if (!uriParser.Parse(Match.Groups["URI"], out uri, true)) return false;
			if (uri == null) return false;

			// parse parameters before > and after >
			if (!addressParameterParser.ParseAll(SIPString.Unescape(Match.Groups["AddressParameters1"].MatchedValue()), ';', out parameters1, false)) return false;
			if (!addressParameterParser.ParseAll( Match.Groups["AddressParameters2"], ';', out parameters2, false)) return false;

			displayName = Match.Groups["DisplayName"].MatchedValue();
			if (displayName != null) displayName = displayName.Trim('"');

			// join parameters before > and after >
			if (parameters1 != null)
			{
				if (parameters2 != null)
				{
					joinedParameters = parameters1.Concat(parameters2).ToArray();
				}
				else joinedParameters = parameters1;
			}
			else if (parameters2 != null)
			{
				joinedParameters = parameters2;
			}
			else joinedParameters = null;

			Value = new Address(displayName,uri, joinedParameters);

			return true;
		}


	}
}
