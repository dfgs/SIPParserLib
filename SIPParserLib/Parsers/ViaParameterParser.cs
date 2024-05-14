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
	public class ViaParameterParser : ClassStringParser<ViaParameter>
	{
		private static Regex regex = new Regex(@"^(?<Name>[^=]+)(=(?<Value>[^?]+))?$");
		private IStructStringParser<HostPort> hostPortParser;

		public ViaParameterParser(ILogger Logger, IStructStringParser<HostPort> HostPortParser) : base(Logger)
		{
			AssertParameterNotNull(HostPortParser, nameof(HostPortParser), out hostPortParser);
		}
		public ViaParameterParser(ILogger Logger):this(Logger,new HostPortParser(Logger))
		{

		}

		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex };

		protected override bool OnParse(Regex Regex, Match Match, out ViaParameter? Value)
		{
			string name;
			string? value;
			byte ttl;
			HostPort? hostPort;

			LogEnter();

			Value = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].MatchedValue();

			switch(name)
			{
				case "branch":
					Value = new ViaBranch(value);
					return true;
				case "received":
					if (!hostPortParser.Parse(value, out hostPort, true)) return false;
					if (hostPort == null) return false;
					Value = new ViaReceived(hostPort.Value);
					return true;
				case "maddr":
					Value = new ViaMAddr(value);
					return true;
				case "ttl":
					if (!byte.TryParse(value, out ttl)) return false;
					Value = new ViaTTL(ttl);
					return true;
				case "hidden":
					Value = new ViaHidden();
					return true;
				default:
					Value = new ViaCustomParameter(name, value);
					return true;
			}

		}


	}
}
