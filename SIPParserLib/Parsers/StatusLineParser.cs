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
	public class StatusLineParser : ClassStringParser<StatusLine>
	{
		private static Regex regex = new Regex(@"^SIP\/2\.0 (?<Code>\d\d\d) (?<Reason>.+)$");


		public StatusLineParser(ILogger Logger) : base(Logger)
		{
		}
		

		protected override Regex OnGetRegex() => regex;


		protected override bool OnParse(Match Match, out StatusLine? Result)
		{
			string code;
			string reason;
			ushort codeValue;

			LogEnter();

			Result = null;
			
			code = Match.Groups["Code"].Value;
			reason = Match.Groups["Reason"].Value;

			if (!ushort.TryParse(code, out codeValue)) return false;

			Result=new StatusLine(codeValue,reason);

			return true;
		}


	
	}
}
