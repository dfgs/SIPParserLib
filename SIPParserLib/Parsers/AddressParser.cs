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
		private static Regex regex = new Regex(@"^<(?<URI>.*)>(;(?<AddressParameters>.+))?$");
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
			AddressParameter[]? parameters;

			LogEnter();

			Value = null;

			if (!uriParser.Parse(Match.Groups["URI"], out uri, true)) return false;
			if (uri == null) return false;

			if (!addressParameterParser.ParseAll(Match.Groups["AddressParameters"],';', out parameters, false)) return false;
			
			Value = new Address("",uri,parameters);

			return true;
		}


	}
}
