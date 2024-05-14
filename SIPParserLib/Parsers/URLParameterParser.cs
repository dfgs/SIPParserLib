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
	public class URLParameterParser : StructStringParser<URLParameter>
	{
		// must not capture ? in order to detect URI headers
		private static Regex regex = new Regex(@"^(?<Name>[^=]+)(=(?<Value>[^?]+))?$");
		
		public URLParameterParser(ILogger Logger) : base(Logger)
		{
		}
		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };

		protected override bool OnParse(Regex Regex, Match Match, out URLParameter? Result)
		{
			string name;
			string? value;

			LogEnter();

			Result = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].MatchedValue();
			Result=new URLParameter(name, value);

			return true;
		}


	}
}
