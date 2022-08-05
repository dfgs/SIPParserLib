using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib.UnitTest
{
	internal static class Consts
	{
        public static string RequestLine1 = "INVITE sip:+33140143960@ecb.core.nord:5060;user=phone SIP/2.0\r\n";
        public static string Invite1 = "INVITE sip:+33140143960@ecb.core.nord:5060;user=phone SIP/2.0\r\nVia: SIP/2.0/UDP 22.19.16.38:5060;branch=z9hG4bKgf8qu4309gm3f6ljhkc0.1\r\nAllow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\nSupported: path,100rel\r\nUser-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\nP-Asserted-Identity: \"ITGP TEL05 T NT1\" <sip:+33157437620@22.19.16.38;user=phone>\r\nContent-Type: application/sdp\r\nTo: <sip:+33140143960@22.19.1.52;user=phone>\r\nFrom: \"ITGP TEL05 T NT1\" <sip:+33157437620@alcatel;user=phone>;tag=SD1duf601-c2810c7ef9af3af338277ef2e8bad0e9\r\nContact: <sip:+33157437620@22.19.16.38:5060;transport=udp>\r\nCall-ID: SD1duf601-56e1a9f579471d1ae274d6db70de1aa7-mo420q0\r\nCSeq: 1568068945 INVITE\r\nMax-Forwards: 69\r\nContent-Length: 288\r\n			\r\nv = 0\r\no =OXE 1657028298 1657028298 IN IP4 22.19.16.38\r\ns = abs\r\nc = IN IP4 22.19.16.38\r\nt = 0 0\r\nm =audio 20664 RTP/AVP 8 18 101\r\na = sendrecv\r\na = rtpmap:8 PCMA/8000\r\na = ptime:20\r\na = maxptime:30\r\na =rtpmap:18 G729/8000\r\na =fmtp:18 annexb=no\r\na = ptime:20\r\na = maxptime:40\r\na =rtpmap:101 telephone-event/8000";
        
        public static string User1 = "j.doe";
        public static string User2 = "j.doe:secret";
        public static string User3 = "+1-212-555-1212:1234";
        public static string User4 = "1212";

        public static string HostPort1 = "big.com";
        public static string HostPort2 = "big.com:1234";
        public static string HostPort3 = "10.1.2.3";
        public static string HostPort4 = "10.1.2.3:1234";


        public static string URI1 = "sip:j.doe@big.com";
        public static string URI2 = "sip:j.doe:secret@big.com;transport=tcp";
        public static string URI3 = "sip:j.doe@big.com?subject=project";
        public static string URI4 = "sip:+1-212-555-1212:1234@gateway.com;user=phone";
        public static string URI5 = "sip:1212@gateway.com";
        public static string URI6 = "sip:alice@10.1.2.3";
        public static string URI7 = "sip:alice@example.com";
        public static string URI8 = "sip:alice%40example.com@gateway.com";
        public static string URI9 = "sip:alice@registrar.com;method=REGISTER";
        public static string URI10 = "sip:big.com";
        public static string URI11 = "sip:j.doe@big.com?subject=project&name=test";

    }

}