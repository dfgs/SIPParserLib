using LogLib;
using SIPParserLib;
using SIPParserLib.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class TELURIParser : ClassStringParser<TELURL>
	{
		private static Regex regex = new Regex(@"^tel:(?<Number>.+)$");

		public TELURIParser(ILogger Logger) : base(Logger)
		{
		}

		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };

		protected override bool OnParse(Regex Regex, Match Match, out TELURL? Value)
		{
			string? number;
			

			LogEnter();

			Value = null;

			number = Match.Groups["Number"].MatchedValue();
			if (number == null) return false;

			Value = new TELURL(number);
			
			return true;
		}
		

		
	}
}
