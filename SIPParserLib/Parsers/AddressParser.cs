using LogLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class AddressParser : StructStringParser<Address>
	{

		// If URI contains ? or , Address Parameters are present
		private static Regex regex1 = new Regex(@"^((?<DisplayName>([^< ""]+)|(""[^""]+"")) *)?<(?<URI>[^>?]+)(\?(?<AddressParameters1>[^>]+))?>(;(?<AddressParameters2>.+))?$");
		private static Regex regex2 = new Regex(@"^sip:((?<UserInfo>.*)\@)?(?<HostPort>[^;]+)(;(?<AddressParameters>.+))?$");

		private IClassStringParser<URI> uriParser;
		private IStructStringParser<AddressParameter> addressParameterParser;
		private IClassStringParser<UserInfo> userInfoParser;
		private IStructStringParser<HostPort> hostPortParser;

		public AddressParser(ILogger Logger, IClassStringParser<URI> URIParser, IStructStringParser<AddressParameter> AddressParameterParser, IClassStringParser<UserInfo> UserInfoParser, IStructStringParser<HostPort> HostPortParser) : base(Logger)
		{
			AssertParameterNotNull(URIParser, nameof(URIParser), out uriParser);
			AssertParameterNotNull(AddressParameterParser, nameof(AddressParameterParser), out addressParameterParser);
			AssertParameterNotNull(UserInfoParser, nameof(UserInfoParser), out userInfoParser);
			AssertParameterNotNull(HostPortParser, nameof(HostPortParser), out hostPortParser);
		}
		public AddressParser(ILogger Logger) : this(Logger,new URIParser(Logger),new AddressParameterParser(Logger),new UserInfoParser(Logger),new HostPortParser(Logger) )
		{
		}


		protected override IEnumerable<Regex> OnGetRegexes() => new Regex[] { regex1,regex2 };

		protected override bool OnParse(Regex Regex,Match Match, out Address? Value)
		{
			URI? uri;
			AddressParameter[]? parameters1;
			AddressParameter[]? parameters2;
			AddressParameter[]? joinedParameters;
			string? displayName;
			UserInfo? userInfo;
			HostPort? hostPort;

			LogEnter();
			
			Value = null;

			if (Regex == regex1)
			{

				if (!uriParser.Parse(Match.Groups["URI"], out uri, true)) return false;
				if (uri == null) return false;

				// parse parameters before > and after >
				if (!addressParameterParser.ParseAll(SIPString.Unescape(Match.Groups["AddressParameters1"].MatchedValue()), ';', out parameters1, false)) return false;
				if (!addressParameterParser.ParseAll(Match.Groups["AddressParameters2"], ';', out parameters2, false)) return false;

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

				Value = new Address(displayName, uri, joinedParameters);

				return true;
			}
			if (Regex==regex2)
			{
				userInfoParser.Parse(Match.Groups["UserInfo"], out userInfo, false);

				if (!hostPortParser.Parse(Match.Groups["HostPort"], out hostPort, true)) return false;
				if (hostPort == null) return false;

				if (!addressParameterParser.ParseAll(SIPString.Unescape(Match.Groups["AddressParameters"].MatchedValue()), ';', out parameters1, false)) return false;

				uri = new SIPURL(userInfo, hostPort.Value,null, null);
				Value = new Address(null, uri, parameters1);

				return true;
			}

			return false;

		}


	}
}
