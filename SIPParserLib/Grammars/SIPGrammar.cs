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

        public static ISingleParser<string> Method = Parse.String("INVITE").Or(Parse.String("ACK")).Or(Parse.String("PRACK")).Or(Parse.String("OPTIONS")).Or(Parse.String("BYE")).Or(Parse.String("CANCEL")).Or(Parse.String("REGISTER")).Or(Parse.String("UPDATE")).Or(Parse.String("NOTIFY")).Or(Parse.String("REFER")).Or(Parse.String("MESSAGE")).Or(Parse.String("SUBSCRIBE"));
 
        public static ISingleParser<string> SIPVersion = Parse.String("SIP/2.0");

        public static ISingleParser<StatusLine> StatusLine = from _ in SIPVersion from  code in Parse.Digit().Then(Parse.Digit()).Then(Parse.Digit()).ToStringParser()
                                                             from reason in Parse.Except('\r').OneOrMoreTimes().ToStringParser() from eol in EOL
                                                             select new StatusLine(ushort.Parse(code),reason);
        public static ISingleParser<RequestLine> RequestLine = 
                                                    from method in Method 
                                                    from requestURI in URIGrammar.RequestURI 
                                                    from sipVersion in SIPVersion from eol in EOL 
                                                    select new RequestLine(method, requestURI, sipVersion); // Method SP Request-URI SP SIP-Version CRLF
 
		public static ISingleParser<string> ProtocolName = Parse.String("SIP").Or(CommonGrammar.Token);
		public static ISingleParser<string> ProtocolVersion = CommonGrammar.Token;
		public static ISingleParser<string> Transport = Parse.String("UDP").Or(Parse.String("TCP")).Or(CommonGrammar.Token);

		public static ISingleParser<string> SentProtocol = ProtocolName.Then(Parse.Char('/').ToStringParser()).Then(ProtocolVersion).Then(Parse.Char('/').ToStringParser()).Then(Transport).ToStringParser();
		public static ISingleParser<string> SentBy = URIGrammar.HostPort.ToStringParser().Or(CommonGrammar.Token);

		public static ISingleParser<ViaParameter> ViaBranch = from _ in Parse.String("branch=") from value in CommonGrammar.Token select new ViaBranch(value);
		public static ISingleParser<ViaParameter> ViaReceived = from _ in Parse.String("received=") from value in URIGrammar.HostPort select new ViaReceived(value);
		public static ISingleParser<ViaParameter> ViaMAddr = from _ in Parse.String("maddr=") from value in CommonGrammar.Token select new ViaMAddr(value);
		public static ISingleParser<ViaParameter> ViaTTL = from _ in Parse.String("ttl=") from value in Parse.Byte() select new ViaTTL(value);
		public static ISingleParser<ViaParameter> ViaHidden = from _ in Parse.String("hidden") select new ViaHidden();
		public static ISingleParser<ViaParameter> ViaCustomParameter = from name in CommonGrammar.Token from _ in Parse.Char('=') from value in CommonGrammar.Token select new ViaCustomParameter(name,value);

		public static IMultipleParser<ViaParameter> ViaParams = Parse.ZeroOrMoreTimes( from _ in Parse.Char(';') from value in ViaBranch.Or(ViaReceived).Or(ViaMAddr).Or(ViaTTL).Or(ViaHidden).Or(ViaCustomParameter) select value );

		public static ISingleParser<string> HeaderValue =  Parse.Except('\r').ReaderIncludes(' ').ZeroOrMoreTimes().ToStringParser();

        public static ISingleParser<MessageHeader> AcceptHeader = from _ in Parse.String("Accept: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptHeader(value);
        public static ISingleParser<MessageHeader> AcceptEncodingHeader = from _ in Parse.String("Accept-Encoding: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptEncodingHeader(value);
        public static ISingleParser<MessageHeader> AllowHeader = from _ in Parse.String("Allow: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AllowHeader(value);
        public static ISingleParser<MessageHeader> AcceptLanguageHeader = from _ in Parse.String("Accept-Language: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AcceptLanguageHeader(value);
        public static ISingleParser<MessageHeader> AuthorizationHeader = from _ in Parse.String("Authorization: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new AuthorizationHeader(value);
        public static ISingleParser<MessageHeader> CallIDHeader = from _ in Parse.String("Call-ID: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CallIDHeader(value);
        public static ISingleParser<MessageHeader> ContactHeader = from _ in Parse.String("Contact: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContactHeader(value);
        public static ISingleParser<MessageHeader> ContentEncodingHeader = from _ in Parse.String("Content-Encoding: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentEncodingHeader(value);
        public static ISingleParser<MessageHeader> ContentLengthHeader = from _ in Parse.String("Content-Length: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentLengthHeader(value);
        public static ISingleParser<MessageHeader> ContentTypeHeader = from _ in Parse.String("Content-Type: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ContentTypeHeader(value);
        public static ISingleParser<MessageHeader> CSeqHeader = from _ in Parse.String("CSeq: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CSeqHeader(value);
        public static ISingleParser<MessageHeader> DateHeader = from _ in Parse.String("Date: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new DateHeader(value);
        public static ISingleParser<MessageHeader> EncryptionHeader = from _ in Parse.String("Encryption: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new EncryptionHeader(value);
        public static ISingleParser<MessageHeader> ExpiresHeader = from _ in Parse.String("Expires: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new ExpiresHeader(value);
        public static ISingleParser<MessageHeader> FromHeader = from _ in Parse.String("From: ",true).ReaderIncludes(' ') from value in URIGrammar.Address from eol in EOL select new FromHeader(value);
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
        public static ISingleParser<MessageHeader> ViaHeader = from _ in Parse.String("Via: ", true).ReaderIncludes(' ') from protocol in SentProtocol from sentby in SentBy from parameters in ViaParams from eol in EOL select new ViaHeader(protocol + " "+ sentby, parameters.ToArray());
        public static ISingleParser<MessageHeader> WarningHeader = from _ in Parse.String("Warning: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WarningHeader(value);
        public static ISingleParser<MessageHeader> WWWAuthenticateHeader = from _ in Parse.String("WWW-Authenticate: ", true).ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new WWWAuthenticateHeader(value);

		public static ISingleParser<MessageHeader> CustomHeader = from name in CommonGrammar.Token from __ in Parse.String(": ").ReaderIncludes(' ') from value in HeaderValue from eol in EOL select new CustomHeader(name, value);
		public static ISingleParser<MessageHeader> CustomHeaderWithoutValue = from name in CommonGrammar.Token from __ in Parse.String(":") from eol in EOL select new CustomHeader(name, "");

		public static IMultipleParser<MessageHeader> MessageHeaders = (AcceptHeader.Or(AcceptEncodingHeader).Or(AllowHeader).Or(AcceptLanguageHeader).Or(AuthorizationHeader).Or(CallIDHeader).Or(ContactHeader).Or(ContentEncodingHeader)
            .Or(ContentLengthHeader).Or(ContentTypeHeader).Or(CSeqHeader).Or(DateHeader).Or(EncryptionHeader).Or(ExpiresHeader).Or(FromHeader).Or(HideHeader).Or(MaxForwardsHeader)
            .Or(OrganizationHeader).Or(PriorityHeader).Or(ProxyAuthenticateHeader).Or(ProxyAuthorizationHeader).Or(ProxyRequireHeader).Or(RecordRouteHeader).Or(ReferToHeader).Or(ReferredByHeader)
            .Or(RequireHeader).Or(ResponseKeyHeader).Or(RetryAfterHeader).Or(RouteHeader).Or(ServerHeader).Or(SubjetHeader).Or(TimestampHeader).Or(ToHeader).Or(UnsupportedHeader).Or(UserAgentHeader)
            .Or(ViaHeader).Or(WarningHeader).Or(WWWAuthenticateHeader).Or(CustomHeader).Or(CustomHeaderWithoutValue)).ZeroOrMoreTimes();

        public static ISingleParser<string> MessageBody = Parse.Any().ReaderIncludes(' ').ZeroOrMoreTimes().ToStringParser();



        public static ISingleParser<Request> Request = from requestLine in RequestLine from headers in MessageHeaders
                                                       from eol in EOL from body in MessageBody select new Request(requestLine, headers.ToArray(), body);

        public static ISingleParser<Response> Response=from statusLine in StatusLine from headers in MessageHeaders 
                                                       from eol in EOL from body in MessageBody select new Response(statusLine,headers.ToArray(),body);

        public static ISingleParser<SIPMessage> SIPMessage = Request.Or<SIPMessage>(Response);
    }

}
