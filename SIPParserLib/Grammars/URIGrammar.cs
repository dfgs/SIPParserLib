using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public static class URIGrammar
	{

		public static ISingleParser<string> Method = Parse.String("INVITE").Or(Parse.String("ACK")).Or(Parse.String("OPTIONS")).Or(Parse.String("BYE")).Or(Parse.String("CANCEL")).Or(Parse.String("REGISTER"));

		public static ISingleParser<string> User = Parse.OneOrMoreTimes(CommonGrammar.Unreserved.Or(CommonGrammar.Escaped).Or(Parse.AnyOf('&', '=', '+', '$', ','))).ToStringParser();
		public static ISingleParser<string> Password = Parse.OneOrMoreTimes(CommonGrammar.Unreserved.Or(CommonGrammar.Escaped).Or(Parse.AnyOf('&', '=', '+', '$', ','))).ToStringParser();

		public static ISingleParser<UserInfo> UserInfo = from user in User 
												 from password in Parse.ZeroOrOneTime(
													 from _ in Parse.Char(':')
													 from password in Password
													 select password
													 ) 
												 select new UserInfo(user,password.FirstOrDefault());

		// hostname        = *(domainlabel "." ) toplabel["."]
		// domainlabel = alphanum | alphanum *(alphanum | "-") alphanum
		// toplabel = alpha | alpha *(alphanum | "-") alphanum

		public static ISingleParser<string> DomainLabel = CommonGrammar.Alphanum.OneOrMoreTimes().Then(Parse.Char('-').Then(CommonGrammar.Alphanum.OneOrMoreTimes()).ZeroOrMoreTimes()).ToStringParser();
		public static ISingleParser<string> TopLabel = CommonGrammar.Alpha.Then(CommonGrammar.Alphanum.ZeroOrMoreTimes()).Then(Parse.Char('-').Then(CommonGrammar.Alphanum.OneOrMoreTimes()).ZeroOrMoreTimes()).ToStringParser();
		public static ISingleParser<string> Hostname = URIGrammar.DomainLabel.Then(Parse.Char('.').ToStringParser().Then(TopLabel).ZeroOrMoreTimes()).ToStringParser();


		public static ISingleParser<string> IPv4Address = CommonGrammar.Digit.OneOrMoreTimes().Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes()).Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes()).Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes()).ToStringParser();

		public static ISingleParser<string> Host = IPv4Address.Or(Hostname);

		public static ISingleParser<ushort> Port = from value in Parse.ZeroOrMoreTimes(CommonGrammar.Digit).ToStringParser() select UInt16.Parse(value??"0");

		public static ISingleParser<HostPort> HostPort = from host in Host
												 from port in Parse.ZeroOrOneTime(
												 from _ in Parse.Char(':')
												 from port in Port select port)
												 select new HostPort(host,port.FirstOrDefault());


		public static ISingleParser<URLParameter> TransportParam = from name in Parse.String("transport")
															 from _ in Parse.Char('=')
														from value in Parse.String("udp").Or(Parse.String("tcp"))
															 select new URLParameter(name, value);
		public static ISingleParser<URLParameter> UserParam = from name in Parse.String("user")
														from _ in Parse.Char('=')
														 from value in Parse.String("phone").Or(Parse.String("ip"))
														select new URLParameter(name, value);
		public static ISingleParser<URLParameter> MethodParam = from name in Parse.String("method")
														  from _ in Method
														 from value in Parse.Any().OneOrMoreTimes().ToStringParser()
														  select new URLParameter(name, value);
		public static ISingleParser<URLParameter> TTLParam = from name in Parse.String("ttl")
													   from _ in Parse.Char('=')
													   from value in Parse.Byte().ToStringParser()
													   select new URLParameter(name, value);
		public static ISingleParser<URLParameter> MaddrParam = from name in Parse.String("maddr")
														 from _ in Parse.Char('=')
													   from value in Host
													   select new URLParameter(name, value);
		public static ISingleParser<URLParameter> OtherParam = from name in CommonGrammar.Alpha.OneOrMoreTimes().ToStringParser()
														 from _ in Parse.Char('=')
														 from value in Parse.Any().OneOrMoreTimes().ToStringParser()
														 select new URLParameter(name, value);

		public static ISingleParser<URLParameter> URLParameter = TransportParam.Or(UserParam).Or(MethodParam).Or(TTLParam).Or(MaddrParam).Or(OtherParam);

		public static IMultipleParser<URLParameter> URLParameters = Parse.ZeroOrMoreTimes(
			from _ in Parse.Char(';')
			from parameter in URLParameter
			select parameter
			);

	
		public static ISingleParser<Header> Header = from name in CommonGrammar.Alpha.OneOrMoreTimes().ToStringParser()
											   from _ in Parse.Char('=')
														 from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser()
											   select new Header(name, value);
		public static IMultipleParser<Header> Headers = Parse.ZeroOrOneTime<Header>(
														(from _ in Parse.Char('?') from header in Header select header).Then(
														Parse.ZeroOrMoreTimes(from __ in Parse.Char('&') from header in Header select header)
														)
													);

		public static ISingleParser<URI> RequestURI =
			from _ in Parse.String("sip:")
			from userInfo in Parse.ZeroOrOneTime( from userInfo in UserInfo from __ in Parse.Char('@') select userInfo) 
			from hostPort in HostPort 
			from urlParameters in URLParameters
			from headers in Headers
			select new URI(userInfo.FirstOrDefault(),hostPort,urlParameters.ToArray(),headers.ToArray());


	}
}
