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
	public class URIHeaderParser : StructStringParser<URIHeader>
	{
		private static Regex regex = new Regex(@"^(?<Name>[^=]+)=(?<Value>.+)$");
		
		public URIHeaderParser(ILogger Logger) : base(Logger)
		{
		}
		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };


		protected override bool OnParse(Regex Regex, Match Match, out URIHeader? Result)
		{
			string name;
			string value;

			LogEnter();

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].Value;

			Result= new URIHeader(name, value);
			return true;
		}


	}
}
