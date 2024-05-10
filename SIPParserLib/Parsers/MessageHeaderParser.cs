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
	public class MessageHeaderParser : ClassStringParser<MessageHeader>
	{
		private static Regex regex = new Regex(@"^(?<Name>[^:]+): *(?<Value>.+)?$");
		
		public MessageHeaderParser(ILogger Logger) : base(Logger)
		{
		}
		protected override Regex OnGetRegex() => regex;


		protected override bool OnParse(Match Match, out MessageHeader? Result)
		{
			string name;
			string? value;

			LogEnter();

			Result = null;

			name = Match.Groups["Name"].Value;
			value = Match.Groups["Value"].MatchedValue();

			switch(name)
			{
				case "Accept":
					Result = new AcceptHeader(value);
					return true;
				case "Accept-Encoding":
					Result = new AcceptEncodingHeader(value);
					return true;
				case "Accept-Language":
					Result = new AcceptLanguageHeader(value);
					return true;
				case "Allow":
					Result = new AllowHeader(value);
					return true;
				case "Authorization":
					Result = new AuthorizationHeader(value);
					return true;
				case "Call-ID":
					Result = new CallIDHeader(value);
					return true;
				case "Contact":
					Result = new ContactHeader(value);
					return true;
				case "Content-Encoding":
					Result = new ContentEncodingHeader(value);
					return true;
				case "Content-Length":
					Result = new ContentLengthHeader(value);
					return true;
				case "Content-Type":
					Result = new ContentTypeHeader(value);
					return true;
				case "CSeq":
					Result = new CSeqHeader(value);
					return true;
				case "Date":
					Result = new DateHeader(value);
					return true;
				case "Encryption":
					Result = new EncryptionHeader(value);
					return true;
				case "Expires":
					Result = new ExpiresHeader(value);
					return true;
				/*case "From":
					Result = new FromHeader(value);
					return true;*/

				default:
					Result=new CustomHeader(name, value);
					return true;
			}
			/*
public static ISingleParser<MessageHeader> HideHeader = from _ in Parse.String("Hide: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new HideHeader(value);
public static ISingleParser<MessageHeader> MaxForwardsHeader = from _ in Parse.String("Max-Forwards: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new MaxForwardsHeader(value);
public static ISingleParser<MessageHeader> OrganizationHeader = from _ in Parse.String("Organization: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new OrganizationHeader(value);
public static ISingleParser<MessageHeader> PriorityHeader = from _ in Parse.String("Priority: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new PriorityHeader(value);
public static ISingleParser<MessageHeader> ProxyAuthenticateHeader = from _ in Parse.String("Proxy-Authenticate: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyAuthenticateHeader(value);
public static ISingleParser<MessageHeader> ProxyAuthorizationHeader = from _ in Parse.String("Proxy-Authorization: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyAuthorizationHeader(value);
public static ISingleParser<MessageHeader> ProxyRequireHeader = from _ in Parse.String("Proxy-Require: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyRequireHeader(value);
public static ISingleParser<MessageHeader> RecordRouteHeader = from _ in Parse.String("Record-Route: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RecordRouteHeader(value);
public static ISingleParser<MessageHeader> ReferToHeader = from _ in Parse.String("Refer-To: ", true).ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new ReferToHeader(value);
public static ISingleParser<MessageHeader> ReferredByHeader = from _ in Parse.String("Referred-By: ", true).ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new ReferredByHeader(value);
public static ISingleParser<MessageHeader> RequireHeader = from _ in Parse.String("Require: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RequireHeader(value);
public static ISingleParser<MessageHeader> ResponseKeyHeader = from _ in Parse.String("Response-Key: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ResponseKeyHeader(value);
public static ISingleParser<MessageHeader> RetryAfterHeader = from _ in Parse.String("Retry-After: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RetryAfterHeader(value);
public static ISingleParser<MessageHeader> RouteHeader = from _ in Parse.String("Route: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RouteHeader(value);
public static ISingleParser<MessageHeader> ServerHeader = from _ in Parse.String("Server: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ServerHeader(value);
public static ISingleParser<MessageHeader> SubjetHeader = from _ in Parse.String("Subject: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new SubjetHeader(value);
public static ISingleParser<MessageHeader> TimestampHeader = from _ in Parse.String("Timestamp: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new TimestampHeader(value);
public static ISingleParser<MessageHeader> ToHeader = from _ in Parse.String("To: ", true).ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new ToHeader(value);
public static ISingleParser<MessageHeader> UnsupportedHeader = from _ in Parse.String("Unsupported: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new UnsupportedHeader(value);
public static ISingleParser<MessageHeader> UserAgentHeader = from _ in Parse.String("User-Agent: ", true).ReaderIncludes(' ') from value in Parse.Except('\r').ReaderIncludes(' ').OneOrMoreTimes().ToStringParser() from eol in EOL select new UserAgentHeader(value);
public static ISingleParser<MessageHeader> ViaHeader = from _ in Parse.String("Via: ", true).ReaderIncludes(' ') from protocol in SentProtocol from sentby in URIGrammar.HostPort.ToStringParser() from parameters in ViaParams from eol in EOL select new ViaHeader(protocol + " "+ sentby, parameters.ToArray());
public static ISingleParser<MessageHeader> WarningHeader = from _ in Parse.String("Warning: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WarningHeader(value);
public static ISingleParser<MessageHeader> WWWAuthenticateHeader = from _ in Parse.String("WWW-Authenticate: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WWWAuthenticateHeader(value);
 * */

		}


	}
}
