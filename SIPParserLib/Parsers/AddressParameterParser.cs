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
	public class AddressParameterParser : StructStringParser<AddressParameter>
	{
		private static Regex regex = new Regex(@"^(?<Name>[^=]+)(=(?<Value>.+))?$");
		
		public AddressParameterParser(ILogger Logger) : base(Logger)
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out AddressParameter? Result)
		{
			string name;
			string? value;

			LogEnter();

			Result = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].MatchedValue();
			Result=new AddressParameter(name, value);

			return true;
		}


	}
}
