using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIPParserLib.Parsers
{
	public class SIPURIParser : ClassStringParser<SIPURL>
	{
		private static Regex regex = new Regex(@"^sip:((?<UserInfo>.*)\@)?(?<HostPort>[^;?]+)(;(?<URLParameters>[^?]+))?(\?(?<URLHeaders>.+))?$");

		private IClassStringParser<UserInfo> userInfoParser;
		private IStructStringParser<HostPort> hostPortParser;
		private IStructStringParser<URLParameter> urlParameterParser;
		private IStructStringParser<URIHeader> urlHeaderParser;

		public SIPURIParser(ILogger Logger, IClassStringParser<UserInfo> UserInfoParser, IStructStringParser<HostPort> HostPortParser, IStructStringParser<URLParameter> URLParameterParser, IStructStringParser<URIHeader> URLHeaderParser) : base(Logger)
		{
			AssertParameterNotNull(UserInfoParser, nameof(UserInfoParser), out userInfoParser);
			AssertParameterNotNull(HostPortParser, nameof(HostPortParser), out hostPortParser);
			AssertParameterNotNull(URLParameterParser, nameof(URLParameterParser), out urlParameterParser);
			AssertParameterNotNull(URLHeaderParser, nameof(URLHeaderParser), out urlHeaderParser);
		}
		public SIPURIParser(ILogger Logger) : this(Logger,new UserInfoParser(Logger),new HostPortParser(Logger),new URLParameterParser(Logger),new URIHeaderParser(Logger) )
		{
		}
		protected override Regex OnGetRegex() => regex;

		protected override bool OnParse(Match Match, out SIPURL? Value)
		{
			UserInfo? userInfo;
			HostPort? hostPort;
			URLParameter[]? parameters;
			URIHeader[]? headers;

			LogEnter();

			Value = null;

			userInfoParser.Parse(Match.Groups["UserInfo"],out userInfo,false);
			
			if (!hostPortParser.Parse(Match.Groups["HostPort"], out hostPort, true)) return false;
			if (hostPort == null) return false;

			if (!urlParameterParser.ParseAll(Match.Groups["URLParameters"], ';', out parameters, false)) return false;
			if (!urlHeaderParser.ParseAll(Match.Groups["URLHeaders"], '&', out headers, false)) return false;

			Value = new SIPURL(userInfo, hostPort.Value, parameters, headers);
			
			return true;
		}
		

		
	}
}
