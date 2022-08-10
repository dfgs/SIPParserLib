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
        public static string HeaderLine1 = "Via: SIP/2.0/UDP 22.19.16.38:5060;branch=z9hG4bKgf8qu4309gm3f6ljhkc0.1\r\nAllow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\nSupported: path,100rel\r\nUser-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\nP-Asserted-Identity: \"ITGP TEL05 T NT1\" <sip:+33157437620@22.19.16.38;user=phone>\r\nContent-Type: application/sdp\r\nTo: <sip:+33140143960@22.19.1.52;user=phone>\r\nFrom: \"ITGP TEL05 T NT1\" <sip:+33157437620@alcatel;user=phone>;tag=SD1duf601-c2810c7ef9af3af338277ef2e8bad0e9\r\nContact: <sip:+33157437620@22.19.16.38:5060;transport=udp>\r\nCall-ID: SD1duf601-56e1a9f579471d1ae274d6db70de1aa7-mo420q0\r\nCSeq: 1568068945 INVITE\r\nMax-Forwards: 69\r\nContent-Length: 288\r\n";
        public static string Invite1 = "INVITE sip:+33140143960@ecb.core.nord:5060;user=phone SIP/2.0\r\nVia: SIP/2.0/UDP 22.19.16.38:5060;branch=z9hG4bKgf8qu4309gm3f6ljhkc0.1\r\nAllow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\nSupported: path,100rel\r\nUser-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\nP-Asserted-Identity: \"ITGP TEL05 T NT1\" <sip:+33157437620@22.19.16.38;user=phone>\r\nContent-Type: application/sdp\r\nTo: <sip:+33140143960@22.19.1.52;user=phone>\r\nFrom: \"ITGP TEL05 T NT1\" <sip:+33157437620@alcatel;user=phone>;tag=SD1duf601-c2810c7ef9af3af338277ef2e8bad0e9\r\nContact: <sip:+33157437620@22.19.16.38:5060;transport=udp>\r\nCall-ID: SD1duf601-56e1a9f579471d1ae274d6db70de1aa7-mo420q0\r\nCSeq: 1568068945 INVITE\r\nMax-Forwards: 69\r\nContent-Length: 288\r\n    \r\nv = 0\r\no =OXE 1657028298 1657028298 IN IP4 22.19.16.38\r\ns = abs\r\nc = IN IP4 22.19.16.38\r\nt = 0 0\r\nm =audio 20664 RTP/AVP 8 18 101\r\na = sendrecv\r\na = rtpmap:8 PCMA/8000\r\na = ptime:20\r\na = maxptime:30\r\na =rtpmap:18 G729/8000\r\na =fmtp:18 annexb=no\r\na = ptime:20\r\na = maxptime:40\r\na =rtpmap:101 telephone-event/8000";

        public static string RequestLine2 = "INVITE sip:0243444265@100.127.1.1;transport=UDP;user=phone SIP/2.0\r\n";
        public static string HeaderLine2 = "Allow: INVITE, ACK, CANCEL, BYE, OPTIONS, NOTIFY, PRACK, UPDATE\r\nSupported: 100rel,from-change,timer,histinfo\r\nUser-Agent: OXO031/034.001 GW_031/036.001\r\nSession-Expires: 43080\r\nP-Asserted-Identity: <sip:0243444256@10.63.51.246;user=phone>\r\nHistory-Info: <sip:0243444265@100.127.1.1;transport=UDP;user=phone>;index=1\r\nTo: <sip:0243444265@100.127.1.1;user=phone>\r\nFrom: <sip:0243444256@10.63.51.246;user=phone>;tag=805c39dd2baeee3bffc8428aefefcf0b\r\nContact: <sip:0243444256@10.63.51.246;transport=UDP;user=phone>\r\nContent-Type: application/sdp\r\nCall-ID: da1a52791418607f7df917df392a0b02@10.63.51.246\r\nCSeq: 1052050797 INVITE\r\nVia: SIP/2.0/UDP 10.63.51.246;rport;branch=z9hG4bK51a961c0007d0655a9525af08038540a\r\nMax-Forwards: 70\r\nContent-Length: 287\r\n";
        public static string Invite2 = "INVITE sip:0243444265@100.127.1.1;transport=UDP;user=phone SIP/2.0\r\nAllow: INVITE, ACK, CANCEL, BYE, OPTIONS, NOTIFY, PRACK, UPDATE\r\nSupported: 100rel,from-change,timer,histinfo\r\nUser-Agent: OXO031/034.001 GW_031/036.001\r\nSession-Expires: 43080\r\nP-Asserted-Identity: <sip:0243444256@10.63.51.246;user=phone>\r\nHistory-Info: <sip:0243444265@100.127.1.1;transport=UDP;user=phone>;index=1\r\nTo: <sip:0243444265@100.127.1.1;user=phone>\r\nFrom: <sip:0243444256@10.63.51.246;user=phone>;tag=805c39dd2baeee3bffc8428aefefcf0b\r\nContact: <sip:0243444256@10.63.51.246;transport=UDP;user=phone>\r\nContent-Type: application/sdp\r\nCall-ID: da1a52791418607f7df917df392a0b02@10.63.51.246\r\nCSeq: 1052050797 INVITE\r\nVia: SIP/2.0/UDP 10.63.51.246;rport;branch=z9hG4bK51a961c0007d0655a9525af08038540a\r\nMax-Forwards: 70\r\nContent-Length: 287\r\n\r\nv=0\r\no=OxO 1657197043 1657197043 IN IP4 10.63.51.246\r\ns=Alcatel-Lucent OXO031/036.001\r\nc=IN IP4 10.63.51.49\r\nt=0 0\r\nm=audio 32000 RTP/AVP 8 101\r\na=rtcp:32001\r\na=sendrecv\r\na=rtpmap:8 PCMA/8000\r\na=rtpmap:101 telephone-event/8000\r\na=fmtp:101 0-15\r\na=maxptime:60\r\na=silenceSupp:off - - - -\r\n\r\n";

        public static string RequestLine3 = "ACK sip:+33663326291@185.221.88.177:5060;user=phone;sdp_iwf;transport=udp SIP/2.0\r\n";
        public static string ACK1 = "ACK sip:+33663326291@185.221.88.177:5060;user=phone;sdp_iwf;transport=udp SIP/2.0\r\nVia: SIP/2.0/UDP 185.115.197.34:5060;branch=z9hG4bKfk8urf301gp3l4vhh060.1\r\nFrom: <sip:+33555320042@185.115.197.34>;tag=SDfefdf99-1163359643-1660038006385\r\nTo: <sip:+33663326291@185.221.88.177;user=phone>;tag=SDfefdf03-007302670000fdcf\r\nCall-ID: SDfefdf03-c44de57873bf3416ce187480189d9631-v300g00060\r\nCSeq: 16573240 ACK\r\nContact: <sip:185.115.197.34:5060;transport=udp>\r\nSession-ID: 23711d2948484865b7f1c3008742bb56;remote=00000000000000000000000000000000\r\nMax-Forwards: 69\r\nContent-Type: application/sdp\r\nContent-Length: 165\r\n\r\nv=0\r\no=BroadWorks 29051720 2 IN IP4 185.115.197.34\r\ns=-\r\nc=IN IP4 185.115.197.34\r\nt=0 0\r\nm=audio 26926 RTP/AVP 8 101\r\na=rtpmap:101 telephone-event/8000\r\na=ptime:20\r\n\r\n";


        public static string Ringing1 = "SIP/2.0 180 Ringing\r\nTo: <sip:0243444265@10.63.51.246;user=phone>;tag=SDlkqmf99-32404998-1657196912165\r\nFrom: <sip:0243444256@100.127.1.1;user=phone>;tag=805c39dd2baeee3bffc8428aefefcf0b\r\nCall-ID: da1a52791418607f7df917df392a0b02@10.63.51.246\r\nCSeq: 1052050797 INVITE\r\nVia: SIP/2.0/UDP 10.63.51.246;received=10.63.51.246;branch=z9hG4bK51a961c0007d0655a9525af08038540a;rport=5060\r\nAllow: INVITE,ACK,CANCEL,BYE,OPTIONS,UPDATE\r\nSupported: \r\nContact: <sip:100.127.1.1:5060;sdp_iwf;transport=udp>\r\nP-Asserted-Identity: <sip:0687756748@100.127.1.1;user=phone>\r\nPrivacy: none\r\nP-Early-Media: sendonly\r\nContent-Length: 0\r\n\r\n";
        public static string StatusLine3 = "SIP/2.0 180 Ringing\r\n";
        public static string HeaderLine3 = "To: <sip:0243444265@10.63.51.246;user=phone>;tag=SDlkqmf99-32404998-1657196912165\r\nFrom: <sip:0243444256@100.127.1.1;user=phone>;tag=805c39dd2baeee3bffc8428aefefcf0b\r\nCall-ID: da1a52791418607f7df917df392a0b02@10.63.51.246\r\nCSeq: 1052050797 INVITE\r\nVia: SIP/2.0/UDP 10.63.51.246;received=10.63.51.246;branch=z9hG4bK51a961c0007d0655a9525af08038540a;rport=5060\r\nAllow: INVITE,ACK,CANCEL,BYE,OPTIONS,UPDATE\r\nSupported: \r\nContact: <sip:100.127.1.1:5060;sdp_iwf;transport=udp>\r\nP-Asserted-Identity: <sip:0687756748@100.127.1.1;user=phone>\r\nPrivacy: none\r\nP-Early-Media: sendonly\r\nContent-Length: 0\r\n";

        public static string OK1 = "SIP/2.0 200 OK\r\nTo: <sip:0243444265@10.63.51.246;user=phone>;tag=SDlkqmf99-32404998-1657196912165\r\nFrom: <sip:0243444256@100.127.1.1;user=phone>;tag=805c39dd2baeee3bffc8428aefefcf0b\r\nCall-ID: da1a52791418607f7df917df392a0b02@10.63.51.246\r\nCSeq: 1052050797 INVITE\r\nVia: SIP/2.0/UDP 10.63.51.246;received=10.63.51.246;branch=z9hG4bK51a961c0007d0655a9525af08038540a;rport=5060\r\nSupported: \r\nContact: <sip:100.127.1.1:5060;sdp_iwf;transport=udp>\r\nP-Asserted-Identity: <sip:0687756748@100.127.1.1;user=phone>\r\nPrivacy: none\r\nAllow: INVITE,ACK,CANCEL,BYE,OPTIONS,UPDATE\r\nAccept: application/media_control+xml,application/sdp,application/x-broadworks-call-center+xml\r\nContent-Type: application/sdp\r\nContent-Length: 256\r\n\r\nv=0\r\no=- 1897409663 1 IN IP4 100.127.1.4\r\ns=-\r\nc=IN IP4 100.127.1.4\r\nt=0 0\r\na=sendrecv\r\nm=audio 20640 RTP/AVP 8 101\r\nc=IN IP4 100.127.1.4\r\nb=RR:0\r\nb=RS:0\r\na=rtpmap:8 PCMA/8000\r\na=rtpmap:101 telephone-event/8000\r\na=fmtp:101 0-15\r\na=maxptime:40\r\na=ptime:20\r\n";
 
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
        public static string URI12 = "sip:0243444265@100.127.1.1;transport=UDP;user=phone";
        public static string URI13 = "tel:+334556677";

        public static string Address1 = "\"A. G. Bell\" <sip:agb@bell-telephone.com>";
        public static string Address2 = "sip:+12125551212@server.phone2net.com";
        public static string Address3 = "Anonymous <sip:c8oqz84zk7z@privacy.org>";
        public static string Address4 = "<sip:+33663326291@185.221.88.177;user=phone>;tag=SDfefdf03-007302670000fdcf";


    }

}