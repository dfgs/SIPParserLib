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

		
		public static ISingleParser<string> DomainLabel = CommonGrammar.Alphanum.OneOrMoreTimes().Then(Parse.Char('-').Then(CommonGrammar.Alphanum.OneOrMoreTimes()).ZeroOrMoreTimes()).ToStringParser();
		public static ISingleParser<string> TopLabel = CommonGrammar.Alpha.Then(CommonGrammar.Alphanum.ZeroOrMoreTimes()).Then(Parse.Char('-').Then(CommonGrammar.Alphanum.OneOrMoreTimes()).ZeroOrMoreTimes()).ToStringParser();
		public static ISingleParser<string> Hostname = URIGrammar.DomainLabel.Then(Parse.Char('.').ToStringParser().Then(TopLabel).ZeroOrMoreTimes()).ReaderIncludes(' ').ToStringParser();


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
		public static ISingleParser<URLParameter> TagParam = from name in Parse.String("tag")
															 from _ in Parse.Char('=')
															 from value in CommonGrammar.Token
															 select new URLParameter(name, value);
		public static ISingleParser<URLParameter> OtherParam = from name in CommonGrammar.Token
															   from _ in Parse.Char('=')
															   from value in CommonGrammar.HeaderValue
															   select new URLParameter(name, value);
		public static ISingleParser<URLParameter> EmptyParam = from name in CommonGrammar.Token
															    select new URLParameter(name,  "");

		public static ISingleParser<URLParameter> URLParameter = TransportParam.Or(UserParam).Or(MethodParam).Or(TTLParam).Or(MaddrParam).Or(TagParam).Or(OtherParam).Or(EmptyParam);

		public static IMultipleParser<URLParameter> URLParameters = Parse.ZeroOrMoreTimes(
			from _ in Parse.Char(';')
			from parameter in URLParameter
			select parameter
			);

	
		public static ISingleParser<Header> Header = from name in CommonGrammar.Token
											   from _ in Parse.Char('=')
														 from value in CommonGrammar.Escaped.Or(CommonGrammar.Alphanum.Or(Parse.Char('-')).Or(Parse.Char('@')).Or(Parse.Char(';')).Or(Parse.Char('='))).OneOrMoreTimes().ToStringParser()
											   select new Header(name, value);
		public static IMultipleParser<Header> Headers = Parse.ZeroOrOneTime<Header>(
														(from _ in Parse.Char('?') from header in Header select header).Then(
														Parse.ZeroOrMoreTimes(from __ in Parse.Char('&') from header in Header select header)
														)
													);


		public static ISingleParser<URI> RequestURI =
			from _ in Parse.String("sip:")
			from userInfo in Parse.ZeroOrOneTime(from userInfo in UserInfo from __ in Parse.Char('@') select userInfo)
			from hostPort in HostPort
			from urlParameters in URLParameters
			select
			new SIPURL(userInfo.FirstOrDefault(), hostPort, urlParameters.ToArray(), new Header[] { });


		public static ISingleParser<URI> SIPURI1 =
				from _ in Parse.String("sip:")
				from userInfo in Parse.ZeroOrOneTime(from userInfo in UserInfo from __ in Parse.Char('@') select userInfo)
				from hostPort in HostPort
				from urlParameters in URLParameters
				from headers in Headers
				select
				new SIPURL(userInfo.FirstOrDefault(), hostPort, urlParameters.ToArray(), headers.ToArray());

		public static ISingleParser<URI> SIPURI2 =
			from _ in Parse.String("sip:")
			from userInfo in Parse.ZeroOrOneTime(from userInfo in UserInfo from __ in Parse.Char('@') select userInfo)
			from hostPort in HostPort
			//from urlParameters in URLParameters
			//from headers in Headers
			select
			new SIPURL(userInfo.FirstOrDefault(), hostPort, new URLParameter[] { }, new Header[] { });

		public static ISingleParser<URI> SIPURI3 =
				from _ in Parse.String("<")
				from __ in Parse.String("sip:")
				from userInfo in Parse.ZeroOrOneTime(from userInfo in UserInfo from ___ in Parse.Char('@') select userInfo)
				from hostPort in HostPort
				from urlParameters in URLParameters
				from headers in Headers
				from ____ in Parse.String(">")
				from otherParameters in URLParameters
				select
				new SIPURL(userInfo.FirstOrDefault(), hostPort, urlParameters.Concat(otherParameters).ToArray(), headers.ToArray());


		public static ISingleParser<URI> TELURI =
				from _ in Parse.String("tel:")
				from phoneNumber in CommonGrammar.Token
			select new TELURL(phoneNumber);
		
		public static ISingleParser<URI> URI = SIPURI1.Or(SIPURI2).Or(SIPURI3).Or(TELURI);



		//public static ISingleParser<string> TagParam = from _ in Parse.String(";tag=") from value in CommonGrammar.Token select value;

		public static ISingleParser<Address> URIAddress = from uri in URI
														  select new Address("", uri);

		public static ISingleParser<Address> NamedAddress = from displayName in CommonGrammar.QuotedString.Or(CommonGrammar.Token).ZeroOrOneTime()
															from uri in URI
															select new Address(displayName.FirstOrDefault()??"", uri);

		public static ISingleParser<Address> Address = URIAddress.Or(NamedAddress);

	}
}
