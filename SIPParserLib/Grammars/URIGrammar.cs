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
		
		public static IParser<string> HostPort = Parse.String("sip:");
		public static IParser<string> URLParameters = Parse.String("sip:");
		public static IParser<string> Headers = Parse.String("sip:");

		public static IParser<string> RequestURI = 
			from _ in Parse.String("sip:") 
			from userInfo in UserInfo.ZeroOrOneTime() 
			from hostPort in HostPort 
			from urlParameters in URLParameters
			from headers in Headers
			select "uri";


	}
}
