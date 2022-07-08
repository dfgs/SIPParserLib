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
        public static IParser<string> EOL = Parse.String("\r\n");

        public static IParser<string> Method = Parse.String("INVITE").Or(Parse.String("ACK")).Or(Parse.String("OPTIONS")).Or(Parse.String("BYE")).Or(Parse.String("CANCEL")).Or(Parse.String("REGISTER"));

 
        public static IParser<string> SIPVersion = Parse.Any();

        public static IParser<string> RequestLine = from method in Method from requestURI in URIGrammar.RequestURI from sipVersion in SIPVersion from eol in EOL select "requestLine"; // Method SP Request-URI SP SIP-Version CRLF
        public static IParser<string> GeneralHeader = Parse.String("Accept").Or(Parse.String("Accept-Encoding")).Or(Parse.String("Accept-Language")).Or(Parse.String("Call-ID")).Or(Parse.String("Contact")).Or(Parse.String("CSeq")).Or(Parse.String("Date")).Or(Parse.String("Encryption")).Or(Parse.String("Expires")).Or(Parse.String("From")).Or(Parse.String("Record-Route")).Or(Parse.String("Timestamp")).Or(Parse.String("To")).Or(Parse.String("Via"));
        public static IParser<string> RequestHeader = Parse.String("Authorization").Or(Parse.String("Contact")).Or(Parse.String("Hide")).Or(Parse.String("Max-Forwards")).Or(Parse.String("Organization")).Or(Parse.String("Priority")).Or(Parse.String("Proxy-Authorization")).Or(Parse.String("Proxy-Require")).Or(Parse.String("Route")).Or(Parse.String("Require")).Or(Parse.String("Response-Key")).Or(Parse.String("Subject")).Or(Parse.String("User-Agent"));
        public static IParser<string> EntityHeader = Parse.String("Content-Encoding").Or(Parse.String("Content-Length")).Or(Parse.String("Content-Type"));
        public static IParser<string> ResponseHeader = Parse.String("Allow").Or(Parse.String("Proxy-Authenticate")).Or(Parse.String("Retry-After")).Or(Parse.String("Server")).Or(Parse.String("Unsupported")).Or(Parse.String("Warning")).Or(Parse.String("WWW-Authenticate"));

        public static IParser<string> MessageBody = Parse.String("Accept").Or(Parse.String("Accept-Encoding")).Or(Parse.String("Accept-Language"));
        
        public static IParser<Request> Request=from requestLine in RequestLine from header in GeneralHeader.Or(RequestHeader).Or(EntityHeader) from eol in EOL from body in MessageBody select new Request();
        public static IParser<Response> Response=from _ in Parse.Any() select new Response();
        
        public static IParser<SIPMessage> SIPMessage = Request.Or<SIPMessage>(Response);
    }

}
