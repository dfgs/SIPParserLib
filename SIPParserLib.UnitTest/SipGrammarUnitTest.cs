using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SipGrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseRequestLine1()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine1, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("ecb.core.nord:5060", result.RequestURI.HostPort.ToString());
			Assert.AreEqual("sip:+33140143960@ecb.core.nord:5060;user=phone", result.RequestURI.ToString());
		}
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
			RequestHeader<string> result;

			result = (RequestHeader<string>)SIPGrammar.UserAgentHeader.Parse("User-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\n", ' ');
			Assert.AreEqual("User-Agent", result.Name);
			Assert.AreEqual("OmniPCX Enterprise R12.1 m2.300.23", result.Value);

			result = (RequestHeader<string>)SIPGrammar.AllowHeader.Parse("Allow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\n", ' ');
			Assert.AreEqual("Allow", result.Name);
			Assert.AreEqual("INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO", result.Value);


		
		}
		[TestMethod]
		public void ShouldParseHeaderLine1()
		{
			RequestHeader[] result;

			result = SIPGrammar.RequestHeaders.Parse(Consts.HeaderLine1, ' ').ToArray();
			Assert.AreEqual(13, result.Length);
			Assert.IsTrue(result[0] is ViaHeader);
			Assert.IsTrue(result[1] is AllowHeader);
		}
		[TestMethod]
		public void ShouldParseInvite1()
		{
			Request message;

			message=(Request)SIPGrammar.SIPMessage.Parse(Consts.Invite1,' ');
			Assert.AreEqual(13, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("+33140143960", message.RequestLine.RequestURI.UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}
	}
}
