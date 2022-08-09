using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
    
    public static class SIPGrammar
    {
        public static ISingleParser<string> EOL = Parse.String("\r\n");

        public static ISingleParser<string> Method = Parse.String("INVITE").Or(Parse.String("ACK")).Or(Parse.String("OPTIONS")).Or(Parse.String("BYE")).Or(Parse.String("CANCEL")).Or(Parse.String("REGISTER"));
 
        public static ISingleParser<string> SIPVersion = Parse.String("SIP/2.0");

        public static ISingleParser<StatusLine> StatusLine = from _ in SIPVersion from  code in Parse.Digit().Then(Parse.Digit()).Then(Parse.Digit()).ToStringParser()
                                                             from reason in Parse.Except('\r').OneOrMoreTimes().ToStringParser() from eol in EOL
                                                             select new StatusLine(code,reason);
        public static ISingleParser<RequestLine> RequestLine = from method in Method from requestURI in URIGrammar.RequestURI 
                                                          from sipVersion in SIPVersion from eol in EOL select new RequestLine(method, requestURI, sipVersion); // Method SP Request-URI SP SIP-Version CRLF
       
        public static ISingleParser<string> HeaderValue =  Parse.Except('\r').ReaderIncludes(' ').ZeroOrMoreTimes().ToStringParser();

        public static ISingleParser<MessageHeader> AcceptHeader = from _ in Parse.String("Accept: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptHeader(value);
        public static ISingleParser<MessageHeader> AcceptEncodingHeader = from _ in Parse.String("Accept-Encoding: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptEncodingHeader(value);
        public static ISingleParser<MessageHeader> AllowHeader = from _ in Parse.String("Allow: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AllowHeader(value);
        public static ISingleParser<MessageHeader> AcceptLanguageHeader = from _ in Parse.String("Accept-Language: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptLanguageHeader(value);
        public static ISingleParser<MessageHeader> AuthorizationHeader = from _ in Parse.String("Authorization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AuthorizationHeader(value);
        public static ISingleParser<MessageHeader> CallIDHeader = from _ in Parse.String("Call-ID: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CallIDHeader(value);
        public static ISingleParser<MessageHeader> ContactHeader = from _ in Parse.String("Contact: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContactHeader(value);
        public static ISingleParser<MessageHeader> ContentEncodingHeader = from _ in Parse.String("Content-Encoding: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentEncodingHeader(value);
        public static ISingleParser<MessageHeader> ContentLengthHeader = from _ in Parse.String("Content-Length: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentLengthHeader(value);
        public static ISingleParser<MessageHeader> ContentTypeHeader = from _ in Parse.String("Content-Type: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentTypeHeader(value);
        public static ISingleParser<MessageHeader> CSeqHeader = from _ in Parse.String("CSeq: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CSeqHeader(value);
        public static ISingleParser<MessageHeader> DateHeader = from _ in Parse.String("Date: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new DateHeader(value);
        public static ISingleParser<MessageHeader> EncryptionHeader = from _ in Parse.String("Encryption: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new EncryptionHeader(value);
        public static ISingleParser<MessageHeader> ExpiresHeader = from _ in Parse.String("Expires: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ExpiresHeader(value);
        public static ISingleParser<MessageHeader> FromHeader = from _ in Parse.String("From: ").ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new FromHeader(value);
        public static ISingleParser<MessageHeader> HideHeader = from _ in Parse.String("Hide: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new HideHeader(value);
        public static ISingleParser<MessageHeader> MaxForwardsHeader = from _ in Parse.String("Max-Forwards: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new MaxForwardsHeader(value);
        public static ISingleParser<MessageHeader> OrganizationHeader = from _ in Parse.String("Organization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new OrganizationHeader(value);
        public static ISingleParser<MessageHeader> PriorityHeader = from _ in Parse.String("Priority: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new PriorityHeader(value);
        public static ISingleParser<MessageHeader> ProxyAuthenticateHeader = from _ in Parse.String("Proxy-Authenticate: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyAuthenticateHeader(value);
        public static ISingleParser<MessageHeader> ProxyAuthorizationHeader = from _ in Parse.String("Proxy-Authorization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyAuthorizationHeader(value);
        public static ISingleParser<MessageHeader> ProxyRequireHeader = from _ in Parse.String("Proxy-Require: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyRequireHeader(value);
        public static ISingleParser<MessageHeader> RecordRouteHeader = from _ in Parse.String("Record-Route: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RecordRouteHeader(value);
        public static ISingleParser<MessageHeader> RequireHeader = from _ in Parse.String("Require: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RequireHeader(value);
        public static ISingleParser<MessageHeader> ResponseKeyHeader = from _ in Parse.String("Response-Key: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ResponseKeyHeader(value);
        public static ISingleParser<MessageHeader> RetryAfterHeader = from _ in Parse.String("Retry-After: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RetryAfterHeader(value);
        public static ISingleParser<MessageHeader> RouteHeader = from _ in Parse.String("Route: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RouteHeader(value);
        public static ISingleParser<MessageHeader> ServerHeader = from _ in Parse.String("Server: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ServerHeader(value);
        public static ISingleParser<MessageHeader> SubjetHeader = from _ in Parse.String("Subject: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new SubjetHeader(value);
        public static ISingleParser<MessageHeader> TimestampHeader = from _ in Parse.String("Timestamp: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new TimestampHeader(value);
        public static ISingleParser<MessageHeader> ToHeader = from _ in Parse.String("To: ").ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new ToHeader(value);
        public static ISingleParser<MessageHeader> UnsupportedHeader = from _ in Parse.String("Unsupported: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new UnsupportedHeader(value);
        public static ISingleParser<MessageHeader> UserAgentHeader = from _ in Parse.String("User-Agent: ").ReaderIncludes(' ') from value in Parse.Except('\r').ReaderIncludes(' ').OneOrMoreTimes().ToStringParser() from eol in EOL select new UserAgentHeader(value);
        public static ISingleParser<MessageHeader> ViaHeader = from _ in Parse.String("Via: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ViaHeader(value);
        public static ISingleParser<MessageHeader> WarningHeader = from _ in Parse.String("Warning: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WarningHeader(value);
        public static ISingleParser<MessageHeader> WWWAuthenticateHeader = from _ in Parse.String("WWW-Authenticate: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WWWAuthenticateHeader(value);

        public static ISingleParser<MessageHeader> CustomHeader = from name in CommonGrammar.Token from __ in Parse.String(": ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CustomHeader(name,value);

        public static IMultipleParser<MessageHeader> MessageHeaders = (AcceptHeader.Or(AcceptEncodingHeader).Or(AllowHeader).Or(AcceptLanguageHeader).Or(AuthorizationHeader).Or(CallIDHeader).Or(ContactHeader).Or(ContentEncodingHeader).Or(ContentLengthHeader).Or(ContentTypeHeader).Or(CSeqHeader).Or(DateHeader).Or(EncryptionHeader).Or(ExpiresHeader).Or(FromHeader).Or(HideHeader).Or(MaxForwardsHeader).Or(OrganizationHeader).Or(PriorityHeader).Or(ProxyAuthenticateHeader).Or(ProxyAuthorizationHeader).Or(ProxyRequireHeader).Or(RecordRouteHeader).Or(RequireHeader).Or(ResponseKeyHeader).Or(RetryAfterHeader).Or(RouteHeader).Or(ServerHeader).Or(SubjetHeader).Or(TimestampHeader).Or(ToHeader).Or(UnsupportedHeader).Or(UserAgentHeader).Or(ViaHeader).Or(WarningHeader).Or(WWWAuthenticateHeader).Or(CustomHeader)).ZeroOrMoreTimes();

        public static ISingleParser<string> MessageBody = Parse.Any().ReaderIncludes(' ').ZeroOrMoreTimes().ToStringParser();



        public static ISingleParser<Request> Request = from requestLine in RequestLine from headers in MessageHeaders
                                                       from eol in EOL from body in MessageBody select new Request(requestLine, headers.ToArray(), body);

        public static ISingleParser<Response> Response=from statusLine in StatusLine from headers in MessageHeaders 
                                                       from eol in EOL from body in MessageBody select new Response(statusLine,headers.ToArray(),body);

        public static ISingleParser<SIPMessage> SIPMessage = Request.Or<SIPMessage>(Response);
    }

}
