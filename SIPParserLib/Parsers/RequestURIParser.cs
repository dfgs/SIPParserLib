using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class RequestURIParser : ClassStringParser<SIPURL>
	{
		private static Regex regex = new Regex(@"^sip:((?<UserInfo>.*)\@)?(?<HostPort>[^;]+)(;(?<URLParameters>.+))?$");

		private IClassStringParser<UserInfo> userInfoParser;
		private IStructStringParser<HostPort> hostPortParser;
		private IStructStringParser<URLParameter> urlParameterParser;

		public RequestURIParser(ILogger Logger, IClassStringParser<UserInfo> UserInfoParser, IStructStringParser<HostPort> HostPortParser, IStructStringParser<URLParameter> URLParameterParser) : base(Logger)
		{
			AssertParameterNotNull(UserInfoParser, nameof(UserInfoParser), out userInfoParser);
			AssertParameterNotNull(HostPortParser, nameof(HostPortParser), out hostPortParser);
			AssertParameterNotNull(URLParameterParser, nameof(URLParameterParser), out urlParameterParser);
		}
		public RequestURIParser(ILogger Logger) : this(Logger,new UserInfoParser(Logger),new HostPortParser(Logger),new URLParameterParser(Logger))
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out SIPURL? Value)
		{
			UserInfo? userInfo;
			HostPort? hostPort;
			URLParameter[]? parameters;

			LogEnter();

			Value = null;

			userInfoParser.Parse(Match.Groups["UserInfo"],out userInfo,false);
			
			if (!hostPortParser.Parse(Match.Groups["HostPort"], out hostPort, true)) return false;
			if (hostPort == null) return false;

			if (!urlParameterParser.ParseAll(Match.Groups["URLParameters"], ';',out parameters,false)) return false;

			Value = new SIPURL(userInfo, hostPort.Value, parameters, new Header[] { });
			
			return true;
		}
		

		
	}
}
