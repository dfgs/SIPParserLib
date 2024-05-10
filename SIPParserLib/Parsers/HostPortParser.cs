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
	public class HostPortParser : StructStringParser<HostPort>
	{
		private static Regex regex = new Regex(@"^(?<Host>[^:]+)(:(?<Port>\d+))?$");
		
		public HostPortParser(ILogger Logger) : base(Logger)
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out HostPort? Result)
		{
			string host;
			string? port;

			LogEnter();
			host = Match.Groups["Host"].Value;
			port = Match.Groups["Port"].MatchedValue();
			if (port == null) Result= new HostPort(host, null);
			else Result=new HostPort(host, ushort.Parse(port));
			
			return true;
		}

		


	}
}
