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

        public static ISingleParser<RequestLine> RequestLine = from method in Method from requestURI in URIGrammar.RequestURI 
                                                          from sipVersion in SIPVersion from eol in EOL select new RequestLine(method, requestURI, sipVersion); // Method SP Request-URI SP SIP-Version CRLF
        public static ISingleParser<string> HeaderValue =  Parse.Except('\r').ReaderIncludes(' ').OneOrMoreTimes().ToStringParser();
        public static ISingleParser<RequestHeader> AllowHeader = from _ in Parse.String("Allow: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AllowHeader(value);
        public static ISingleParser<RequestHeader> AcceptHeader = from _ in Parse.String("Accept: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptHeader(value);
        public static ISingleParser<RequestHeader> AcceptEncodingHeader = from _ in Parse.String("Accept-Encoding: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptEncodingHeader(value);
        public static ISingleParser<RequestHeader> AcceptLanguageHeader = from _ in Parse.String("Accept-Language: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptLanguageHeader(value);
        public static ISingleParser<RequestHeader> CallIDHeader = from _ in Parse.String("Call-ID: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CallIDHeader(value);
        public static ISingleParser<RequestHeader> ContactHeader = from _ in Parse.String("Contact: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContactHeader(value);
        public static ISingleParser<RequestHeader> CSeqHeader = from _ in Parse.String("CSeq: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CSeqHeader(value);
        public static ISingleParser<RequestHeader> DateHeader = from _ in Parse.String("Date: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new DateHeader(value);
        public static ISingleParser<RequestHeader> EncryptionHeader = from _ in Parse.String("Encryption: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new EncryptionHeader(value);
        public static ISingleParser<RequestHeader> ExpiresHeader = from _ in Parse.String("Expires: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ExpiresHeader(value);
        public static ISingleParser<RequestHeader> FromHeader = from _ in Parse.String("From: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new FromHeader(value);
        public static ISingleParser<RequestHeader> RecordRouteHeader = from _ in Parse.String("Record-Route: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RecordRouteHeader(value);
        public static ISingleParser<RequestHeader> TimestampHeader = from _ in Parse.String("Timestamp: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new TimestampHeader(value);
        public static ISingleParser<RequestHeader> ToHeader = from _ in Parse.String("To: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ToHeader(value);
        public static ISingleParser<RequestHeader> ViaHeader = from _ in Parse.String("Via: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ViaHeader(value);
        public static ISingleParser<RequestHeader> AuthorizationHeader = from _ in Parse.String("Authorization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AuthorizationHeader(value);
        public static ISingleParser<RequestHeader> HideHeader = from _ in Parse.String("Hide: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new HideHeader(value);
        public static ISingleParser<RequestHeader> MaxForwardsHeader = from _ in Parse.String("Max-Forwards: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new MaxForwardsHeader(value);
        public static ISingleParser<RequestHeader> OrganizationHeader = from _ in Parse.String("Organization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new OrganizationHeader(value);
        public static ISingleParser<RequestHeader> PriorityHeader = from _ in Parse.String("Priority: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new PriorityHeader(value);
        public static ISingleParser<RequestHeader> ProxyAuthorizationHeader = from _ in Parse.String("Proxy-Authorization: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyAuthorizationHeader(value);
        public static ISingleParser<RequestHeader> ProxyRequireHeader = from _ in Parse.String("Proxy-Require: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ProxyRequireHeader(value);
        public static ISingleParser<RequestHeader> RouteHeader = from _ in Parse.String("Route: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RouteHeader(value);
        public static ISingleParser<RequestHeader> RequireHeader = from _ in Parse.String("Require: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new RequireHeader(value);
        public static ISingleParser<RequestHeader> ResponseKeyHeader = from _ in Parse.String("Response-Key: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ResponseKeyHeader(value);
        public static ISingleParser<RequestHeader> SubjetHeader = from _ in Parse.String("Subject: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new SubjetHeader(value);
        public static ISingleParser<RequestHeader> UserAgentHeader = from _ in Parse.String("User-Agent: ").ReaderIncludes(' ')  from value in Parse.Except('\r').ReaderIncludes(' ').OneOrMoreTimes().ToStringParser() from eol in EOL select new UserAgentHeader(value);
        public static ISingleParser<RequestHeader> ContentEncodingHeader = from _ in Parse.String("Content-Encoding: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentEncodingHeader(value);
        public static ISingleParser<RequestHeader> ContentLengthHeader = from _ in Parse.String("Content-Length: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentLengthHeader(value);
        public static ISingleParser<RequestHeader> ContentTypeHeader = from _ in Parse.String("Content-Type: ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentTypeHeader(value);
        public static ISingleParser<RequestHeader> CustomHeader = from name in Parse.Except(':').OneOrMoreTimes().ToStringParser() from __ in Parse.String(": ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CustomHeader(name,value);

        public static ISingleParser<RequestHeader> GeneralHeader = AcceptHeader.Or(AcceptEncodingHeader).Or(AcceptLanguageHeader).Or(CallIDHeader).Or(ContactHeader)
            .Or(CSeqHeader).Or(DateHeader).Or(EncryptionHeader).Or(ExpiresHeader).Or(FromHeader).Or(RecordRouteHeader)
            .Or(TimestampHeader).Or(ToHeader).Or(ViaHeader);
        public static ISingleParser<RequestHeader> RequestHeader = AuthorizationHeader.Or(ContactHeader).Or(HideHeader).Or(AllowHeader)
            .Or(MaxForwardsHeader).Or(OrganizationHeader).Or(PriorityHeader).Or(ProxyAuthorizationHeader).Or(ProxyRequireHeader)
            .Or(RouteHeader).Or(RequireHeader).Or(ResponseKeyHeader)
            .Or(SubjetHeader).Or(UserAgentHeader);
        public static ISingleParser<RequestHeader> EntityHeader = ContentEncodingHeader.Or(ContentLengthHeader).Or(ContentTypeHeader);
        
        public static ISingleParser<string> ResponseHeader = Parse.String("Allow").Or(Parse.String("Proxy-Authenticate")).Or(Parse.String("Retry-After")).Or(Parse.String("Server")).Or(Parse.String("Unsupported")).Or(Parse.String("Warning")).Or(Parse.String("WWW-Authenticate"));

        public static IMultipleParser<RequestHeader> RequestHeaders = (GeneralHeader.Or(RequestHeader).Or(EntityHeader).Or(CustomHeader)).ZeroOrMoreTimes();

        public static ISingleParser<string> MessageBody = Parse.Any().ReaderIncludes(' ').ZeroOrMoreTimes().ToStringParser();



        public static ISingleParser<Request> Request = from requestLine in RequestLine from headers in RequestHeaders
                                                       from eol in EOL from body in MessageBody select new Request(requestLine, headers.ToArray(), body);
        public static ISingleParser<Response> Response=from _ in Parse.Any() select new Response();

        public static ISingleParser<SIPMessage> SIPMessage = Request;//.Or<SIPMessage>(Response);
    }

}
