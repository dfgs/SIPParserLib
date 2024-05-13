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
	public class SDPFieldParser : ClassStringParser<SDPField>
	{
		private static Regex regex = new Regex(@"^(?<Name>.)=(?<Value>.+)$");
		private static Regex attributeRegex = new Regex(@"^(?<Name>[^:]+)(:(?<Value>.+))?$");
		private static Regex mediaRegex = new Regex(@"^(?<Media>[^ ]+) (?<Port>\d+) (?<Protocol>[^ ]+) (?<Format>.+)$");

		private IStructStringParser<Address> addressParser;
		private IClassStringParser<ViaParameter> viaParameterParser;

		public SDPFieldParser(ILogger Logger,IStructStringParser<Address> AddressParser,IClassStringParser<ViaParameter> ViaParameterParser) : base(Logger)
		{
			AssertParameterNotNull(AddressParser, nameof(AddressParser), out addressParser);
			AssertParameterNotNull(ViaParameterParser, nameof(ViaParameterParser), out viaParameterParser);
		}

		public SDPFieldParser(ILogger Logger) : this(Logger, new AddressParser(Logger), new ViaParameterParser(Logger) )
		{
		}

		protected override Regex OnGetRegex() => regex;


		protected override bool OnParse(Match Match, out SDPField? Result)
		{
			string name;
			string? value;
			Match match;
			//ViaParameter[]? viaParameters;
			byte version;
			uint t1, t2;
			string[] parts;
			ushort port;

			LogEnter();

			Result = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].Value;

			switch(name)
			{
				case "v":
					if (!byte.TryParse(value, out version))
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid version ({value})");
						return false;
					}
					Result = new VersionField(version);
					return true;
				case "o":
					parts = value.Split(" ");
					if (parts.Length != 6)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid origin format ({value})");
						return false;
					}
					Result = new OriginField(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
					return true;
				case "s":
					Result = new SessionNameField(value);
					return true;
				case "i":
					Result = new SessionInformationField(value);
					return true;
				case "c":
					parts = value.Split(" ");
					if (parts.Length != 3)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid connection format ({value})");
						return false;
					}
					Result = new ConnectionField(parts[0], parts[1], parts[2]);
					return true;
				case "t":
					parts = value.Split(" ");
					if (parts.Length != 2)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid port format ({value})");
						return false;
					}
					if (!UInt32.TryParse(parts[0], out t1))
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid time format ({value})");
						return false;
					}
					if (!UInt32.TryParse(parts[1], out t2))
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid time format ({value})");
						return false;
					}
					Result = new TimingField(t1,t2);
					return true;
				case "m":
					match = mediaRegex.Match(value);
					if (!match.Success)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid media format ({value})");
						return false;
					}
					if (!ushort.TryParse(match.Groups["Port"].Value, out port))
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid port format ({value})");
						return false;
					}
					Result = new MediaField(match.Groups["Media"].Value, port, match.Groups["Protocol"].Value, match.Groups["Format"].Value);
					return true;
				case "a":
					//rtpmap:101 telephone-event/8000
					match = attributeRegex.Match(value);
					if (!match.Success)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid attribute format ({value})");
						return false;
					}
					Result = new AttributeField(new SDPAttribute(match.Groups["Name"].Value, match.Groups["Value"].Value));
					return true;

				default:
					Result=new CustomSDPField(name, value);
					return true;
			}
			

		}


	}
}
