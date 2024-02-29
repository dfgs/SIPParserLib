using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SipGrammarUnitTest
	{
		

		[TestMethod]
		public void ShouldParseHeaderValue()
		{
			string result;

			result = SIPGrammar.HeaderValue.Parse("OmniPCX Enterprise R12.1 m2.300.23\r\n", ' ');
			Assert.AreEqual("OmniPCX Enterprise R12.1 m2.300.23", result);
		}
		[TestMethod]
		public void ShouldParseHeader()
		{
			MessageHeader<string> result;

			result = (MessageHeader<string>)SIPGrammar.UserAgentHeader.Parse("User-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\n", ' ');
			Assert.AreEqual("User-Agent", result.Name);
			Assert.AreEqual("OmniPCX Enterprise R12.1 m2.300.23", result.Value);

			result = (MessageHeader<string>)SIPGrammar.AllowHeader.Parse("Allow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\n", ' ');
			Assert.AreEqual("Allow", result.Name);
			Assert.AreEqual("INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO", result.Value);

			result = (MessageHeader<string>)SIPGrammar.CustomHeader.Parse("Session-ID: 23711d2948484865b7f1c3008742bb56;remote=00000000000000000000000000000000\r\n", ' ');
			Assert.AreEqual("Session-ID", result.Name);
			Assert.AreEqual("23711d2948484865b7f1c3008742bb56;remote=00000000000000000000000000000000", result.Value);
		}
		[TestMethod]
		public void ShouldParseFromHeader()
		{
			FromHeader result;
			
			result = (FromHeader)SIPGrammar.FromHeader.Parse("From: <sip:+33663326291@185.221.88.177;user=phone>;tag=SDfefdf03-007302670000fdcf\r\n", ' ');
			Assert.AreEqual("From", result.Name);
			Assert.AreEqual("SDfefdf03-007302670000fdcf", ((SIPURL)result.Value.URI).Parameters[1].Value );

			result = (FromHeader)SIPGrammar.FromHeader.Parse("FROM: <sip:+33663326291@185.221.88.177;user=phone>;tag=SDfefdf03-007302670000fdcf\r\n", ' ');
			Assert.AreEqual("From", result.Name);
			Assert.AreEqual("SDfefdf03-007302670000fdcf", ((SIPURL)result.Value.URI).Parameters[1].Value);

			result = (FromHeader)SIPGrammar.FromHeader.Parse("From: <sip:+33420100881@100.123.12.10;user=phone>;tag=1c2117732372\r\n", ' ');
			Assert.AreEqual("From", result.Name);
			Assert.AreEqual("1c2117732372", ((SIPURL)result.Value.URI).Parameters[1].Value);

			result = (FromHeader)SIPGrammar.FromHeader.Parse("From: sip:+33786953886@10.91.254.206;tag=1B98AEB8-E185-4A22-9B3C-1D52B095739A-377738\r\n", ' ');
			Assert.AreEqual("From", result.Name);
			Assert.AreEqual("1B98AEB8-E185-4A22-9B3C-1D52B095739A-377738", ((SIPURL)result.Value.URI).Parameters[0].Value);



		}
		[TestMethod]
		public void ShouldParseToHeader()
		{
			ToHeader result;

			result = (ToHeader)SIPGrammar.ToHeader.Parse("To: <sip:+33663326291@185.221.88.177;user=phone>;tag=SDfefdf03-007302670000fdcf\r\n", ' ');
			Assert.AreEqual("To", result.Name);
			Assert.AreEqual("SDfefdf03-007302670000fdcf", ((SIPURL)result.Value.URI).Parameters[1].Value);

		

		}

		[TestMethod]
		public void ShouldParseReferToHeader()
		{
			MessageHeader<Address> result;

			result = (MessageHeader<Address>)SIPGrammar.ReferToHeader.Parse("Refer-To: <sip:10.91.254.190:5060;transport=udp?Replaces=607cc119-f498-4e97-84ae-27d2223a8dd3@localhost;to-tag=SDdfsad99-72394F48-7E22-41AF-B84F-14EB0A6130F8-1939908;from-tag=2929199961609947131>\r\n", ' ');
			Assert.AreEqual("Refer-To", result.Name);
			Assert.IsNotNull(result.Value);
		}
		[TestMethod]
		public void ShouldParseReferredByHeader()
		{
			MessageHeader<Address> result;

			result = (MessageHeader<Address>)SIPGrammar.ReferredByHeader.Parse("Referred-By: <sip:+33251886806@10.105.32.141:5060>;user=phone;transport=udp\r\n", ' ');
			Assert.AreEqual("Referred-By", result.Name);
			Assert.IsNotNull(result.Value);
		}
		[TestMethod]
		public void ShouldParseViaHeader()
		{
			ViaHeader result;

			result = (ViaHeader)SIPGrammar.ViaHeader.Parse("Via: SIP/2.0/TCP 172.17.20.13:5060;alias;branch=z9hG4bKac1617994934\r\n\r\n", ' ');
			Assert.AreEqual("Via", result.Name);
			Assert.AreEqual("SIP/2.0/TCP 172.17.20.13:5060", result.Value);
			Assert.AreEqual("z9hG4bKac1617994934", result.GetParameter<ViaBranch>()?.Value);

			result = (ViaHeader)SIPGrammar.ViaHeader.Parse("Via: SIP/2.0/TCP sv049vm.aeu.local:5040;branch=z9hG4bK00046B2C-AA10-15C0-8085-071411ACAA77\r\n\r\n", ' ');
			Assert.AreEqual("Via", result.Name);
			Assert.AreEqual("SIP/2.0/TCP sv049vm.aeu.local:5040", result.Value);
			Assert.AreEqual("z9hG4bK00046B2C-AA10-15C0-8085-071411ACAA77", result.GetParameter<ViaBranch>()?.Value);

			result = (ViaHeader)SIPGrammar.ViaHeader.Parse("Via: SIP/2.0/UDP 80.10.231.51:5060;branch=z9hG4bKndcutq20e8jk52fcnie0.1;initialinvite=yes\r\n\r\n", ' ');
			Assert.AreEqual("Via", result.Name);
			Assert.AreEqual("SIP/2.0/UDP 80.10.231.51:5060", result.Value);
			Assert.AreEqual("z9hG4bKndcutq20e8jk52fcnie0.1", result.GetParameter<ViaBranch>()?.Value);

			result = (ViaHeader)SIPGrammar.ViaHeader.Parse("Via: SIP/2.0/UDP 172.17.21.13:5060;branch=z9hG4bKac226429781\r\n\r\n", ' ');
			Assert.AreEqual("Via", result.Name);
			Assert.AreEqual("SIP/2.0/UDP 172.17.21.13:5060", result.Value);
			Assert.AreEqual("z9hG4bKac226429781", result.GetParameter<ViaBranch>()?.Value);


		}
		[TestMethod]
		public void ShouldParseSessionExpiresHeader()
		{
			MessageHeader result;

			result = (MessageHeader)SIPGrammar.CustomHeader.Parse("Session-Expires: 1800;refresher=uac\r\n\r\n", ' ');
			Assert.AreEqual("Session-Expires", result.Name);
			//Assert.AreEqual("1800", result.Value);
			//Assert.AreEqual("z9hG4bKndcutq20e8jk52fcnie0.1", result.GetParameter<ViaBranch>()?.Value);

		}
		[TestMethod]
		public void ShouldParseRequireHeader()
		{
			RequireHeader result;

			result = (RequireHeader)SIPGrammar.RequireHeader.Parse("Require: timer\r\n", ' ');
			Assert.AreEqual("Require", result.Name);
			Assert.AreEqual("timer", result.Value);

		}

		[TestMethod]
		public void ShouldParseSupportedHeader()
		{
			MessageHeader result;

			result = (MessageHeader)SIPGrammar.CustomHeaderWithoutValue.Parse("Supported:\r\n", ' ');
			Assert.AreEqual("Supported", result.Name);

		}


		[TestMethod]
		public void ShouldParseHeaderLine1()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine1, ' ').ToArray();
			Assert.AreEqual(13, result.Length);
			Assert.IsTrue(result[0] is ViaHeader);
			Assert.IsTrue(result[1] is AllowHeader);
		}

		[TestMethod]
		public void ShouldParseHeaderLine2()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine2, ' ').ToArray();
			Assert.AreEqual(15, result.Length);
			Assert.IsTrue(result[0] is AllowHeader);
			Assert.IsTrue(result[2] is UserAgentHeader);
		}
		[TestMethod]
		public void ShouldParseHeaderLine3()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine3, ' ').ToArray();
			Assert.AreEqual(13, result.Length);
			Assert.IsTrue(result[0] is ViaHeader);
			Assert.IsTrue(result[1] is MaxForwardsHeader);
			Assert.IsTrue(result[2] is FromHeader);
		}
		[TestMethod]
		public void ShouldParseHeaderLine4()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine4, ' ').ToArray();
			Assert.AreEqual(15, result.Length);
			Assert.IsTrue(result[0] is ViaHeader);
			Assert.IsTrue(result[1] is ToHeader);
			Assert.IsTrue(result[2] is FromHeader);
		}
		[TestMethod]
		public void ShouldParseHeaderLine5()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine5, ' ').ToArray();
			Assert.AreEqual(12, result.Length);
			Assert.IsTrue(result[0] is ToHeader);
			Assert.IsTrue(result[1] is FromHeader);
			Assert.IsTrue(result[2] is CallIDHeader);
		}


		[TestMethod]
		public void ShouldParseRequestLine1()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine1, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("ecb.core.nord:5060", ((SIPURL)result.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33140143960@ecb.core.nord:5060;user=phone", result.RequestURI.ToString());
		}
		[TestMethod]
		public void ShouldParseRequestLine2()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine2, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("100.127.1.1", ((SIPURL)result.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:0243444265@100.127.1.1;transport=UDP;user=phone", result.RequestURI.ToString());
		}
		[TestMethod]
		public void ShouldParseRequestLine3()
		{
			RequestLine result;
			

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine3, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("100.127.2.1", ((SIPURL)result.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33450050545@100.127.2.1;user=phone;BP=1086996", result.RequestURI.ToString());
		}
		[TestMethod]
		public void ShouldParseRequestLine4()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine4, ' ');
			Assert.AreEqual("ACK", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("185.221.88.177:5060", ((SIPURL)result.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33663326291@185.221.88.177:5060;user=phone;sdp_iwf;transport=udp", result.RequestURI.ToString());
		}

		[TestMethod]
		public void ShouldParseRequestLine5()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine5, ' ');
			Assert.AreEqual("ACK", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("100.127.2.1", ((SIPURL)result.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:100.127.2.1;transport=udp;sdp_iwf", result.RequestURI.ToString());

		}

		[TestMethod]
		public void ShouldParseRequestLine10()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine10, ' ');
			Assert.AreEqual("ACK", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);

		}

		[TestMethod]
		public void ShouldParseRequestLine6()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine6, ' ');
			Assert.AreEqual("OPTIONS", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("sip:sip.pstnhub.microsoft.com", result.RequestURI.ToString());

		}
		[TestMethod]
		public void ShouldParseRequestLine7()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine7, ' ');
			Assert.AreEqual("NOTIFY", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("sip:10.1.240.40:5060", result.RequestURI.ToString());

		}
		[TestMethod]
		public void ShouldParseRequestLine8()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine8, ' ');
			Assert.AreEqual("REFER", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("sip:0426223338@10.7.240.40:5060;transport=UDP", result.RequestURI.ToString());

		}

		[TestMethod]
		public void ShouldParseRequestLine9()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine9, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("sip:+33299725203@10.91.254.17:5060;user=phone", result.RequestURI.ToString());

		}

		[TestMethod]
		public void ShouldParseStatusLine3()
		{
			StatusLine result;

			result = SIPGrammar.StatusLine.Parse(Consts.StatusLine4, ' ');
			Assert.AreEqual((ushort)180, result.StatusCode);
			Assert.AreEqual("Ringing", result.Reason);
		}


		[TestMethod]
		public void ShouldParseInvite1()
		{
			Request message;

			message=(Request)SIPGrammar.SIPMessage.Parse(Consts.Invite1,' ');
			Assert.AreEqual(13, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("+33140143960", ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}



		
		
		[TestMethod]
		public void ShouldParseInvite2()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Invite2, ' ');
			Assert.AreEqual(15, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("0243444265", ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}


		[TestMethod]
		public void ShouldParseInvite3()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Invite3, ' ');
			Assert.AreEqual(13, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("+33450050545", ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}

		[TestMethod]
		public void ShouldParseInvite4()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Invite4, ' ');
			Assert.AreEqual(13, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("0755643784", ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}

		[TestMethod]
		public void ShouldParseInvite5()
		{
			Response message;

			message = (Response)SIPGrammar.SIPMessage.Parse(Consts.Invite5, ' ');
			Assert.AreEqual(15, message.Headers.Length);
		}
		[TestMethod]
		public void ShouldParseInvite6()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Invite6, ' ');
			Assert.AreEqual(15, message.Headers.Length);
		}
		



		[TestMethod]
		public void ShouldParsePrack1()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Prack1, ' ');
			Assert.IsNotNull(message);
			Assert.AreEqual("PRACK", message.RequestLine.Method);
		}
		[TestMethod]
		public void ShouldParsePrack2()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Prack2, ' ');
			Assert.IsNotNull(message);
			Assert.AreEqual("PRACK", message.RequestLine.Method);
		}
		[TestMethod]
		public void ShouldParseUpdate1()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Update1, ' ');
			Assert.IsNotNull(message);
			Assert.AreEqual("UPDATE", message.RequestLine.Method);
		}
		[TestMethod]
		public void ShouldParseUpdate2()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Update2, ' ');
			Assert.IsNotNull(message);
			Assert.AreEqual("UPDATE", message.RequestLine.Method);
		}

		[TestMethod]
		public void ShouldParseResponse1()
		{
			Response message;

			message = (Response)SIPGrammar.SIPMessage.Parse(Consts.Ringing1, ' ');
			Assert.AreEqual((ushort)180, message.StatusLine.StatusCode);
			Assert.AreEqual("Ringing", message.StatusLine.Reason);
			Assert.AreEqual(12, message.Headers.Length);
			Assert.IsTrue(string.IsNullOrEmpty(message.Body));
		}
		[TestMethod]
		public void ShouldParseResponse2()
		{
			Response message;

			message = (Response)SIPGrammar.SIPMessage.Parse(Consts.OK1, ' ');
			Assert.AreEqual((ushort)200, message.StatusLine.StatusCode);
			Assert.AreEqual("OK", message.StatusLine.Reason);
			Assert.AreEqual(13, message.Headers.Length);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}



		[TestMethod]
		public void ShouldParseACK1()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.ACK1, ' ');
			Assert.AreEqual(10, message.Headers.Length);
			Assert.AreEqual("ACK", message.RequestLine.Method);
			Assert.AreEqual("+33663326291", ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}

		[TestMethod]
		public void ShouldParseACK2()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.ACK2, ' ');
			Assert.AreEqual(10, message.Headers.Length);
			Assert.AreEqual("ACK", message.RequestLine.Method);
			Assert.IsNull( ((SIPURL)message.RequestLine.RequestURI).UserInfo.User);
			Assert.IsTrue(string.IsNullOrEmpty(message.Body));
		}


		[TestMethod]
		public void ShouldParseOption1()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Option1, ' ');
			Assert.AreEqual(12, message.Headers.Length);
		}


	}
}
