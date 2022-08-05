using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
    // rfc2543
    // generic-message  =  start-line
    //                        * message-header
    //                       CRLF
    //                        [message - body]
    //
    // start-line       =  Request-Line |     ;Section 4.1
    //                     Status-Line        ;Section 5.1
    //
    // message-header  =  (general-header | request-header | response-header | entity-header )

    public static class SIPGrammar
    {
        public static ISingleParser<string> EOL = Parse.String("\r\n");

        public static ISingleParser<string> Method = Parse.String("INVITE").Or(Parse.String("ACK")).Or(Parse.String("OPTIONS")).Or(Parse.String("BYE")).Or(Parse.String("CANCEL")).Or(Parse.String("REGISTER"));

 
        public static ISingleParser<string> SIPVersion = Parse.String("SIP/2.0");

        public static ISingleParser<RequestLine> RequestLine = from method in Method from requestURI in URIGrammar.RequestURI 
                                                          from sipVersion in SIPVersion from eol in EOL select new RequestLine(method, requestURI, sipVersion); // Method SP Request-URI SP SIP-Version CRLF

        public static ISingleParser<RequestHeader> AcceptHeader = from _ in Parse.String("Accept") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new AcceptHeader(value);
        public static ISingleParser<RequestHeader> AcceptEncodingHeader = from _ in Parse.String("Accept-Encoding") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new AcceptEncodingHeader(value);
        public static ISingleParser<RequestHeader> AcceptLanguageHeader = from _ in Parse.String("Accept-Language") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new AcceptLanguageHeader(value);
        public static ISingleParser<RequestHeader> CallIDHeader = from _ in Parse.String("Call-ID") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new CallIDHeader(value);
        public static ISingleParser<RequestHeader> ContactHeader = from _ in Parse.String("Contact") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new ContactHeader(value);
        public static ISingleParser<RequestHeader> CSeqHeader = from _ in Parse.String("CSeq") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new CSeqHeader(value);
        public static ISingleParser<RequestHeader> DateHeader = from _ in Parse.String("Date") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new DateHeader(value);
        public static ISingleParser<RequestHeader> EncryptionHeader = from _ in Parse.String("Encryption") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new EncryptionHeader(value);
        public static ISingleParser<RequestHeader> ExpiresHeader = from _ in Parse.String("Expires") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new ExpiresHeader(value);
        public static ISingleParser<RequestHeader> FromHeader = from _ in Parse.String("From") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new FromHeader(value);
        public static ISingleParser<RequestHeader> RecordRouteHeader = from _ in Parse.String("Record-Route") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new RecordRouteHeader(value);
        public static ISingleParser<RequestHeader> TimestampHeader = from _ in Parse.String("Timestamp") from value in CommonGrammar.Alphanum.OneOrMoreTimes().ToStringParser() from eol in EOL select new TimestampHeader(value);

        public static ISingleParser<RequestHeader> GeneralHeader = AcceptHeader.Or(AcceptEncodingHeader).Or(AcceptLanguageHeader).Or(CallIDHeader).Or(ContactHeader)
            .Or(CSeqHeader).Or(DateHeader).Or(EncryptionHeader).Or(ExpiresHeader).Or(FromHeader).Or(RecordRouteHeader)
            .Or(TimestampHeader).Or(Parse.String("To")).Or(Parse.String("Via"));
        public static ISingleParser<RequestHeader> RequestHeader = Parse.String("Authorization").Or(Parse.String("Contact")).Or(Parse.String("Hide")).Or(Parse.String("Max-Forwards")).Or(Parse.String("Organization")).Or(Parse.String("Priority")).Or(Parse.String("Proxy-Authorization")).Or(Parse.String("Proxy-Require")).Or(Parse.String("Route")).Or(Parse.String("Require")).Or(Parse.String("Response-Key")).Or(Parse.String("Subject")).Or(Parse.String("User-Agent"));
        public static ISingleParser<RequestHeader> EntityHeader = Parse.String("Content-Encoding").Or(Parse.String("Content-Length")).Or(Parse.String("Content-Type"));
        
        public static ISingleParser<string> ResponseHeader = Parse.String("Allow").Or(Parse.String("Proxy-Authenticate")).Or(Parse.String("Retry-After")).Or(Parse.String("Server")).Or(Parse.String("Unsupported")).Or(Parse.String("Warning")).Or(Parse.String("WWW-Authenticate"));

        public static ISingleParser<string> MessageBody = Parse.String("Accept").Or(Parse.String("Accept-Encoding")).Or(Parse.String("Accept-Language"));
        
        public static ISingleParser<Request> Request=from requestLine in RequestLine from headers in (GeneralHeader.Or(RequestHeader).Or(EntityHeader)).ZeroOrMoreTimes()
                                                     from eol in EOL from body in MessageBody select new Request(requestLine,headers.ToArray());
        public static ISingleParser<Response> Response=from _ in Parse.Any() select new Response();

        public static ISingleParser<SIPMessage> SIPMessage = Request;//.Or<SIPMessage>(Response);
    }

}
