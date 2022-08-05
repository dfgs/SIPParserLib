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
		public void ShouldParseInvite1()
		{
			SIPMessage message;

			message=SIPGrammar.SIPMessage.Parse(Consts.Invite1,' ');

		}
	}
}
