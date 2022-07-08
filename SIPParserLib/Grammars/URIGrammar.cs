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
		

		public static IParser<string> User = Parse.ZeroOrMoreTimes(CommonGrammar.Unreserved.Or(CommonGrammar.Escaped).Or(Parse.AnyOf('&', '=', '+', '$', ',')));
		public static IParser<string> Password = Parse.ZeroOrMoreTimes(CommonGrammar.Unreserved.Or(CommonGrammar.Escaped).Or(Parse.AnyOf('&', '=', '+', '$', ',')));

		public static IParser<UserInfo> UserInfo = from user in User 
												 from password in Parse.ZeroOrOneTime(
													 from _ in Parse.Char(':')
													 from password in Password
													 select password
													 ) 
												 select new UserInfo(user,password);

		
		public static IParser<string> DomainLabel = CommonGrammar.Alphanum.OneOrMoreTimes().Then(
			Parse.ZeroOrMoreTimes(Parse.Char('-').Then(CommonGrammar.Alphanum.OneOrMoreTimes()))
			);
			

		public static IParser<string> TopLabel = CommonGrammar.Alpha.Or(CommonGrammar.Alpha.Then(Parse.ZeroOrMoreTimes(CommonGrammar.Alphanum.Or(Parse.Char('-')).Then(CommonGrammar.Alphanum))));

		public static IParser<string> IPv4Address = CommonGrammar.Digit.OneOrMoreTimes().Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes()).Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes()).Then(Parse.Char('.')).Then(CommonGrammar.Digit.OneOrMoreTimes());

		//*( domainlabel "." ) toplabel [ "." ]
		public static IParser<string> Hostname = Parse.ZeroOrMoreTimes(DomainLabel.Then(Parse.Char('.')).Then(TopLabel).Then(Parse.ZeroOrOneTime(Parse.Char('.'))));
		public static IParser<string> Host = Hostname.Or(IPv4Address);

		public static IParser<ushort> Port = from value in Parse.ZeroOrMoreTimes(CommonGrammar.Digit) select UInt16.Parse(value);

		public static IParser<HostPort> HostPort = from host in Host
												 from port in Parse.ZeroOrOneTime(
												 from _ in Parse.Char(':')
												 from port in Port select port)
												 select new HostPort(host,port);

		public static IParser<string> URLParameters = Parse.String("sip:");
		public static IParser<string> Headers = Parse.String("sip:");

		public static IParser<URI> RequestURI = 
			from _ in Parse.String("sip:") 
			from userInfo in UserInfo.ZeroOrOneTime() 
			from hostPort in HostPort 
			from urlParameters in URLParameters
			from headers in Headers
			select new URI(userInfo);


	}
}
